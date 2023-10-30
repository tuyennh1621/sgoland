using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Department
    {
        public Department()
        {
            CtvDepartments = new HashSet<CtvDepartment>();
            Tblctvonlines = new HashSet<Tblctvonline>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; } = null!;
        public int? IdSuperiors { get; set; }
        public bool Deleted { get; set; }
        public int? IdCongty { get; set; }

        public virtual ICollection<CtvDepartment> CtvDepartments { get; set; }
        public virtual ICollection<Tblctvonline> Tblctvonlines { get; set; }
    }
}
