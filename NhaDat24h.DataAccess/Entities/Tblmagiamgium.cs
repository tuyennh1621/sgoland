using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblmagiamgium
    {
        public int Id { get; set; }
        public int? IdTt { get; set; }
        public int? IdQh { get; set; }
        public int? IdKv { get; set; }
        public double? Discount { get; set; }
        public string? Code { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Ids { get; set; }
    }
}
