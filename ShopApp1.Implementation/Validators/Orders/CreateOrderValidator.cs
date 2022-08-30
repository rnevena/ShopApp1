using FluentValidation;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Validators.Orders
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator(ShopApp1Context context)
        {
            RuleForEach(x => x.ProductId).NotEmpty().WithMessage("Product must not be empty.")
                .Must(x => context.Products.Any(y => y.Id == x && y.IsActive))
                .WithMessage("The chosen product Id does not exist.");

            RuleForEach(x => x.Amount).NotEmpty().WithMessage("Amount must not be empty")
                .GreaterThan(0).WithMessage("Amount must be a positive number");

            //RuleFor(x => x.OrderStatusId).NotEmpty().WithMessage("Category must not be empty.")
            //    .Must(x => context.OrderStatuses.Any(y => y.Id == x))
            //    .WithMessage("The chosen order status Id does not exist.");
        }

    }
}
