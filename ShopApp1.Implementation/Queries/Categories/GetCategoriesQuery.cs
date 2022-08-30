using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Queries;
using ShopApp1.Application.Queries.Categories;
using ShopApp1.Application.Searches;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp1.Implementation.Queries.Categories
{
    public class GetCategoriesQuery : IGetCategoriesQuery
    {
        private readonly ShopApp1Context _context;

        public GetCategoriesQuery(ShopApp1Context context)
        {
            _context = context;
        }

        public int Id => 2;

        public string Name => "search categories";

        public PagedResponse<CategoryDto> Execute(CategoriesPagedSearch search)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }
            var skipItems = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<CategoryDto>();
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId
               
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
