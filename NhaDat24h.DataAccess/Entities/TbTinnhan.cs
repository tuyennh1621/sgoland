using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbTinnhan
    {
        public int IdTn { get; set; }
        public string? Subject { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Message { get; set; }
        public string? EmailTo { get; set; }
        public string? EmailFrom { get; set; }
        public DateTime? Date { get; set; }
    }
}
