using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblChungcu
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Thumbnai { get; set; }
        public int? SoTang { get; set; }
        public int? DienTichSan { get; set; }
        public int? IdKv { get; set; }
        public int? Stt { get; set; }
        public int? IdLoainha { get; set; }
    }
}
