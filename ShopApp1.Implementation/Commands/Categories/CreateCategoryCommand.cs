using FluentValidation;
using ShopApp1.Application.Commands.Categories;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Implementation.Commands.Categories
{
    public class CreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly ShopApp1Context _context;
        private readonly CreateCategoryValidator _validator;
        public CreateCategoryCommand(ShopApp1Context context, CreateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "create new category (using entity framework)";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            var category = new Category
            {
                Name = request.Name,
                ParentId = request.ParentId
            };

            _context.Categories.Add(category);

            _context.SaveChanges();
        }
    }
}
