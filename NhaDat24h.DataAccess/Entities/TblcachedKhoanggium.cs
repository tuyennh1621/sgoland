using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblcachedKhoanggium
    {
        public int Id { get; set; }
        public int? IdTt { get; set; }
        public int? IdQh { get; set; }
        public int? IdKv { get; set; }
        public int? IdLn { get; set; }
        public string? Detail { get; set; }
        public DateTime? Data { get; set; }
        public int? Style { get; set; }
        public int? IdLt { get; set; }
    }
}
