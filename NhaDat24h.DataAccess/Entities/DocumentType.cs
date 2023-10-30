using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class DocumentType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Stt { get; set; }
        public bool Deleted { get; set; }
    }
}
