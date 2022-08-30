using ShopApp1.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp1.Api.Core
{
    public class AnonymousActor : IApplicationActor
    {
        public int Id => 0;

        public string Identity => "Anonymous";

        public IEnumerable<int> AllowedUseCases => new List<int> { 1,2,3,4,5,6,7 };
    }
}
