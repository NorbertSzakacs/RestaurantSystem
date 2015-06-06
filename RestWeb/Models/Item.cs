namespace RestWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    [Serializable]
    public partial class Item 
    {
        public Item()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ItemID { get; set; }

        [Required]
        [StringLength(30)]
        public string ItemName { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int? UnitPrice { get; set; }
        [XmlIgnore]
        public virtual Category Category { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        internal Item(ItemDTO dto)
        {
            ItemID = dto.ItemID;
            ItemName = dto.ItemName;
            CategoryID = dto.CategoryID;
            Description = dto.Description;
            UnitPrice = dto.UnitPrice;
        }

        public ItemDTO DTO
        {
            get
            {
                return new ItemDTO
                {
                    ItemID = ItemID,
                    ItemName = ItemName,
                    CategoryName = Category.CategoryName,
                    CategoryID = CategoryID,
                    Description = Description,
                    UnitPrice = UnitPrice
                };
            }
        }
    }
}
