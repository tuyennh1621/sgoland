using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tbllinkseo
    {
        public int Id { get; set; }
        public string? Keyword { get; set; }
        public string? Link { get; set; }
        public int? Idquan { get; set; }
        public int? Idkhuvuc { get; set; }
    }
}
