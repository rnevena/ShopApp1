using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.DTO
{
    public class CreateProductWithImageDto : CreateProductDto
    {
        public List<IFormFile> Image { get; set; }
    }
}
