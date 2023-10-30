using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Tblslider
    {
        public int Id { get; set; }
        public int? IdduanUser { get; set; }
        public string? Linkimage { get; set; }
        public string? Title { get; set; }
        public string? Content1 { get; set; }
        public string? Content2 { get; set; }
        public int? Bed { get; set; }
        public int? Bat { get; set; }
        public string? Link { get; set; }
        public byte? Position { get; set; }
        public decimal? Dientich { get; set; }
        public byte? Gara { get; set; }
        public string? Tonggiatien { get; set; }
        public string? Captionbutton { get; set; }
    }
}
