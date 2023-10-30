using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbNhagr
    {
        public int IdNgr { get; set; }
        public int? IdTt { get; set; }
        public int? IdQ { get; set; }
        public int? IdLg { get; set; }
        public string? IdU { get; set; }
        public string? Header { get; set; }
        public string? Content { get; set; }
        public DateTime? Ngaydang { get; set; }
        public DateTime? Ngayhethan { get; set; }
        public double? Dientich { get; set; }
        public double? Giatien { get; set; }
        public int? IdLn { get; set; }
        public int? Count { get; set; }
        public int? IdLt { get; set; }
        public int? Tinvip { get; set; }
        public string? Urlpic1 { get; set; }
        public int? Flag { get; set; }
        public int? IdHn { get; set; }
        public int? Tl { get; set; }
    }
}
