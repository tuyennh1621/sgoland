using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblCanHo
    {
        public int Id { get; set; }
        public int? IdchungCu { get; set; }
        public decimal? GiaGoc { get; set; }
        public decimal? Dientich { get; set; }
        public int? PhongNgu { get; set; }
        public int? PhongWc { get; set; }
        public string? Title { get; set; }
        public int? HuongBc { get; set; }
        public int? HuongCuaVao { get; set; }
        public string? ThumbnaiImage { get; set; }
        public string? FullImage { get; set; }
        public int? Stt { get; set; }
    }
}
