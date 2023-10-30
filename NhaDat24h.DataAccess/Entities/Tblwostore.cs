using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblwostore
    {
        public int Id { get; set; }
        public int? Idwo { get; set; }
        public int? Giaban { get; set; }
        public int? Giachothue { get; set; }
        public byte? Status { get; set; }
        public string? Mota { get; set; }
    }
}
