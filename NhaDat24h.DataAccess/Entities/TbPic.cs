using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbPic
    {
        public string? Filecomment { get; set; }
        public string? FileUrl { get; set; }
        public string? IdN { get; set; }
        public string? Urlthumbnai { get; set; }
        public int? Height { get; set; }
        public DateTime? Date { get; set; }
        public byte? IsDelete { get; set; }
        public int FileId { get; set; }
    }
}
