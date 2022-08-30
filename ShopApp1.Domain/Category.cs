using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual ICollection<Category> ChildCategories { get; set; } = new List<Category>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual Category ParentCategory { get; set; }
    }
}
