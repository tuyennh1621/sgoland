using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbLienhe
    {
        public int IdLh { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public DateTime? Date { get; set; }
        public string? Diachi { get; set; }
        public byte? Style { get; set; }
        public int? Idu { get; set; }
        public int? IdduanUser { get; set; }
    }
}
