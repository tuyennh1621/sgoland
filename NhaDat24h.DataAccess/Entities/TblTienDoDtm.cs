using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblTienDoDtm
    {
        public int Id { get; set; }
        public int? IdKv { get; set; }
        public string? TienDo { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}
