using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblMobileCode
    {
        public int Id { get; set; }
        public string? Mobile { get; set; }
        public string? Code { get; set; }
        public DateTime? Datein { get; set; }
        public string? Ip { get; set; }
        public string? Ssid { get; set; }
    }
}
