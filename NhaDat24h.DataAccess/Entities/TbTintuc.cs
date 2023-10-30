using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbTintuc
    {
        public int IdTin { get; set; }
        public string? Title { get; set; }
        public string? Tomtat { get; set; }
        public string? Content { get; set; }
        public DateTime? Date { get; set; }
        public string? IdU { get; set; }
        public string? UrlImage { get; set; }
        public int? IdNt { get; set; }
        public int? Numclick { get; set; }
        public string? UrlThumbnai { get; set; }
        public int? Intstyle { get; set; }
        public string? NewLink { get; set; }
        public string? Title2 { get; set; }
        public int? Mode { get; set; }
    }
}
