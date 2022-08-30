using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public class OrderLine : Entity
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal PricePerUnit { get; set; }
        public int OrderId { get; set; }
        public int? ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
