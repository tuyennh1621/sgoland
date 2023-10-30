using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbEnter
    {
        public int IdE { get; set; }
        public string? Email { get; set; }
        public byte[]? Logo { get; set; }
        public byte[]? Banner { get; set; }
    }
}
