using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestWeb.Models
{
    public partial class OrderDTO
    {
        public int OrderID { get; set; }
        public int TableID { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}