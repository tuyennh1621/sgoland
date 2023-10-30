using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tbltemptinvip
    {
        public int Id { get; set; }
        public int? IdN { get; set; }
        public DateTime? Enddate { get; set; }
        public byte? IsKhuyenmai { get; set; }
        public byte? Tinvip { get; set; }
    }
}
