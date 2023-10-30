using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Getalltinvip
    {
        public int IdN { get; set; }
        public int? Idu { get; set; }
        public string? Header { get; set; }
        public string? TinVip { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Lefhour { get; set; }
    }
}
