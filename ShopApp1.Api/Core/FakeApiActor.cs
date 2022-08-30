using ShopApp1.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp1.Api.Core
{
    public class FakeApiActor : IApplicationActor
    {
        public int Id => 1;

        public string Identity => "Test Api User";

        public IEnumerable<int> AllowedUseCases => new List<int> { 66 };
    }
    public class FakeApiAdminActor : IApplicationActor
    {
        public int Id => 2;

        public string Identity => "Test Api Admin";

        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1,80);
    }
}
