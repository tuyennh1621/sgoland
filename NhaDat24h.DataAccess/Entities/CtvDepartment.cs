using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class CtvDepartment
    {
        public int Id { get; set; }
        public int IdCtv { get; set; }
        public decimal IdDepartment { get; set; }

        public virtual Tblctvonline? IdCtvNavigation { get; set; }
        public virtual Department? IdDepartmentNavigation { get; set; }
    }
}
