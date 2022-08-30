using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application
{
    public interface IApplicationActor
    {
        public int Id { get; }
        public string Identity { get; }

        IEnumerable<int> AllowedUseCases { get; }
    }
}
