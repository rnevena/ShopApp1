using Microsoft.EntityFrameworkCore;
using ShopApp1.Application.Commands.Users;
using ShopApp1.Application.Exceptions;
using ShopApp1.DataAccess;
using ShopApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Implementation.Commands.Users
{
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly ShopApp1Context _context;

        public DeleteUserCommand(ShopApp1Context context)
        {
            _context = context;
        }
        public int Id => 22;

        public string Name => "delete user (using entity framework)";

        public void Execute(int request)
        {
            var user = _context.Users.Find(request);
            var query = _context.Orders.Where(x => x.UserId == request);
            var query2 = _context.UserUseCases.Where(x => x.UserId == request);
            if (user==null)
            {
                throw new EntityNotFoundException(request, typeof(User));
            }
            user.IsActive = false;
            user.DeletedAt = DateTime.Now;
            
            
            //var user_use_cases = _context.UserUseCases.Where(x=>x.UserId==request);
            //if(user_use_cases.Count()!=null)
            //{
            //    foreach(var uuc in user_use_cases.Select((value, i) => new { value, i }))
            //    {
            //        _context.Remove(_context.UserUseCases.Find(uuc.value));
            //    }
            //}
            if(query!=null)
            {
                foreach(var i in query)
                {
                    i.IsActive = false;
                    i.DeletedAt = DateTime.Now;
                }
            }
            _context.RemoveRange(query2);

            _context.SaveChanges();

        }
    }
}
