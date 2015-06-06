namespace RestClientWinForms.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Table
    {
        public Table()
        {
            Orders = new HashSet<Order>();
        }

        public int TableID { get; set; }

        public short Capacity { get; set; }

        public bool? Reserved { get; set; }

        public bool? InUse { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
