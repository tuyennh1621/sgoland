using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblModule
    {
        public int Id { get; set; }
        public string? ModuleName { get; set; }
        public string? LinkControl { get; set; }
    }
}
