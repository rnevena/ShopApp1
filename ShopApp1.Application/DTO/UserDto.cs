using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application.DTO
{
    public class UserDto : BaseDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
