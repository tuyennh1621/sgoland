using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblGroupUser
    {
        public int Id { get; set; }
        public int? Idgroup { get; set; }
        public string? Name { get; set; }
    }
}
