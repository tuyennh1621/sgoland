using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblUserLink
    {
        public int Id { get; set; }
        public string? IdU { get; set; }
        public string? IdUlink { get; set; }
        public int? IntWebsite { get; set; }
        public string? Email { get; set; }
    }
}
