using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.DTO
{
    public class OrderDto : BaseDto
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatusId { get; set; }
    }
}
