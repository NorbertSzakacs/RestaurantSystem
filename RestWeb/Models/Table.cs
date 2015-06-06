namespace RestWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        internal Table(TableDTO dto)
        {
            TableID = dto.TableID;
            Capacity = dto.Capacity;
            Reserved = dto.Reserved;
            InUse = dto.InUse;
        }

        public TableDTO DTO
        {
            get
            {
                return new TableDTO
                {
                    TableID = TableID,
                    Capacity = Capacity,
                    Reserved = Reserved,
                    InUse = InUse
                };
            }
        }
    }
}
