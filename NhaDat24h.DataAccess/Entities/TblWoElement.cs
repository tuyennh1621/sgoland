using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblWoElement
    {
        public int Id { get; set; }
        public string? Element { get; set; }
        public string? Cssname { get; set; }
        public string? ThuocTinh { get; set; }
        public int? Inttheme { get; set; }
        public byte? Intthuoctinh { get; set; }
        public string? DefaultTheme { get; set; }
    }
}
