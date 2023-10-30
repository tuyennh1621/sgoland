using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbllinksearchContent
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Content { get; set; }
    }
}
