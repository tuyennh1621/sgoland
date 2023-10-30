using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblWebOneClick
    {
        public int Id { get; set; }
        public int? Idwo { get; set; }
        public int? Ngansach { get; set; }
        public string? Title { get; set; }
        public DateTime? Ngaychay { get; set; }
        public byte? Tinhtrang { get; set; }
        public int? Click { get; set; }
        public string? Mota { get; set; }
        public string? Image { get; set; }
    }
}
