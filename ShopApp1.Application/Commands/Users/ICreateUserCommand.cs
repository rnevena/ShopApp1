using ShopApp1.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.Commands.Users
{
    public interface ICreateUserCommand : ICommand<UserDto>
    {
    }
}
