using FluentValidation;
using ShopApp1.Application.Commands.Users;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Exceptions;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Commands.Users
{
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly ShopApp1Context _context;
        private readonly RegisterValidator _validator;

        public UpdateUserCommand(ShopApp1Context context, RegisterValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 21;

        public string Name => "update user (using entity framework)";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);
            var user = _context.Users.Find(request.Id);
            if(user==null)
            {
                throw new EntityNotFoundException(request.Id, typeof(User));
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Username = request.Username;
            user.Password = request.Password;

            _context.Update(user);

            _context.SaveChanges();
        }
    }
}
