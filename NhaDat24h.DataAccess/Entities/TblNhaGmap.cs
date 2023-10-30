using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNhaGmap
    {
        public int Id { get; set; }
        public string? Idn { get; set; }
        public string? Lat { get; set; }
        public string? Lon { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
