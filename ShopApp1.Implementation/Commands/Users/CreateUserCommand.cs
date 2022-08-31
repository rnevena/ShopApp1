using FluentValidation;
using ShopApp1.Application.Commands.Users;
using ShopApp1.Application.DTO;
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
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly ShopApp1Context _context;
        private readonly RegisterValidator _validator;

        public CreateUserCommand(ShopApp1Context context, RegisterValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 21;

        public string Name => "create user (using entity framework)";

        public void Execute(UserDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
                Address = request.Address
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            var useCases = Enumerable.Range(2, 9).ToList();
            useCases.ForEach(x => _context.UserUseCases.Add(new UserUseCase
            {
                UserId = user.Id,
                UserUseCaseId = x
            }));

            _context.SaveChanges();
        }
    }
}
