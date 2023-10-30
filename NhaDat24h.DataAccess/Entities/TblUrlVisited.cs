using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblUrlVisited
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public int? Idu { get; set; }
        public DateTime? Time { get; set; }
        public int? IntStyle { get; set; }
    }
}
