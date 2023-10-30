using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbluserDatmua
    {
        public int Id { get; set; }
        public int? Iddatmua { get; set; }
        public int? Iduserdatmua { get; set; }
        public DateTime? Time { get; set; }
    }
}
