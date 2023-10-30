using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblchudautu
    {
        public int Id { get; set; }
        public string? Ten { get; set; }
        public string? Diachi { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }
        public string? Lat { get; set; }
        public string? Lon { get; set; }
        public int? Intgroup { get; set; }
        public int? Iduser { get; set; }
        public int? Idtt { get; set; }
        public int? Idqh { get; set; }
        public int? Idkv { get; set; }
    }
}
