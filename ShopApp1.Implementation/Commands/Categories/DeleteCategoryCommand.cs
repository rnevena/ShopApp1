using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
    public class DeleteCategoryCommand : IDeleteCategoryCommand
    {
        private readonly ShopApp1Context _context;
        private readonly DeleteCategoryValidator _validator;

        public DeleteCategoryCommand(ShopApp1Context context, DeleteCategoryValidator validator)
        {
            this._context = context;
            _validator = validator;
        }

        public int Id => 20;

        public string Name => "delete category (using entity framework)";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);
            var category = _context.Categories.Include(x=>x.Products).FirstOrDefault(x=>x.Id==request && x.IsActive);

            if (category == null)
            {
                throw new EntityNotFoundException(request, typeof(Category));
            }
            if (category.Products.Any())
            {
                throw new UseCaseConflictException("this category is linked to products");
            }

            _context.Categories.Remove(category);

            _context.SaveChanges();
        }
    }
}
