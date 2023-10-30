using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblvmgpayLog
    {
        public int TraceId { get; set; }
        public int? Uid { get; set; }
        public double? Price { get; set; }
        public byte? Status { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateOut { get; set; }
    }
}
