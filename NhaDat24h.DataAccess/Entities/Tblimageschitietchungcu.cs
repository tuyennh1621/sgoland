using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblimageschitietchungcu
    {
        public int Id { get; set; }
        public int? Idchitietcanho { get; set; }
        public int? Idcanho { get; set; }
        public string? Titile { get; set; }
        public string? Thumbnai { get; set; }
        public byte? Stt { get; set; }
    }
}
