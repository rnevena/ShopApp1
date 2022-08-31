using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp1.Application.DTO
{
    public class OneOrderSearchDto : OrderSearchDto
    {
        public IEnumerable<OrderLineDto> OrderLines { get; set; } = new List<OrderLineDto>();
        public IEnumerable<UserOrderDto> User { get; set; } = new List<UserOrderDto>();
    }
    public class OrderLineDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal PricePerUnit { get; set; }
    }
    public class UserOrderDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
