using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.Commands.Products;
using ShopApp1.Application.Exceptions;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Commands.Products
{
    public class DeleteProductCommand : IDeleteProductCommand
    {
        private readonly ShopApp1Context _context;

        public DeleteProductCommand(ShopApp1Context context)
        {
            _context = context;
        }

        public int Id => 19;

        public string Name => "delete product (using entity framework)";

        public void Execute(int request)
        {
            var product = _context.Products.Find(request);
            //var query = _context.ProductMaterials.Include(x => x.Product).Where(x => x.ProductId == request).FirstOrDefault();
            

            if (product==null)
            {
                throw new EntityNotFoundException(request, typeof(Product));
            }
            product.IsActive = false;
            product.DeletedAt = DateTime.Now;
            //var product_materials = product.ProductMaterials;
            //if (product_materials != null)
            //{
            //    foreach (var pm in product_materials)
            //    {
            //        _context.ProductMaterials.Remove(query);
            //    }
            //}
            _context.SaveChanges();
        }
    }
}
