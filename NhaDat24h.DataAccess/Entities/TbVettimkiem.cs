using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbVettimkiem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? IdTt { get; set; }
        public int? IdQ { get; set; }
        public int? IdKv { get; set; }
        public double? Fromdt { get; set; }
        public double? Todt { get; set; }
        public double? Fromgia { get; set; }
        public double? Togia { get; set; }
        public int? Dvdt { get; set; }
        public int? Dvfromgia { get; set; }
        public int? Dvtogia { get; set; }
        public string? IdU { get; set; }
        public int? IdLn { get; set; }
        public int? IdHn { get; set; }
        public int? IdLt { get; set; }
        public int? Numrc { get; set; }
        public bool? Andvip { get; set; }
        public int? Orderby { get; set; }
        public DateTime? Ngaydang { get; set; }
    }
}
