using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Exceptions;
using ShopApp1.Application.Queries.Orders;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Queries.Orders
{
    public class GetOneOrderQuery : IGetOneOrderQuery
    {
        private readonly ShopApp1Context _context;

        public GetOneOrderQuery(ShopApp1Context context)
        {
            _context = context;
        }

        public int Id => 11;

        public string Name => "get one order (using entity framework)";

        public OneOrderSearchDto Execute(int search)
        {
            var order = _context.Orders.Find(search);
            if (order == null)
            {
                throw new EntityNotFoundException(search, typeof(Order));
            }
            var query = _context.Orders.Include(x => x.OrderLines).Where(x => x.Id == search && x.IsActive).First();
            
            var query3 = _context.Users.Include(x => x.Orders).Where(x => x.Id == query.UserId).Select(x=>new UserOrderDto { FirstName=x.FirstName, LastName=x.LastName});

            return new OneOrderSearchDto
            {
                Id = query.Id,
                UserId = query.UserId,
                User = query3,
                OrderDate = query.OrderDate,
                OrderLines = query.OrderLines.Select(y=> new OrderLineDto 
                { 
                    ProductId = (int)y.ProductId,
                    Name = y.Name,
                    Amount = y.Amount,
                    PricePerUnit = y.PricePerUnit
                }),
                OrderStatusName = _context.OrderStatuses.Where(y => y.Id == query.OrderStatusId).Select(y => y.Name).FirstOrDefault().ToString()
            };
        }
    }
}
