using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Exceptions;
using ShopApp1.Application.Queries.Categories;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp1.Implementation.Queries.Categories
{
    public class GetOneCategoryQuery : IGetOneCategoryQuery
    {
        private readonly ShopApp1Context _context;

        public GetOneCategoryQuery(ShopApp1Context context)
        {
            _context = context;
        }
        public int Id => 3;

        public string Name => "get products belonging to one category";

        public OneCategoryDto Execute(int search)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == search && x.IsActive);
            var category2 = _context.Categories.FirstOrDefault(x => x.ParentId == search && x.IsActive);

            
            var query = _context.Products.Include(x => x.Category).Where(x => x.CategoryId == search);
            //var query2 = _context.Categories.Include(x => x.ParentCategory).Where(x => x.ParentId == search).FirstOrDefault();
            //var query3 = _context.Products.Include(x => x.Category).Where(x => x.CategoryId == query2.Id);

            var query3 = _context.Products.Include(x => x.Category).Where(x => x.Category.ParentId != null && x.Category.ParentId == search);


            if (category == null)
            {
                throw new EntityNotFoundException(search, typeof(Category));
            }

            // provera da li je prosleđeni id možda parent id i u slučaju da jeste izbacuje sve proizvode
            // koji spadaju pod podkategrije te kategorije

            else if (category2 != null)
            {
                {
                    return new OneCategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Products = query3.Select(y => new ProductDto
                        {
                            Id = y.Id,
                            Name = y.Name,
                            Description = y.Description,
                            Price = y.Price
                        })

                    };
                }
            }

            return new OneCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Products = query.Select(y => new ProductDto
                {
                    Id = y.Id,
                    Name = y.Name,
                    Description = y.Description
                })

            };
        }
    }
}
