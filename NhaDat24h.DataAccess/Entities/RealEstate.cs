using System;
using System.Collections.Generic;

namespace NhaDat24h.DataAccess.Entities
{
    public partial class RealEstate
    {
        public RealEstate()
        {
            RealEstateDocs = new HashSet<RealEstateDoc>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int IdType { get; set; }
        public int IdProvince { get; set; }
        public int IdDistrict { get; set; }
        public int IdWard { get; set; }
        public string Address { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public string? ImplementCompany { get; set; }
        public string? Manager { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public byte Status { get; set; }
        public int IdUser { get; set; }
        public DateTime DateCreate { get; set; }
        public int IdCompany { get; set; }
        public bool Deleted { get; set; }

        public virtual TblDanhSachCongTy IdCompanyNavigation { get; set; } = null!;
        public virtual TbQuan IdDistrictNavigation { get; set; } = null!;
        public virtual TbTinhthanh IdProvinceNavigation { get; set; } = null!;
        public virtual TbLoainha IdTypeNavigation { get; set; } = null!;
        public virtual Tblctvonline IdUserNavigation { get; set; } = null!;
        public virtual TbPhuong IdWardNavigation { get; set; } = null!;
        public virtual ICollection<RealEstateDoc> RealEstateDocs { get; set; }
    }
}
