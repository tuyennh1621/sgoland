using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblInfoTv
    {
        public int Id { get; set; }
        public int? Idn { get; set; }
        public string? StrLinkVideo { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
