using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblcitySelected
    {
        public int Id { get; set; }
        public int? Stt { get; set; }
        public int? IdTt { get; set; }
        public string? TenTt { get; set; }
        public string? Link { get; set; }
    }
}
