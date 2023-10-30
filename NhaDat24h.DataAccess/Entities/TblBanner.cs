using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblBanner
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public int? Stt { get; set; }
        public int? Click { get; set; }
        public int? LangId { get; set; }
        public int? Position { get; set; }
        public int? GroupId { get; set; }
        public string? Image { get; set; }
    }
}
