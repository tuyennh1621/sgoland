using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblVitriBanner
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
