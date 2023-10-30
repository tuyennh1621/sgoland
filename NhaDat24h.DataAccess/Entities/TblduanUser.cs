using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblduanUser
    {
        public int Id { get; set; }
        public int? Idu { get; set; }
        public int? Idduan { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public string? Keyword { get; set; }
        public int? IdKv { get; set; }
        public DateTime? Createdate { get; set; }
        public int? Intupdate { get; set; }
        public byte? Intactive { get; set; }
        public byte? IntTheme { get; set; }
        public string? OriginalUrl { get; set; }
        public byte? IntGoiDichVu { get; set; }
        public byte? IntHot { get; set; }
        public DateTime? Enddate { get; set; }
        public string? Logo { get; set; }
        public string? Domain { get; set; }
        public string? Idga { get; set; }
        public string? About { get; set; }
        public byte? IntHomePage { get; set; }
    }
}
