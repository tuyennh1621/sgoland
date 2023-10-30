using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblDanhSachCongTy
    {
        public int Id { get; set; }
        public string? TenCongTy { get; set; }
        public string? DiaChi { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public int? IdTt { get; set; }
        public int? Type { get; set; }
        public string? Avatar { get; set; }
    }
}
