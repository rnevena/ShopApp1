using FluentValidation;
using ShopApp1.Application.Commands.Users;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Email;
using ShopApp1.DataAccess;
using ShopApp1.Implementation.Validators.Users;
using System;
using System.Collections.Generic;
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
            _context.Users.Add(new Domain.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Address = request.Address
            });

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
