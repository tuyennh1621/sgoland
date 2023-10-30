using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class RewriteTable
    {
        public string OriginalUrl { get; set; } = null!;
        public string NewUrl { get; set; } = null!;
        public int Id { get; set; }
        public int? WoId { get; set; }
    }
}
