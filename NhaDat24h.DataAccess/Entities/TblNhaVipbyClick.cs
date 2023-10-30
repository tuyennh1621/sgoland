using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNhaVipbyClick
    {
        public int Id { get; set; }
        public int? Idn { get; set; }
        public int? IdTt { get; set; }
        public int? IdQ { get; set; }
        public int? IdKv { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Status { get; set; }
        public int? Click { get; set; }
        public int? NganSach { get; set; }
    }
}
