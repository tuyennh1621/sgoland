using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblHeSoDuToan
    {
        public int Id { get; set; }
        public int? Idln { get; set; }
        public int? Idm { get; set; }
        public double? Hesochung1 { get; set; }
        public double? Hesochung2 { get; set; }
        public decimal? Cpnhancong1 { get; set; }
        public decimal? Cpnhancong2 { get; set; }
        public decimal? Cpxdphantho1 { get; set; }
        public decimal? Cpxpphantho2 { get; set; }
        public decimal? Cphoanthien1 { get; set; }
        public decimal? Cphoanthien2 { get; set; }
    }
}
