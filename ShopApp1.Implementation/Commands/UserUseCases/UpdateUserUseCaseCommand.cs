using FluentValidation;
using ShopApp1.Application.Commands.UserUseCases;
using ShopApp1.Application.DTO;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using ShopApp1.Implementation.Validators.UserUseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Commands.UserUseCases
{
    public class UpdateUserUseCaseCommand : IUpdateUserUseCaseCommand
    {
        private readonly ShopApp1Context _context;
        private readonly UpdateUserUseCaseValidator _validator;

        public UpdateUserUseCaseCommand(ShopApp1Context context, UpdateUserUseCaseValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int Id => 24;

        public string Name => "update user use cases (using entity framework)";

        public void Execute(UpdateUserUseCaseDto request)
        {
            _validator.ValidateAndThrow(request);
            var currentUseCases = _context.UserUseCases.Where(x => x.UserId == request.UserId);
            _context.RemoveRange(currentUseCases);
            var newUseCases = request.UseCaseIds.Select(x => new UserUseCase
            {
                UserId = request.UserId,
                UserUseCaseId = x
            });
            _context.UserUseCases.AddRange(newUseCases);
            _context.SaveChanges();
        }
    }
}
