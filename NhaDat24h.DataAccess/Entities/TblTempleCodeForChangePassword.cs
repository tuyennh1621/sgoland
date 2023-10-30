using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblTempleCodeForChangePassword
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string TempCode { get; set; } = null!;
        public int? Status { get; set; }
        public DateTime? Datein { get; set; }
    }
}
