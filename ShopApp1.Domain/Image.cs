using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public class Image : Entity
    {
        public string Path { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
