using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class VNumSm
    {
        public DateTime? Ngaydang { get; set; }
        public int IdN { get; set; }
        public int? NumSms { get; set; }
        public byte? Smstrue { get; set; }
    }
}
