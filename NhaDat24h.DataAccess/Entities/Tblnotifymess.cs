using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblnotifymess
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime? Time { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Link { get; set; }
        public string? Idu { get; set; }
    }
}
