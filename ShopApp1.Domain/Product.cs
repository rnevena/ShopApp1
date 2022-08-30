using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
