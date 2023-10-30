using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class WeboneclickGetbyclickview
    {
        public int Id { get; set; }
        public int? Idduan { get; set; }
        public string? Title { get; set; }
        public string? Domain { get; set; }
        public string? OriginalUrl { get; set; }
        public int? Idu { get; set; }
        public string? Mota { get; set; }
        public string? Image { get; set; }
        public byte? IntTheme { get; set; }
    }
}
