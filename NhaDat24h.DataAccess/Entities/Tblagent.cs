using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblagent
    {
        public int Id { get; set; }
        public string? Ten { get; set; }
        public string? Description { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? DuanUser { get; set; }
        public string? Avatar { get; set; }
    }
}
