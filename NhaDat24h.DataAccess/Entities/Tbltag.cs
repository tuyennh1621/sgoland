using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tbltag
    {
        public int Id { get; set; }
        public string? Nametag { get; set; }
        public string? Link { get; set; }
        public string? Captag { get; set; }
    }
}
