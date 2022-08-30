using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public virtual ICollection<UserUseCase> UserUseCases { get; set; } = new List<UserUseCase>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
