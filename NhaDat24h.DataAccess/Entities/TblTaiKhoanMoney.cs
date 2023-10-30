using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblTaiKhoanMoney
    {
        public int Id { get; set; }
        public int? IdU { get; set; }
        public int? IdHinhThucNap { get; set; }
        public double? Sotien { get; set; }
        public string? MaGiaoDichNganLuong { get; set; }
        public DateTime? ThoiGianPhatSinh { get; set; }
        public DateTime? ThoiGianHoanThanh { get; set; }
        public string? MaNapTien { get; set; }
    }
}
