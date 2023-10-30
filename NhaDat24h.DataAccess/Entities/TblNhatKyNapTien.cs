using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNhatKyNapTien
    {
        public int Id { get; set; }
        public int? HinhThucNapTien { get; set; }
        public DateTime? ThoiGianNap { get; set; }
        public double? SoTienNap { get; set; }
        public int? Idu { get; set; }
    }
}
