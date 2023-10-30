using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblVnptcard
    {
        public string? LoaiThe { get; set; }
        public string? Serial { get; set; }
        public string? Mathe { get; set; }
        public string? Sotien { get; set; }
        public DateTime? Datein { get; set; }
        public string? TransId { get; set; }
        public string? TargetId { get; set; }
        public int Id { get; set; }
        public string? Status { get; set; }
    }
}
