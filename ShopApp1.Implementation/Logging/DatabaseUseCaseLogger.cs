using Newtonsoft.Json;
using ShopApp1.Application;
using ShopApp1.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Implementation.Logging
{
    public class DatabaseUseCaseLogger : IUseCaseLogger
    {
        private readonly ShopApp1Context _context;

        public DatabaseUseCaseLogger(ShopApp1Context context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor applicationActor, object useCaseData)
        {
            _context.UseCaseLogs.Add(new Domain.UseCaseLog
            {
                UserId = applicationActor.Id,
                Actor = applicationActor.Identity,
                Data = JsonConvert.SerializeObject(useCaseData),
                CreatedAt = DateTime.UtcNow,
                UseCaseName = useCase.Name
            });

            _context.SaveChanges();
        }
    }
}
