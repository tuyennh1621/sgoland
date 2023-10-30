using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblliksearchDuan
    {
        public int Id { get; set; }
        public int? IdTt { get; set; }
        public string? Keyword { get; set; }
        public int? IntCount { get; set; }
    }
}
