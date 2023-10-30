using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class ViewTintucGetbyTagid
    {
        public int IdTin { get; set; }
        public string? Title { get; set; }
        public int? IdNt { get; set; }
        public string? Tennt { get; set; }
        public string? Title2 { get; set; }
        public string? Tomtat { get; set; }
        public DateTime? Date { get; set; }
        public int? Idtag { get; set; }
        public string? UrlImage { get; set; }
        public string? UrlThumbnai { get; set; }
        public string? NewLink { get; set; }
    }
}
