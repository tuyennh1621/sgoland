using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblVnptbanking
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? BankId { get; set; }
        public string? Amount { get; set; }
        public string? TransId { get; set; }
        public DateTime? Datein { get; set; }
        public string? Status { get; set; }
        public string? KetQua { get; set; }
        public string? Transref { get; set; }
    }
}
