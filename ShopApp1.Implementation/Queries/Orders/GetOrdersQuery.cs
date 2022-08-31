using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Queries;
using ShopApp1.Application.Queries.Orders;
using ShopApp1.Application.Searches;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Queries.Orders
{
    public class GetOrdersQuery : IGetOrdersQuery
    {
        private readonly ShopApp1Context _context;

        public GetOrdersQuery(ShopApp1Context context)
        {
            _context = context;
        }
        public int Id => 10;

        public string Name => "search orders";

        public PagedResponse<OrderSearchDto> Execute(OrdersPagedSearch search)
        {
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(search.Id) || !string.IsNullOrWhiteSpace(search.Id))
            {
                query = query.Where(x => x.Id.ToString().Equals(search.Id));
            }
            if (!string.IsNullOrEmpty(search.UserId) || !string.IsNullOrWhiteSpace(search.UserId))
            {
                query = query.Where(x => x.UserId.ToString().Equals(search.UserId));
            }
            

            var skipItems = (search.Page.Value - 1) * search.PerPage.Value;
            var response = new PagedResponse<OrderSearchDto>();
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new OrderSearchDto
            {
                Id = x.Id,
                UserId = x.UserId,
                OrderDate = x.OrderDate,
                OrderStatusName = _context.OrderStatuses.Where(y=>y.Id==x.OrderStatusId).Select(y=>y.Name).FirstOrDefault().ToString()
            
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
