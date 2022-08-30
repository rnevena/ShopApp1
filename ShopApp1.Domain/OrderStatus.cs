using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Domain
{
    public class OrderStatus : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
