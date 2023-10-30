using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tbldownloadtailieu
    {
        public int Id { get; set; }
        public int? Idduan { get; set; }
        public int? Idkhuvuc { get; set; }
        public int? IdduanUser { get; set; }
        public string? Tentailieu { get; set; }
        public string? Link { get; set; }
    }
}
