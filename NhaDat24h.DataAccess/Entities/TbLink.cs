using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbLink
    {
        public int IdLk { get; set; }
        public string? Img { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public int? Stt { get; set; }
        public int? Style { get; set; }
        public string? Email { get; set; }
    }
}
