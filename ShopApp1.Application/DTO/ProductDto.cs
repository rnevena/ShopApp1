using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application.DTO
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

}
