namespace RestWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int CategoryID { get; set; }

        [Required]
        [StringLength(15)]
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
