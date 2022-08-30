using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Application.DTO
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }

}
