using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblimagegallery
    {
        public int Id { get; set; }
        public int? IdG { get; set; }
        public int? IdSg { get; set; }
        public string? Title { get; set; }
        public string? Linkthumbnai { get; set; }
    }
}
