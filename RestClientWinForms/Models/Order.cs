namespace RestClientWinForms.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderID { get; set; }

        public int TableID { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Table Table { get; set; }
        internal Order(OrderDTO dto)
        {
            OrderID = dto.OrderID;
            TableID = dto.TableID;
            OrderDate = dto.OrderDate;
        }

        public OrderDTO DTO
        {
            get
            {
                return new OrderDTO
                {
                    OrderID = OrderID,
                    TableID = TableID,
                    OrderDate = OrderDate
                };
            }
        }

    }
}
