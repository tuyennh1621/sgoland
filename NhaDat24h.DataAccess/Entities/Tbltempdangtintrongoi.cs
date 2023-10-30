using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tbltempdangtintrongoi
    {
        public int Id { get; set; }
        public int? Idu { get; set; }
        public DateTime? Enddate { get; set; }
        public string? Status { get; set; }
        public DateTime? Datein { get; set; }
    }
}
