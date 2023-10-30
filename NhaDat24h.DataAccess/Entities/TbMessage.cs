using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbMessage
    {
        public int IdM { get; set; }
        public string? Content { get; set; }
        public int Style { get; set; }
    }
}
