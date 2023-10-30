using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class VCanho
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Tang { get; set; }
        public decimal? Giagoc { get; set; }
        public decimal? Chenh { get; set; }
        public string? ThumbnaiImage { get; set; }
        public decimal? Dientich { get; set; }
        public string? Huongbc { get; set; }
        public int? IdchungCu { get; set; }
        public int? Idch { get; set; }
        public DateTime? Datein { get; set; }
        public string? ToaChungcu { get; set; }
        public string? Descrip { get; set; }
        public int? PhongNgu { get; set; }
        public int? PhongWc { get; set; }
        public string? FullImage { get; set; }
    }
}
