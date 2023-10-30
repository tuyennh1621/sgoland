using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Dtproperty1
    {
        public int Id { get; set; }
        public int? Objectid { get; set; }
        public string Property { get; set; } = null!;
        public string? Value { get; set; }
        public string? Uvalue { get; set; }
        public byte[]? Lvalue { get; set; }
        public int? Version { get; set; }
    }
}
