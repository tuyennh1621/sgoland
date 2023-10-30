using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class Document
    {
        public Document()
        {
            RealEstateDocs = new HashSet<RealEstateDoc>();
        }

        public int Id { get; set; }
        public int IdType { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool Deleted { get; set; }

        public virtual DocumentType IdTypeNavigation { get; set; } = null!;
        public virtual ICollection<RealEstateDoc> RealEstateDocs { get; set; }
    }
}
