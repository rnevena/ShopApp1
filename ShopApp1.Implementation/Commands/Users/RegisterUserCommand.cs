using FluentValidation;
using ShopApp1.Application.Commands.Users;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Email;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp1.Implementation.Commands.Users
{
    public class RegisterUserCommand : IRegisterUserCommand
    {
        private readonly ShopApp1Context _context;
        private readonly RegisterValidator _validator;
        private readonly IEmailSender _sender;

        public RegisterUserCommand(ShopApp1Context context, RegisterValidator validator, IEmailSender sender)
        {
            _context = context;
            _validator = validator;
            _sender = sender;
        }

        public int Id => 1;

        public string Name => "register a new user";

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

            _sender.Send(new SendEmailDto
            {
                Subject = "<h1>Welcome to our shop. Please confirm your email.</h1>",
                Content = "<p>Activate your account</p>",
                SendTo = request.Email
            });

        }
    }
}
