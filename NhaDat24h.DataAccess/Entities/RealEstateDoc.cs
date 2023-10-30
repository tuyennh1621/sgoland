using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class RealEstateDoc
    {
        public int Id { get; set; }
        public int IdDocument { get; set; }
        public int IdRealEstate { get; set; }

        public virtual Document IdDocumentNavigation { get; set; } = null!;
        public virtual RealEstate IdRealEstateNavigation { get; set; } = null!;
    }
}
