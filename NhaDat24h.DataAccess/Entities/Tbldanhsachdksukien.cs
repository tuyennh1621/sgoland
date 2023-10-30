using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tbldanhsachdksukien
    {
        public int Id { get; set; }
        public int? Idsukien { get; set; }
        public int? Idu { get; set; }
        public string? Hoten { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public DateTime? Datein { get; set; }
    }
}
