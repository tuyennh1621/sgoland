using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblBanner2012
    {
        public int Id { get; set; }
        public string? Link { get; set; }
        public string? Image { get; set; }
        public int? IntStyle { get; set; }
        public int? IntPosition { get; set; }
        public int? IntTinhThanh { get; set; }
        public string? Image2 { get; set; }
        public int? IntPage { get; set; }
        public int? IntLoaitin { get; set; }
        public int? Iduser { get; set; }
        public string? Title { get; set; }
        public DateTime? Datein { get; set; }
        public DateTime? Enddate { get; set; }
    }
}
