using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbNhomtin
    {
        public int IdNt { get; set; }
        public string? Tennt { get; set; }
        public int? IdTt { get; set; }
        public int? IdDuan { get; set; }
        public int? IdDuanUser { get; set; }
        public int? ParentId { get; set; }
        public int? IdKv { get; set; }
    }
}
