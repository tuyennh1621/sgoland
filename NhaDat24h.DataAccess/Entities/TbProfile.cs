using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbProfile
    {
        public int IdPro { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
    }
}
