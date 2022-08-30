using FluentValidation;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp1.Implementation.Validators.Materials
{
    public class CreateMaterialValidator : AbstractValidator<MaterialDto>
    {
        public CreateMaterialValidator(ShopApp1Context context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Material name must not be empty")
                .MinimumLength(3).WithMessage("Material name must be between 3 and 30 characters long")
                .MaximumLength(30).WithMessage("Material name must be between 3 and 30 characters long")
                .Must(name => !context.Materials.Any(g => g.Name == name)).WithMessage("Material name must be unique");
        }
    }
}
