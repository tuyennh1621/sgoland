using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblchitietchungcu
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Descrip { get; set; }
        public int? IdKv { get; set; }
        public int? IdChungcu { get; set; }
        public int? Tang { get; set; }
        public decimal? Giagoc { get; set; }
        public decimal? Chenh { get; set; }
        public decimal? Hoahong { get; set; }
        public int? Idu { get; set; }
        public DateTime? Datein { get; set; }
        public byte? Status { get; set; }
        public int? Stt { get; set; }
        public int? IdduanUser { get; set; }
        public byte? IdLoaitin { get; set; }
        public byte? IsDatCoc { get; set; }
    }
}
