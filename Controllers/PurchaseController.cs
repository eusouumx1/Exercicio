using Exercicio.Entities;
using Exercicio.Other;
using Exercicio.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exercicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseRepository _purchaserepo;
        private readonly ProductRepository _productrepo;
        private readonly UserRepository _userrepo;
        private readonly Validation Validator;
        

        public PurchaseController(PurchaseRepository repository, ProductRepository repository1, UserRepository repository2, Validation validator)
        {
            _purchaserepo = repository;
            _productrepo = repository1;
            Validator = validator;
            _userrepo = repository2;
        }


        [HttpGet]
        public IActionResult Purchase(int userId, string password)
        {
            Validator.Validate(_userrepo.ShowUserById(userId), password);
            if (_userrepo.ShowUserById(userId).Validated == false)
            {
                return Unauthorized(new
                {
                    Message = "Invalid Password or Id"
                });
            }
            return Ok(new
            {
                Message = "Purchase made successfully",
                Items = _purchaserepo.ShowCart().Select(x => x.ProductName).ToList()
            });
        }
        [HttpPost]
        public IActionResult Post(int productId)
        {
            _purchaserepo.PutInCart(productId, _productrepo);
            return Ok(new
            {
                Message = "Item added to your cart",
                Items = _purchaserepo.ShowCart().Select(x => x.ProductName).ToList()
            });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _purchaserepo.TakeOffCart(id);
            return NoContent();
        }
    }
}



