using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class WebTblnhatkygiaodich
    {
        public int Idnk { get; set; }
        public int? Idn { get; set; }
        public string? Inputer { get; set; }
        public string? KhachHang { get; set; }
        public string? Giaodich { get; set; }
        public string? Ketqua { get; set; }
        public DateTime? Thoigian { get; set; }
        public byte? Intstatusread { get; set; }
    }
}
