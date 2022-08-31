using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Queries;
using ShopApp1.Application.Queries.UseCaseLogs;
using ShopApp1.Application.Searches;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Queries.UseCaseLogs
{
    public class UseCaseLogsQuery : IUseCaseLogsQuery
    {
        private readonly ShopApp1Context _context;

        public UseCaseLogsQuery(ShopApp1Context context)
        {
            _context = context;
        }

        public int Id => 25;

        public string Name => "search use case logs (using entity framework)";

        public PagedResponse<UseCaseLogsDto> Execute(UseCaseLogsPagedSearch search)
        {
            var query = _context.UseCaseLogs.AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword) || !string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(x => x.UseCaseName.Contains(search.Keyword) || x.UseCaseName.Contains(search.Keyword));
            }
            if (!string.IsNullOrEmpty(search.UserId) || !string.IsNullOrWhiteSpace(search.UserId))
            {
                query = query.Where(x => x.UserId.ToString().Equals(search.UserId));
            }
            var skipItems = (search.Page.Value - 1) * search.PerPage.Value;
            var response = new PagedResponse<UseCaseLogsDto>();
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new UseCaseLogsDto
            {
                Id = x.Id,
                CreatedAt = DateTime.Parse(x.CreatedAt.ToString()),
                UseCaseName = x.UseCaseName,
                Data = x.Data,
                UserId = x.UserId,
                Actor = x.Actor
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
