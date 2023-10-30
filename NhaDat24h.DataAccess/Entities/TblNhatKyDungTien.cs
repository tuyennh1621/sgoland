using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNhatKyDungTien
    {
        public int Id { get; set; }
        public string? Iduser { get; set; }
        public int? IdN { get; set; }
        public int? IddichVuDungTien { get; set; }
        public DateTime? ThoiGianPhatSinh { get; set; }
        public DateTime? ThoiGianKetThucDv { get; set; }
        public DateTime? ThoiGianKetThucLenh { get; set; }
        public int? Status { get; set; }
        public double? SoTien { get; set; }
    }
}
