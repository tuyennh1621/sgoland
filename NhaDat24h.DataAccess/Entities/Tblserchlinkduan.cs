using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblserchlinkduan
    {
        public int Id { get; set; }
        public int? Idkv { get; set; }
        public int? Idchungcu { get; set; }
        public int? Inttang { get; set; }
        public decimal? Dientich { get; set; }
        public int? Intphongngu { get; set; }
        public int? Intphongwc { get; set; }
        public int? Inthuongbc { get; set; }
        public int? Inthuongcuavao { get; set; }
        public DateTime Date { get; set; }
        public string? Title { get; set; }
        public int? Click { get; set; }
    }
}
