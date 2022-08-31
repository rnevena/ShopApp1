using FluentValidation;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Validators.UserUseCases
{
    public class UpdateUserUseCaseValidator : AbstractValidator<UpdateUserUseCaseDto>
    {
        public UpdateUserUseCaseValidator(ShopApp1Context context)
        {
            RuleFor(x => x.UseCaseIds).NotEmpty()
                .WithMessage("UseCaseIds can not be empty.")
                .Must(x => x.Distinct().Count() == x.Count())
                .WithMessage("Duplicates are not allowed.");

            RuleFor(x => x.UserId).NotEmpty()
                .Must(x => context.Users.Any(user => user.Id == x && user.IsActive))
                .WithMessage("The provided user Id does not exist.");
        }
    }
}
