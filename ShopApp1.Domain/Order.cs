using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp1.Domain
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public int OrderStatusId { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }  = new List<OrderLine>();
        public virtual User User { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }

    //public enum OrderStatus
    //{
    //    Processing,
    //    Recieved,
    //    Delivered,
    //    Shipped,
    //    Canceled
    //}
}
