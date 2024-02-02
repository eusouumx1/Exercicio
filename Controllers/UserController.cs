using Exercicio.Entities;
using Exercicio.Other;
using Exercicio.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace awaaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserRepository _userrepo;
        private readonly Validation Validator;
        public UserController(UserRepository repository, Validation validator)
        {
            Validator = validator;
            _userrepo = repository;
        }
        [HttpGet]
        public IActionResult List(int userId, string password)
        {
            Validator.Validate(_userrepo.ShowUserById(userId), password);
            if (_userrepo.ShowUserById(userId).Validated == false || _userrepo.ShowUserById(userId).Admin == false)
            {
                return Unauthorized(new
                {
                    Message = "You must be an admin and be validated to access this function"
                });
            }
            return Ok(_userrepo.ShowUsers());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int userId, string password, int id)
        {
            var Usuario = _userrepo.ShowUserById(id);
            Validator.Validate(_userrepo.ShowUserById(userId), password);
            if (_userrepo.ShowUserById(userId).Validated == true && id == userId)
            {
                return Usuario is null ? NotFound() : Ok(Usuario);
            }
            else if(_userrepo.ShowUserById(userId).Validated == false || _userrepo.ShowUserById(userId).Admin == false)
            {
                return Unauthorized(new
                {
                    Message = "You must be an admin and be validated to access this function"
                });
            }
            

            return Usuario is null ? NotFound() : Ok(Usuario);
        }
        [HttpPost]
        public IActionResult Post([FromBody] User usuario)
        {
            _userrepo.Register(usuario);

            return Ok(usuario);
        }
        [HttpPut("{id}")]
        public IActionResult Put( int userId, string password, int id, [FromBody] User usuario )
        {
            
            Validator.Validate(_userrepo.ShowUserById(userId), password);
            if (_userrepo.ShowUserById(userId).Validated == true && id == userId)
            {
                _userrepo.Edit(usuario, id);
            }
            else if (_userrepo.ShowUserById(userId).Validated == false || _userrepo.ShowUserById(userId).Admin == false)
            {
                return Unauthorized(new
                {
                    Message = "You must be an admin and be validated to access this function"
                });
            }
            
                var NewUser = _userrepo.Edit(usuario, id);
            return Ok(NewUser);

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int userId, string password, int id)
        {
            Validator.Validate(_userrepo.ShowUserById(userId), password);
            if (_userrepo.ShowUserById(userId).Validated == true && id == userId)
            {
                _userrepo.Delete(id);
            }
            else if (_userrepo.ShowUserById(userId).Validated == false || _userrepo.ShowUserById(userId).Admin == false)
            {
                return Unauthorized(new
                {
                    Message = "You must be an admin and be validated to access this function"
                });
            }
            
            _userrepo.Delete(id);
            return NoContent();

        }
    }
}
