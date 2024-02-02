using Exercicio.Entities;
using Exercicio.Repository;

namespace Exercicio.Other
{
    public class Validation
    {
        private readonly UserRepository _userrepo;

        public Validation(UserRepository repository)
        { 
            _userrepo = repository;
        }
        public void Validate(User user, string senha)
        {
            if (user.Password != senha)
            {
                user.Validated = false;
            }
            else
            {
                user.Validated = true;
            }
        }
    }
}
