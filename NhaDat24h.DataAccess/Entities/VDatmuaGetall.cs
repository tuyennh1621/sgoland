using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class VDatmuaGetall
    {
        public int Id { get; set; }
        public int? IdTt { get; set; }
        public string? TenTt { get; set; }
        public string? Tenquan { get; set; }
        public string? Tenkv { get; set; }
        public string? Title3 { get; set; }
        public bool? IntActive { get; set; }
        public string? Ten { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Diachi { get; set; }
        public string? Thongtin { get; set; }
        public DateTime? Datein { get; set; }
        public string? Tenduan { get; set; }
        public string? Diachiduan { get; set; }
        public string? Tukhoa { get; set; }
        public int? Idduan { get; set; }
        public byte? Idstyle { get; set; }
    }
}
