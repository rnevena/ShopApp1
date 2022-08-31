using Microsoft.EntityFrameworkCore;
using ShopApp1.Application;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Exceptions;
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
    public class GetPersonalOrdersQuery : IGetPersonalOrdersQuery
    {
        private readonly ShopApp1Context _context;
        private readonly IApplicationActor _user;

        public GetPersonalOrdersQuery(ShopApp1Context context, IApplicationActor user)
        {
            _context = context;
            _user = user;
        }
        public int Id => 9;

        public string Name => "search personal orders";

        public PagedResponse<OrderSearchDto> Execute(OrdersPagedSearch search)
        {
            var query = _context.Orders.AsQueryable();
            //var query2 = query.Single(x => x.UserId == _user.Id).UserId;

            if (!string.IsNullOrEmpty(search.Id) || !string.IsNullOrWhiteSpace(search.Id))
            {
                query = query.Where(x => x.Id.ToString().Equals(search.Id));
            }
            if (!string.IsNullOrEmpty(search.UserId) || !string.IsNullOrWhiteSpace(search.UserId))
            {
                query = query.Where(x => x.UserId.ToString().Equals(search.UserId));
            }


            if (_user.Id != query.Single(x => x.UserId == _user.Id).UserId)
            {
                throw new UnAuthorizedAccessUserException(_user, Name);
            }
                var skipItems = (search.Page.Value - 1) * search.PerPage.Value;
                var response = new PagedResponse<OrderSearchDto>();
                response.TotalCount = query.Count();
                response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new OrderSearchDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    OrderDate = x.OrderDate,
                    OrderStatusName = _context.OrderStatuses.Where(y => y.Id == x.OrderStatusId).Select(y => y.Name).FirstOrDefault().ToString()

                }).ToList();

                response.CurrentPage = search.Page.Value;
                response.ItemsPerPage = search.PerPage.Value;

                return response;
            
        }
    }
}
