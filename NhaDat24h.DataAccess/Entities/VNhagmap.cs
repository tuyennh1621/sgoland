using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class VNhagmap
    {
        public int? IdN { get; set; }
        public string? Lat { get; set; }
        public string? Lon { get; set; }
        public string? Header { get; set; }
        public int? IdTt { get; set; }
        public byte? IdLt { get; set; }
        public int? IdLn { get; set; }
        public int? Mode { get; set; }
        public double? Sotien { get; set; }
        public double? Dientich { get; set; }
        public DateTime? Ngaydang { get; set; }
        public string? UrlPixbe { get; set; }
        public string? Contactinfo { get; set; }
        public byte? Smstrue { get; set; }
        public byte? ActionsmsId { get; set; }
        public int? Tinvip { get; set; }
        public int? IdG { get; set; }
        public int? IntDate { get; set; }
        public int? IdQq { get; set; }
    }
}
