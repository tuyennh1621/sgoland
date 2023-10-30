using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblSm
    {
        public int Id { get; set; }
        public string? MobileNumber { get; set; }
        public string? ServiceNumber { get; set; }
        public string? Smsid { get; set; }
        public string? Message { get; set; }
        public DateTime? DateIn { get; set; }
        public int? Status { get; set; }
        public string? StrService { get; set; }
        public string? OutPut { get; set; }
        public string? Smsc { get; set; }
    }
}
