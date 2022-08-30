using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.DTO
{
    public class ProductSearchDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        //public int? ParentId { get; set; }
        public string Category { get; set; }
        public IEnumerable<MaterialDto> Materials { get; set; } = new List<MaterialDto>();
        
    }
}
