using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNgayLamViec
    {
        public int Id { get; set; }
        public DateTime? Ngay { get; set; }
        public int? Ca { get; set; }
    }
}
