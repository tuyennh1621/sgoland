using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblSmsActionDetail
    {
        public int Id { get; set; }
        public int? ActionId { get; set; }
        public int? Idn { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
