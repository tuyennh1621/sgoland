using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblDonGiaVip
    {
        public int Id { get; set; }
        public int? IdTt { get; set; }
        public int? IdLoaivip { get; set; }
        public double? Dongia { get; set; }
    }
}
