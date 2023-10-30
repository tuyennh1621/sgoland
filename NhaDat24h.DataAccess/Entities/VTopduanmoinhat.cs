using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class VTopduanmoinhat
    {
        public int Id { get; set; }
        public string? Tenduan { get; set; }
        public string? Tukhoa { get; set; }
        public string? Mota { get; set; }
        public string? Image { get; set; }
        public byte? IntHot { get; set; }
    }
}
