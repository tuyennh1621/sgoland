using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblWebsite
    {
        public int Id { get; set; }
        public string? Website { get; set; }
        public string? Connection { get; set; }
        public byte? ActiveIos { get; set; }
    }
}
