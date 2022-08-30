using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Queries;
using ShopApp1.Application.Queries.Products;
using ShopApp1.Application.Searches;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Queries.Products
{
    public class GetProductsQuery : IGetProductsQuery
    {
        private readonly ShopApp1Context _context;

        public GetProductsQuery(ShopApp1Context context)
        {
            _context = context;
        }

        public int Id => 6;

        public string Name => "search products";

        public PagedResponse<ProductSearchDto> Execute(ProductsPagedSearch search)
        {
            var query = _context.Products.AsQueryable();
            var query2 = _context.ProductMaterials;
            Console.WriteLine(query.Count());

            if (!string.IsNullOrEmpty(search.Id) || !string.IsNullOrWhiteSpace(search.Id))
            {
                query = query.Where(x => x.Id.ToString().Equals(search.Id));
            }
            if (!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }
            if (!string.IsNullOrEmpty(search.Description) || !string.IsNullOrWhiteSpace(search.Description))
            {
                query = query.Where(x => x.Description.Contains(search.Description));
            }
            var skipItems = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<ProductSearchDto>();
            response.TotalCount = query.Count();
            response.Items = query.Skip(skipItems).Take(search.PerPage.Value).Select(x => new ProductSearchDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = (int)x.Price,
                CategoryId = x.CategoryId,
                Category = _context.Categories.Where(y => y.Id == x.CategoryId).Select(y => y.Name).FirstOrDefault().ToString(),
                Materials = query2.Where(y => y.ProductId == x.Id).Select(y => y.Material).Select(y => new MaterialDto { Id = y.Id, Name = y.Name }).ToList()
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
