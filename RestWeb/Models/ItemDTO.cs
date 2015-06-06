using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestWeb.Models
{
    public class ItemDTO
    {
        public int ItemID { get; set; }

        public string ItemName { get; set; }

        public string CategoryName { get; set; }

        public int CategoryID { get; set; }        

        public string Description { get; set; }

        public int? UnitPrice { get; set; }
    }
}