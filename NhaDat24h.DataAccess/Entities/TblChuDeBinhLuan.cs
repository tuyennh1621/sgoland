using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblChuDeBinhLuan
    {
        public int Id { get; set; }
        public string? Img { get; set; }
        public string? ChuDe { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
