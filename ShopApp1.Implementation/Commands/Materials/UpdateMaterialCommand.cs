using FluentValidation;
using ShopApp1.Application.Commands.Materials;
using ShopApp1.Application.DTO;
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
    public class UpdateMaterialCommand : IUpdateMaterialCommand
    {
        private readonly ShopApp1Context _context;
        private readonly UpdateMaterialValidator _validator;

        public UpdateMaterialCommand(ShopApp1Context context, UpdateMaterialValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 15;

        public string Name => "update material (using entity framework)";

        public void Execute(MaterialDto request)
        {
            _validator.ValidateAndThrow(request);

            var material = _context.Materials.Find(request.Id);
            if (material == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Material));
            }
            material.Name = request.Name;
            _context.Update(material);

            _context.SaveChanges();
        }
    }
}
