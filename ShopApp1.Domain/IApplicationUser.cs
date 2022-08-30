using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public interface IApplicationUser
    {
        public string Identity { get; }
        public int Id { get; }
        public IEnumerable<int> UseCaseIds { get; }
        public string Email { get; }
    }
}
