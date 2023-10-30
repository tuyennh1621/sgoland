using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TblNhaReferId
    {
        public int Id { get; set; }
        public int? IdNhadat24h { get; set; }
        public int? IdRefer { get; set; }
        public int? IdWeb { get; set; }
    }
}
