using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblBlockIdforInvest
    {
        public int Id { get; set; }
        public int? MyId { get; set; }
        public int? BlockId { get; set; }
        public DateTime? Time { get; set; }
    }
}
