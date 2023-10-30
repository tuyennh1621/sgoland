using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblBangGiaDangVip
    {
        public int Id { get; set; }
        public string? LoaiGia { get; set; }
        public double? DonGia { get; set; }
        public int? Mingio { get; set; }
        public int? Maxgio { get; set; }
    }
}
