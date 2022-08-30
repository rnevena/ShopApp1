using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.DTO
{
    public class OneProductSearchDto : ProductSearchDto
    {
        public IEnumerable<ImagesProductSearchDto> Images { get; set; } = new List<ImagesProductSearchDto>();
    }
    public class ImagesProductSearchDto
    {
        public string Path { get; set; }
    }
}
