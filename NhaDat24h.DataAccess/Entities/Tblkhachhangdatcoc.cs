using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblkhachhangdatcoc
    {
        public int Id { get; set; }
        public string? Tenkhachhang { get; set; }
        public string? Socmt { get; set; }
        public string? Mobile { get; set; }
        public string? Diachi { get; set; }
        public int? IdNv { get; set; }
        public DateTime? Datein { get; set; }
        public string? IdCanho { get; set; }
        public int? Tinhtrang { get; set; }
        public int? IdChitietcanho { get; set; }
        public int? Tang { get; set; }
        public decimal? Giagoc { get; set; }
        public decimal? Chenh { get; set; }
        public decimal? Datcoc { get; set; }
        public int? Intstatus { get; set; }
    }
}
