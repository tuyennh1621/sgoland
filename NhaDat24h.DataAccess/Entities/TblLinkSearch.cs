using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblLinkSearch
    {
        public int Id { get; set; }
        public int? IdTt { get; set; }
        public int? IdQh { get; set; }
        public int? IdKv { get; set; }
        public string? Link { get; set; }
        public string? Title { get; set; }
        public int? Click { get; set; }
        public DateTime? Datein { get; set; }
        public byte? IntTag { get; set; }
        public int? IdLt { get; set; }
        public int? IdLn { get; set; }
        public int? IdChinhchu { get; set; }
        public string? Ip { get; set; }
        public string? ReferUrl { get; set; }
        public string? Title3 { get; set; }
        public int? NumRows { get; set; }
        public string? Title4 { get; set; }
        public byte? Active { get; set; }
        public string? Tenquan { get; set; }
        public string? Tentt { get; set; }
        public string? Tenkhuvuc { get; set; }
        public double? Fromgia { get; set; }
        public double? Togia { get; set; }
        public double? Fromdientich { get; set; }
        public double? Todientich { get; set; }
        public bool? Processed { get; set; }
        public string? Reurl { get; set; }
    }
}
