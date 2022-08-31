using FluentValidation;
using ShopApp1.Application.Commands.Materials;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Implementation.Commands.Materials
{
    public class CreateMaterialCommand : ICreateMaterialCommand
    {
        private readonly ShopApp1Context _context;
        private readonly CreateMaterialValidator _validator;

        public CreateMaterialCommand(ShopApp1Context context, CreateMaterialValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 15;

        public string Name => "create new material (using entity framework)";

        public void Execute(MaterialDto request)
        {
            _validator.ValidateAndThrow(request);
            var material = new Material
            {
                Name = request.Name
            };
            _context.Materials.Add(material);
            _context.SaveChanges();
        }
    }
}
