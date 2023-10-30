using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblKhoangGium
    {
        public int Id { get; set; }
        public int? IdLt { get; set; }
        public int? FromGia { get; set; }
        public int? ToGia { get; set; }
    }
}
