using Exercicio.Entities;
using Exercicio.Other;
using Exercicio.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Exercicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _productsrepo;
        private readonly UserRepository _userrepo;
        private readonly Validation Validator;

        public ProductsController(ProductRepository repository, UserRepository repository1, Validation validator)
        {
            _productsrepo = repository;
            _userrepo = repository1;
            Validator = validator;

        }
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_productsrepo.ShowProducts());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Produto = _productsrepo.ShowProductById(id);
            return Produto is null ? NotFound() : Ok(Produto);
        }
        [HttpPost]
        public IActionResult Post(int UserId, string Password, [FromBody]Product produto)
        {
            Validator.Validate(_userrepo.ShowUserById(UserId), Password);
            if (_userrepo.ShowUserById(UserId).Validated = false || _userrepo.ShowUserById(UserId).Admin == false)
            {
                return Unauthorized(new
                {
                    Message = "You must be an admin and be validated to access this function"
                });
            }
            _productsrepo.RegisterProduct(produto);

            return Ok(produto);
        }
        [HttpPut]
        public IActionResult Put(int UserId, string Password, int id, [FromBody]Product product)
        {
            Validator.Validate(_userrepo.ShowUserById(UserId), Password);
            if (_userrepo.ShowUserById(UserId).Validated = false || _userrepo.ShowUserById(UserId).Admin == false)
            {
                return Unauthorized(new
                {
                    Message = "You must be an admin and be validated to access this function"
                });
            }
            var NewProduct = _productsrepo.Edit(product, id);

            return NewProduct is null ? NotFound() : Ok(NewProduct);

        }
        [HttpDelete]
        public IActionResult Delete( int UserId, string Password, int id)
        {
            Validator.Validate(_userrepo.ShowUserById(UserId), Password);
            if (_userrepo.ShowUserById(UserId).Validated = false || _userrepo.ShowUserById(UserId).Admin == false)
            {
                return Unauthorized(new
                {
                    Message = "You must be an admin and be validated to access this function"
                });
            }
            _productsrepo.Delete(id);
            return NoContent();

        }
    }
}
