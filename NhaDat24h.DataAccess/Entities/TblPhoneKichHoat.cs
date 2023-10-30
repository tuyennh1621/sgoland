using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblPhoneKichHoat
    {
        public int Id { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? Date { get; set; }
    }
}
