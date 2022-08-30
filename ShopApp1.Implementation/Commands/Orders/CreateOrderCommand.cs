using FluentValidation;
using ShopApp1.Application;
using ShopApp1.Application.Commands.Orders;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Commands.Orders
{
    public class CreateOrderCommand : ICreateOrderCommand
    {
        private readonly ShopApp1Context _context;
        private readonly CreateOrderValidator _validator;
        private readonly IApplicationActor _user;

        public CreateOrderCommand(CreateOrderValidator validator, ShopApp1Context context, IApplicationActor user)
        {
            _validator = validator;
            _context = context;
            _user = user;
        }

        public int Id => 8;

        public string Name => "create order (using entity framework)";

        public void Execute(CreateOrderDto request)
        {
            _validator.ValidateAndThrow(request);

            var order = new Order
            {
                UserId = _user.Id,
                OrderDate = DateTime.Now,
                OrderStatusId = 1
            };
            _context.Orders.Add(order);

            if(request.ProductId.Count()>0)
            {
                foreach(var prod in request.ProductId.Select((value, i) => new { value, i }))
                {
                    var query = _context.Products.Find(prod.value);
                    var o = new OrderLine
                    {
                        
                        ProductId = prod.value,
                        Order = order,
                        Amount = request.Amount[prod.i],
                        PricePerUnit = query.Price,
                        Name = query.Name
                    };
                    _context.OrderLines.Add(o);
                }
            }
            _context.SaveChanges();
        }
    }
}
