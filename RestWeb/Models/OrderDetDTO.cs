using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestWeb.Models
{
    public class OrderDetDTO
    {
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int UnitPrice { get; set; }
        public short Quantity { get; set; }

        //public virtual Item Item { get; set; }

        //public virtual Order Order { get; set; }
    }
}