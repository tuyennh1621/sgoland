using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblDoanhThuNapGold
    {
        public int Id { get; set; }
        public int? Idu { get; set; }
        public int? Idcustomer { get; set; }
        public double? Sotien { get; set; }
        public DateTime? Datein { get; set; }
    }
}
