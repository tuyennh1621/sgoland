using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNickTuVan2
    {
        public int Id { get; set; }
        public int? IdTt { get; set; }
        public int? IdCongty { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Yahoo { get; set; }
        public string? Skype { get; set; }
        public DateTime? NgayThamGia { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string? TenGoi { get; set; }
    }
}
