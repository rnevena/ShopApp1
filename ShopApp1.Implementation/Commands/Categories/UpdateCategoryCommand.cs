using FluentValidation;
using ShopApp1.Application.Commands.Categories;
using ShopApp1.Application.DTO;
using ShopApp1.Application.Exceptions;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp1.Implementation.Commands.Categories
{
    public class UpdateCategoryCommand : IUpdateCategoryCommand
    {
        private readonly ShopApp1Context _context;
        private readonly UpdateCategoryValidator _validator;
        public UpdateCategoryCommand(ShopApp1Context context, UpdateCategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int Id => 30;

        public string Name => "update category (using entity framework)";

        public void Execute(CategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            var category = _context.Categories.Find(request.Id);

            if (category == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Category));
            }

            category.Name = request.Name;
            category.ParentId = request.ParentId;

            _context.Update(category);

            _context.SaveChanges();
        }
    }
}
