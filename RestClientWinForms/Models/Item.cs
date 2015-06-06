namespace RestClientWinForms.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Item
    {
        public Item()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ItemID { get; set; }

        public string ItemName { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public int? UnitPrice { get; set; }

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
