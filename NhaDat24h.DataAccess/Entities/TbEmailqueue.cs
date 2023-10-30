using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbEmailqueue
    {
        public string Id { get; set; } = null!;
        public string? FromEmail { get; set; }
        public string? ToEmail { get; set; }
        public string? Body { get; set; }
        public string? Sub { get; set; }
        public DateTime? Date { get; set; }
        public byte? Send { get; set; }
    }
}
