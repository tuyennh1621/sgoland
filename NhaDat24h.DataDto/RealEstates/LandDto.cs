
using Microsoft.AspNetCore.Http;

namespace NhaDat24h.DataDto.RealEstates
{
    public class LandAddFormDto
    {
        public int IdUser { get; set; }
        public string Name { get; set; } = null!;
        public int IdType { get; set; }
        public int IdProvince { get; set; }
        public int IdDistrict { get; set; }
        public int IdWard { get; set; }
        public int IdStreet { get; set; }
        public string Address { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public string? ImplementCompany { get; set; }
        public string? Manager { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<IFormFile> AnhBDS { get; set; }
        public List<IFormFile> HopDongMoiGioi { get; set; }
        public List<IFormFile> GiayChungNhanPhanPhoi { get; set; }
        public List<IFormFile> PhapLyDuAn { get; set; }
        public List<IFormFile> MatBangDuAn { get; set; }
        public List<IFormFile> TaiLieuMKT { get; set; }
        public List<IFormFile> FileTraining { get; set; }
        public List<IFormFile> TVCDuAn { get; set; }
        public List<IFormFile> QandA { get; set; }
        public List<IFormFile> QuyTrinhGiaoDich { get; set; }
        public List<IFormFile> MauHopDongDatCoc { get; set; }
        public List<IFormFile> MauHĐCDTKiVoiKhachHang { get; set; }
        public List<IFormFile> MauThongTinKhachHangBookKy { get; set; }
        public List<IFormFile> PhieuTinhGia { get; set; }
        public List<string?> BangHang { get; set; }
    }

    public class AddNewLandDto
    {
        public List<IFormFile> AnhBDS { get; set; }
        public List<IFormFile> AnhSD { get; set; }
        public int IdR { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; } = null!;
        public int IdType { get; set; }
        public int IdProvince { get; set; }
        public int IdDistrict { get; set; }
        public int IdWard { get; set; }
        public int IdStreet { get; set; }
        public string Address { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public string? ImplementCompany { get; set; }
        public string? Manager { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        
        public int cbodvGiaLo
        {
            get; set;
        }

        public int outcbodvGiaLo
        {
            get
            {
                if (OfferPrice < 1000)
                    return 1;
                else
                    return 1000;
            }
        }

        public decimal? OfferPrice { get; set; }

        public decimal? Priceperm2
        {
            get
            {
                if (S != null && S > 0 && OfferPrice != null && OfferPrice > 0)
                {
                    return Math.Round((decimal)(OfferPrice / S), 2);
                }
                else
                    return null;
            }
        }
        public decimal? outOfferPrice
        {
            get
            {
                if (S != null && S > 0 && OfferPrice != null && OfferPrice > 0)
                {
                    if (OfferPrice < 1000)
                    {
                        return OfferPrice;
                    }
                    else
                    {
                        return Math.Round((decimal)(OfferPrice / 1000), 2);
                    }
                    
                }
                else
                {
                    return null;
                }
                    
            }
        }
        public int? LastPrice { get; set; }
        public decimal? S { get; set; }

        public string? Detail { get; set; }
        public decimal? FrontLenght { get; set; }
        public decimal? Lenght { get; set; }
        public decimal? EntranceLenght { get; set; }
        public byte? NumberFloor { get; set; }
        public byte? NumberBedRoom { get; set; }
        public byte? NumberWc { get; set; }
        public byte? NumberBalcony { get; set; }
        public decimal? BonusPercent { get; set; }
        public int? BonusMoney { get; set; }
        public int? DirectionHouse { get; set; }
        public int? Source { get; set; }
        public string? SourcePhone { get; set; }
        public string? LinkFb { get; set; }
        public string? SellerPhone { get; set; }

        public string? OwnerName { get; set; }
        public string? OwnerCCCD { get; set; }


        public List<DocumentDto>? listAnhBDS { get; set; }
		public List<int>? SelectedContactList { get; set; }
        public List<DocumentDto> listAnhSD { get; set; }
	}



    public class DocFileUpload
    {
        public int IdUser { get; set; }
        public string NamePj { get; set; }
        public int IdRE { get; set; }
        public string[] AllowedExtensions { get; set; }
        public bool IsLimited { get; set; }

        public List<CountDocType> listDoc
        {
            get;
            set;
        }

        public List<DocumentDto> listLinkbanghang
        {
            get;
            set;
        }
    }
    public class REIndexDto
    {
        public SearchListREParam SearchParam { get; set; }
        public List<RealEstateDto> ListRE { get; set; }
    }
}
