using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNhanVien
    {
        public int Id { get; set; }
        public int? IdU { get; set; }
        public int? Level { get; set; }
        public DateTime? Namsinh { get; set; }
        public DateTime? Datein { get; set; }
    }
}
