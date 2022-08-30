using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Exceptions;
using ShopApp1.Application.Queries.Products;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Queries.Products
{
    public class GetOneProductQuery : IGetOneProductQuery
    {
        private readonly ShopApp1Context _context;

        public GetOneProductQuery(ShopApp1Context context)
        {
            _context = context;
        }
        public int Id => 7;

        public string Name => "get one product";

        public OneProductSearchDto Execute(int search)
        {
            var product = _context.Products.Find(search);
            if(product==null)
            {
                throw new EntityNotFoundException(search, typeof(Product));
            }
            var query = _context.Products.Include(x => x.ProductMaterials).Where(x=>x.Id==search && x.IsActive).First();
            var query2 = _context.ProductMaterials;
            var categoryName = _context.Categories.FirstOrDefault(x => x.Id == query.CategoryId);

            return new OneProductSearchDto
            {
                Id = query.Id,
                CategoryId = query.CategoryId,
                Images = query.Images.Select(x => new ImagesProductSearchDto
                {
                    Path = x.Path
                }),
                Name = query.Name,
                Description = query.Description,
                Category = categoryName.Name,
                Materials = query2.Where(y => y.ProductId == query.Id).Select(y => y.Material).Select(y => new MaterialDto { Id = y.Id, Name = y.Name }).ToList()
            };
        }
    }
}
