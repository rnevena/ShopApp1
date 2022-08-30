using FluentValidation;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator(ShopApp1Context context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name must not be empty")
                .MinimumLength(3).WithMessage("Product name must be between 3 and 30 characters long")
                .MaximumLength(30).WithMessage("Product name must be between 3 and 30 characters long")
                .Must(x => !context.Products.Any(y => y.Name == x))
                .WithMessage("Product name already exists.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Product description must not be empty")
                .MinimumLength(5).WithMessage("Product description must be between 5 and 200 characters long")
                .MaximumLength(200).WithMessage("Product name must be between 5 and 200 characters long");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category must not be empty.")
                .Must(x => context.Categories.Any(y => y.Id == x && y.IsActive))
                .WithMessage("The chosen category Id does not exist.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Product price must not be empty");

            RuleForEach(x => x.MaterialId).Must(x => context.Materials.Any(y => y.Id == x && y.IsActive))
                .WithMessage("The chosen material Id does not exist.");
            
        }
    }
}
