using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class TbLoainha
    {
        public int IdLn { get; set; }
        public string? Tenln { get; set; }
        public int? Stt { get; set; }
        public bool SelectedType(int value)
        {
            if (IdLn==value)
                return true;
            else
                return false;
        }

        public int IdType { get; set; }
    }
}
