using FluentValidation;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Validators.Categories
{
    public class UpdateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public UpdateCategoryValidator(ShopApp1Context context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name must not be empty")
                .MinimumLength(3).WithMessage("Category name must be between 3 and 30 characters long")
                .MaximumLength(30).WithMessage("Category name must be between 3 and 30 characters long");
            RuleFor(x => x.ParentId)
               .Must(x => context.Categories.Any(c => c.Id == x && c.IsActive))
               .When(dto => dto.ParentId.HasValue).WithMessage("There is no such parent category");
        }
    }
}
