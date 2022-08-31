using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.Commands.Materials;
using ShopApp1.Application.Exceptions;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Commands.Materials
{
    public class DeleteMaterialCommand : IDeleteMaterialCommand
    {
        private readonly ShopApp1Context _context;
        private readonly DeleteMaterialValidator _validator;
        

        public DeleteMaterialCommand(ShopApp1Context context, DeleteMaterialValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 16;

        public string Name => "delete material (using entity framework)";

        public void Execute(int request)
        {
            _validator.ValidateAndThrow(request);
            var material = _context.Materials.Include(x => x.ProductMaterials).FirstOrDefault(x => x.Id == request && x.IsActive);

            if(material==null)
            {
                throw new EntityNotFoundException(request, typeof(Material));
            }
            if (material.ProductMaterials.Any())
            {
                throw new UseCaseConflictException("this material is linked to products");
            }
            _context.Materials.Remove(material);

            _context.SaveChanges();
        }
    }
}
