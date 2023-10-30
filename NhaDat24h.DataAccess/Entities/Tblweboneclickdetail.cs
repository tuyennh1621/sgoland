using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblweboneclickdetail
    {
        public int Id { get; set; }
        public int? Idwoclick { get; set; }
        public string? Ip { get; set; }
        public DateTime? Datetime { get; set; }
        public string? Url { get; set; }
        public int? Day { get; set; }
    }
}
