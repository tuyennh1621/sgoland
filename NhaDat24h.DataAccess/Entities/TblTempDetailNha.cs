using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblTempDetailNha
    {
        public int Id { get; set; }
        public int? IntDate { get; set; }
        public int? IdN { get; set; }
        public string? Navmenu { get; set; }
        public string? Detail { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
