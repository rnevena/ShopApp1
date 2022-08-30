using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application.DTO
{
    public class OneCategoryDto : BaseDto
    {
        public string Name { get; set; }
        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    }

}
