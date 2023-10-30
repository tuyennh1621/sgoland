using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblvideoduan
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Linkvideo { get; set; }
        public int? IduanUser { get; set; }
        public int? IdKv { get; set; }
    }
}
