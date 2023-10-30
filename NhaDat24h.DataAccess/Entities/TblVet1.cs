using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblVet1
    {
        public int Id { get; set; }
        public string? Ten { get; set; }
        public int? Idn { get; set; }
        public DateTime? Ngay { get; set; }
        public string? Idu { get; set; }
    }
}
