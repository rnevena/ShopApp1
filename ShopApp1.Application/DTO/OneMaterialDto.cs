using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.DTO
{
    public class OneMaterialDto : BaseDto
    {
        public string Name { get; set; }
        public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
