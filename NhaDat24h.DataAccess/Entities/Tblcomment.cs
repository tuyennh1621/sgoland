using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblcomment
    {
        public int Id { get; set; }
        public int? Idu { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
        public int? Idtopic { get; set; }
        public int? ParentId { get; set; }
        public int? IdKv { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
