using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tbldatmua
    {
        public int Id { get; set; }
        public string? Ten { get; set; }
        public string? Mobile { get; set; }
        public string? Diachi { get; set; }
        public string? Thongtin { get; set; }
        public int? Idlink { get; set; }
        public bool? IntActive { get; set; }
        public DateTime? Datein { get; set; }
        public string? Email { get; set; }
        public int? IdTt { get; set; }
        public int? IdQh { get; set; }
        public int? IdKv { get; set; }
        public int? Idduan { get; set; }
        public byte? Idstyle { get; set; }
    }
}
