using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public class UserUseCase
    {
        public int UserId { get; set; }
        public int UserUseCaseId { get; set; }

        public virtual User User { get; set; } 
    }
}
