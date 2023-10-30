using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblKhachHang
    {
        public int Id { get; set; }
        public int? Idu { get; set; }
        public string? TenKhachHang { get; set; }
        public string? DienThoai { get; set; }
        public string? NhuCau { get; set; }
        public string? GhiChu { get; set; }
        public DateTime? Date { get; set; }
    }
}
