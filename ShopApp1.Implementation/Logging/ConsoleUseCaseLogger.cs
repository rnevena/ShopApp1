using ShopApp1.Application;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Implementation.Logging
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(IUseCase useCase, IApplicationActor applicationActor, object data)
        {
            Console.WriteLine($"{DateTime.Now}: {applicationActor.Identity} is trying to: {useCase.Name} using data: " +
                $"{JsonConvert.SerializeObject(data)}");
        }
    }
}
