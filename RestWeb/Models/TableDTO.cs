using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestWeb.Models
{
    public class TableDTO
    {
        public int TableID { get; set; }
        public short Capacity { get; set; }
        public bool? Reserved { get; set; }
        public bool? InUse { get; set; }
    }
}