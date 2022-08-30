using FluentValidation;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp1.Implementation.Validators.Users
{
    public class RegisterValidator : AbstractValidator<UserDto>
    {
        public RegisterValidator(ShopApp1Context context)
        {
            var rePassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            var reName = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$";
            var reUserName = @"^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("First name is required.")
                .Matches(reName)
                .WithMessage("First name isn't formatted properly.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .Matches(reName)
                .WithMessage("Last name isn't formatted properly.");

            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Username is required.")
                .Matches(reUserName)
                .WithMessage("Username isn't formatted properly.")
                .Must(x => !context.Users.Any(y => y.Username == x))
                .WithMessage("Username is already taken.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("The provided email isn't formatted properly.")
                .Must(x => !context.Users.Any(y => y.Email == x))
                .WithMessage("Email is already taken.");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password is required.")
                .Matches(rePassword)
                .WithMessage("Password must have at least eight characters, at least one uppercase letter, " +
                "one lowercase letter, one number and one special character.");

            RuleFor(x => x.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Address is required.");
        }
    }
}
