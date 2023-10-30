using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblduanUserPhanphoi
    {
        public int Id { get; set; }
        public int Idu { get; set; }
        public int Idduan { get; set; }
        public DateTime? Date { get; set; }
        public int? Stt { get; set; }
    }
}
