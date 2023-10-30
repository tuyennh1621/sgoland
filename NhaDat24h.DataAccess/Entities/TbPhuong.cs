using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbPhuong
    {
        public int Id { get; set; }
        public string? Tenphuong { get; set; }
        public int? IdQuan { get; set; }
        public byte? Stt { get; set; }
        public string? Tenphuong2 { get; set; }
    }

}
