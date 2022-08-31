using FluentValidation;
using ShopApp1.Application.Commands.Products;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Commands.Products
{
    public class CreateProductCommand : ICreateProductCommand
    {
        private readonly ShopApp1Context _context;
        private readonly CreateProductValidator _validator;

        public CreateProductCommand(ShopApp1Context context, CreateProductValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 18;

        public string Name => "create new product (using entity framework)";

        public void Execute(CreateProductDto request)
        {
            _validator.ValidateAndThrow(request);
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Price = request.Price
            };

            _context.Products.Add(product);

            if (request.MaterialId.Count() > 0)
            {
                foreach(var mat in request.MaterialId)
                {
                    var m = new ProductMaterial
                    {
                        MaterialId = mat,
                        Product = product
                    };
                    _context.ProductMaterials.Add(m);
                }
            }
            if (request.ImageName.Count() > 0)
            {
                foreach(var img in request.ImageName)
                {
                    var i = new Image
                    {
                        Path = img,
                        Product = product
                    };
                    _context.Images.Add(i);
                }
            }
            _context.SaveChanges();
        }
    }
}
