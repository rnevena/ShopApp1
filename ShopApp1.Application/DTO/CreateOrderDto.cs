using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.DTO
{
    public class CreateOrderDto : BaseDto
    {
        //public int UserId { get; set; }
        //public DateTime OrderDate { get; set; }
        //public int OrderStatusId { get; set; }
        public List<int> ProductId { get; set; } = new List<int>();
        public List<int> Amount { get; set; } = new List<int>();
        //public int Amount { get; set; }
        //public decimal PricePerUnit { get; set; }
        //public string ProductName { get; set; }
    }
}
