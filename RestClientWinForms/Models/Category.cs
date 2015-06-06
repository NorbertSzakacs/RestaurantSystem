namespace RestClientWinForms.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        internal Category(CategoryDTO dto)
        {
            CategoryID = dto.CategoryID;
            CategoryName = dto.CategoryName;
        }

        public virtual CategoryDTO DTO
        {
            get
            {
                return new CategoryDTO
                {
                    CategoryName = CategoryName,
                    CategoryID = CategoryID
                };
            }
        }
    }
}
