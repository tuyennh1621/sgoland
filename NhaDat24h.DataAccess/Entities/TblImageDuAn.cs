using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblImageDuAn
    {
        public int Id { get; set; }
        public int? IdKhuVuc { get; set; }
        public int? IdLoaiBds { get; set; }
        public string? UrlImage { get; set; }
        public string? TitleImage { get; set; }
        public int? IdQ { get; set; }
        public int? IdTt { get; set; }
        public string? Urlthumbnai { get; set; }
    }
}
