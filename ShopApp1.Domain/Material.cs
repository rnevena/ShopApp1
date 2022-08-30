using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public class Material : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();
    }
}
