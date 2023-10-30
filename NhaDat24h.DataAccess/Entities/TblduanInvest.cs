using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblduanInvest
    {
        public int Id { get; set; }
        public string? IdDuan { get; set; }
        public int? Ngu { get; set; }
        public int? Wc { get; set; }
        public double? Price { get; set; }
        public double? Totalprice { get; set; }
        public double? Tiemnang { get; set; }
        public double? Daban { get; set; }
    }
}
