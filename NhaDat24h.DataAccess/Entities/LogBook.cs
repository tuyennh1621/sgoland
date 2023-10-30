using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class LogBook
    {
        public int Id { get; set; }
        public int IdUserRequest { get; set; }
        public string Action { get; set; } = null!;
        public string Detail { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public int? IdCtv { get; set; }
        public string? Description { get; set; }
    }
}
