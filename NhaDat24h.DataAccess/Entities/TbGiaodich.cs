using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbGiaodich
    {
        public int IdT { get; set; }
        public int? IdN { get; set; }
        public double? Tien { get; set; }
        public DateTime? Ngay { get; set; }
        public int? Thang { get; set; }
        public string? Email { get; set; }
        public int? Ngaygiahan { get; set; }
    }
}
