using FluentValidation;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Validators.Categories
{
    public class DeleteCategoryValidator : AbstractValidator<int>
    {
        public DeleteCategoryValidator(ShopApp1Context context)
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
