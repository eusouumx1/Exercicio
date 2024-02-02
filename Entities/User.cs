namespace Exercicio.Entities
{
    public class User
    {
            public int Id { get; set; }
            public string Name { get; set; }
        public string Password { get; set; }
        public bool Validated { get; set; }
        public bool Admin { get; set; }

        //public User(int id, string nome, bool admin)
        //{
        //    Id = id;
        //    Name = nome;
        //    Admin = admin;
        //}
    }
}
