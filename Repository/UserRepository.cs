using Exercicio.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Exercicio.Repository
{
    public class UserRepository
    {
        private List<User> _users = new List<User>()
        {
            new User { Id = 1, Name = "Pedro", Admin = true, Password = "baubau", Validated = false },
            new User { Id = 2, Name = "Lucas", Admin = false, Password = "lol", Validated=false },
        };

        public List<User> ShowUsers()
        {
            return _users;
        }

        public User ShowUserById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public User Register(User newUser)
        {
            newUser.Id = _users.Count + 1;
            newUser.Validated = false;
            _users.Add(newUser);
            return newUser;
        }

        public User Edit(User user, int id)
        {
            var OldUser = ShowUserById(id);
            OldUser.Name = user.Name;
            OldUser.Password = user.Password;
            if (OldUser.Admin == false)
            {
                user.Admin = false;
            }
            OldUser.Admin = user.Admin;
            return OldUser;
        }

        public void Delete(int Id)
        {
            var User = ShowUserById(Id);

            _users.Remove(User);
        }
    }
}
