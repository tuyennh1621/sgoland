using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblSuKienLienMinhBd
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? IdTin { get; set; }
        public string? Tomtat { get; set; }
        public DateTime? Enddate { get; set; }
        public int? IdKv { get; set; }
        public string? Txtsms { get; set; }
        public string? TxtEmail { get; set; }
    }
}
