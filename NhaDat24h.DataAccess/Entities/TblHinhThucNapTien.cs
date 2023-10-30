using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblHinhThucNapTien
    {
        public int Id { get; set; }
        public string? HinhThuc { get; set; }
        public int? Stt { get; set; }
    }
}
