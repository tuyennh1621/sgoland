using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbKv
    {
        public int IdKv { get; set; }
        public int? IdQ { get; set; }
        public string? Tenkv { get; set; }
        public int? Stt { get; set; }
        public string? Diengiai { get; set; }
        public byte? Active { get; set; }
        public int? IdLoainha { get; set; }
        public int? NumBan { get; set; }
        public int? NumChothue { get; set; }
        public int? NumCanMua { get; set; }
        public int? IdPhuong { get; set; }
        public string? Tenkv2 { get; set; }
    }
}
