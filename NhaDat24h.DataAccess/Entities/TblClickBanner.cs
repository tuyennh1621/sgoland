using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblClickBanner
    {
        public int Id { get; set; }
        public int? Idbanner { get; set; }
        public string? Ip { get; set; }
        public int? Intdate { get; set; }
        public DateTime? Datein { get; set; }
        public string? UserAgent { get; set; }
    }
}
