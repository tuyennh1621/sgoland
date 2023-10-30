using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbReply
    {
        public string IdR { get; set; } = null!;
        public string? Reply { get; set; }
        public string? IdN { get; set; }
        public string? IdU { get; set; }
        public DateTime? Ngaydang { get; set; }
    }
}
