using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class SongBinhluan
    {
        public int Id { get; set; }
        public int? SongId { get; set; }
        public string? BinhLuan { get; set; }
        public string? UserName { get; set; }
        public DateTime? Ngay { get; set; }
    }
}
