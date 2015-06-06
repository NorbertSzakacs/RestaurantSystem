namespace RestWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    [Serializable]
    public partial class OrderDetail
    {
        public OrderDetail() { }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemID { get; set; }
        public int UnitPrice { get; set; }
        public short Quantity { get; set; }
       
        public virtual Item Item { get; set; }
       
        public virtual Order Order { get; set; }

        internal OrderDetail(OrderDetDTO dto)
        {
            OrderID = dto.OrderID;
            ItemID = dto.ItemID;            
            Quantity = dto.Quantity;
            UnitPrice = dto.UnitPrice;
        }

        public OrderDetDTO DTO
        {
            get
            {
                return new OrderDetDTO
                {
                    OrderID = OrderID,
                    ItemID = ItemID,
                    Quantity = Quantity,
                    UnitPrice = UnitPrice
                };
            }
        }
    }
}
