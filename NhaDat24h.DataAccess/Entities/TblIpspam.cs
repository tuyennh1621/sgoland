using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblIpspam
    {
        public int Id { get; set; }
        public string? Ip { get; set; }
        public DateTime? Date { get; set; }
    }
}
