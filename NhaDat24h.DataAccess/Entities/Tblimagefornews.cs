using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblimagefornews
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? IdTin { get; set; }
    }
}
