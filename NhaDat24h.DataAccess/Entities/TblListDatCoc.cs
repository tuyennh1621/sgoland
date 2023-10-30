using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblListDatCoc
    {
        public int Id { get; set; }
        public DateTime? NgayDatCoc { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string? Vtbds { get; set; }
        public string? Ttchu { get; set; }
        public string? Ttkhach { get; set; }
        public string? GiaChuthu { get; set; }
        public string? GiaBan { get; set; }
        public double? LoiNhuan { get; set; }
        public string? DienGiai { get; set; }
        public string? NhanVien { get; set; }
        public int? Status { get; set; }
        public string? Idu { get; set; }
        public string? LinkFile { get; set; }
    }
}
