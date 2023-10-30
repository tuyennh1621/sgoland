using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblModulePage
    {
        public int Id { get; set; }
        public int? Idp { get; set; }
        public int? Idm { get; set; }
        public int? Position { get; set; }
        public int? Stt { get; set; }
        public string? Description { get; set; }
    }
}
