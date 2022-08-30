using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Exceptions;
using ShopApp1.Application.Queries.Materials;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Queries.Materials
{
    public class GetOneMaterialQuery : IGetOneMaterialQuery
    {
        private readonly ShopApp1Context _context;

        public GetOneMaterialQuery(ShopApp1Context context)
        {
            _context = context;
        }
        public int Id => 5;

        public string Name => "get products belonging to one material";

        public OneMaterialDto Execute(int search)
        {
            var material = _context.Materials.FirstOrDefault(x => x.Id == search && x.IsActive);
            var query = _context.ProductMaterials.Where(x => x.MaterialId == search).Select(x => x.Product);
            
            if(material==null)
            {
                throw new EntityNotFoundException(search, typeof(Material));
            }

            return new OneMaterialDto
            {
                Id = material.Id,
                Name = material.Name,
                Products = query.Select(y => new ProductDto
                {
                    Id = y.Id,
                    Name = y.Name,
                    Description = y.Description,
                    Price = y.Price
                })
            };
        }
    }
}
