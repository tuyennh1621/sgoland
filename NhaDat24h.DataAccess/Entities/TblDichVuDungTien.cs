using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblDichVuDungTien
    {
        public int Id { get; set; }
        public string? DichVu { get; set; }
        public double? DonGia { get; set; }
    }
}
