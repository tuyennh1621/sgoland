using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblGiaoDichKhachHang
    {
        public int Id { get; set; }
        public int? Idkh { get; set; }
        public string? NoiDungGiaoDich { get; set; }
        public DateTime? Times { get; set; }
    }
}
