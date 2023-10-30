namespace NhaDat24h.DataDto.RealEstates
{
    public class REPjInsertDto
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

        public decimal? OfferPrice { get; set; }
        public decimal? S { get; set; }
        public string? Detail { get; set; }
        public List<int>? SelectedContactList { get; set; }
    }
    public class REReInsertDto
    {
        public int IdUser { get; set; }
        public string Name { get; set; } = null!;
        public int IdType { get; set; }
        public int IdProvince { get; set; }
        public int IdDistrict { get; set; }
        public int IdWard { get; set; }
        public int? IdStreet { get; set; }
        public string Address { get; set; } = null!;
        public string? ImplementCompany { get; set; }
        public string? Manager { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public decimal? S { get; set; }
        public decimal? FrontLenght { get; set; }
        public decimal? Lenght { get; set; }
        public decimal? EntranceLenght { get; set; }
        public byte? NumberFloor { get; set; }
        public byte? NumberBedRoom { get; set; }
        public byte? NumberWc { get; set; }
        public byte? NumberBalcony { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? LastPrice { get; set; }
        public decimal? BonusPercent { get; set; }
        public int? BonusMoney { get; set; }
        public int? DirectionHouse { get; set; }
        public int? Source { get; set; }
        public string? SourcePhone { get; set; }
        public string? LinkFb { get; set; }
        public string? SellerPhone { get; set; }
        public string? Detail { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerCCCD { get; set; }
    }

    public class DocShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
    }


    public class RealEstateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Detail { get; set; }
        public byte Status { get; set; }
        public decimal? BonusPercent { get; set; }
        public byte? Style { get; set; }
        public int IdUser { get; set; }
        public int? BonusMoney { get; set; }
        public string? Avatar { get; set; }
        public string? Address { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Wards { get; set; }
        public string? Phone { get; set; }
        public string? ImplementCompany { get; set; }
        public string? Manager { get; set; }
        public int? OfferPrice { get; set; }
        public string? Description { get; set; }
        public string? StringOfferPrice
        {
            get
            {
                if (OfferPrice <= 1000 || OfferPrice == null)
                {
                    return OfferPrice + " Tr";
                }
                else
                {
                    decimal newOff = OfferPrice ?? 0;
                    string temp = Math.Round(newOff / 1000, 2).ToString("G29");
                    return temp + " Tỷ";
                }

            }
        }
        public string? Uimage { get; set; }
        public int? LastPrice { get; set; }
        public decimal? S { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Total { get; set; }
        public List<DocShortDto>? BangHang { get; set; }

        public int? IdType { get; set; }

        public bool SelectedStatus(byte value)
        {
            if (value == Status)
                return true;
            else
                return false;
        }
        public bool isYeuthich { get; set; }

        public int CountView { get; set; }
        public DateTime DateCreate { get; set; }

        public string Nametype()
        { 
            if (IdType == 1)
                return "Căn hộ chung cư";

            if (IdType == 2)
                return "Nhà biệt thự, liền kề";

            if (IdType == 5)
                return "Nhà riêng";

            if (IdType == 6)
                return "Đất nền dự án";
            if (IdType == 8)
                return "Bán đất";

            if (IdType == 10)
                return "Nhà mặt phố";

            if (IdType == 16)
                return "Trang trại, Khu nghỉ dưỡng";
            if (IdType == 17)
                return "Kho nhà xưởng";


            if (IdType == 20)
                return "Nhà hàng, khách sạn, resort";

            if (IdType == 21)
                return "ShopHouse";

            if (IdType == 22)
                return "Condotel";

            if (IdType == 24)
                return "Officetel";
            if (IdType == 26)
                return "Chung cư mini (BĐS dòng tiền)";

            if (IdType == 99)
                return "Nhà tập thể";

            return "";
        }
    }

    public class DocumentDto
    {
        public int Id { get; set; }
        public int IdType { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string? NameType { get; set; }
        public string? FileType { get; set; }
        public string FileSize { get; set; }
        public int IdUser { get; set; }
        public int IdClient { get; set; }

        public bool isShowAction
        {
            get
            {
                return IdUser == IdClient;

            }
        }
        public string? strFileSize
        {
            get
            {
                decimal _filesize = decimal.Parse(FileSize);
                if (_filesize < 1048576)
                {
                    return Math.Round((_filesize / 1024), 1) + " (K)";

                }
                else
                {
                    return Math.Round((_filesize / 1024000), 1) + " (M)";
                }

            }
        }
    }
    public class REDocDto
    {
        public List<DocumentDto> ListREDoc { get; set; }
        public int IdRE { get; set; }
        public int IdUserRequest { get; set; }
    }
    public class UploadREDocResponse
    {
        public List<string>? ListFail { get; set; }
        public string InsertDocResult { get; set; }
    }
    public class UploadDocResponse
    {
        public bool IsSuccess { get; set; }
        public int IdDoc { get; set; }
    }
    public class DocumentInsertDto
    {
        public int Id { get; set; }
        public int IdType { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int IdRE { get; set; }
        public int IdUserRequest { get; set; }

        public string FileType { get; set; }
        public decimal FileSize { get; set; }
    }

    public class RealEstateGetOneDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int IdType { get; set; }
        public string Type { get; set; }
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
        public int? IdStreet { get; set; }
        public decimal? S { get; set; }
        public decimal? FrontLenght { get; set; }
        public decimal? Lenght { get; set; }
        public decimal? EntranceLenght { get; set; }
        public byte? NumberFloor { get; set; }
        public byte? NumberBedRoom { get; set; }
        public byte? NumberWc { get; set; }
        public byte? NumberBalcony { get; set; }
        public int? OfferPrice { get; set; }
        public string? StringOfferPrice
        {
            get
            {
                if (OfferPrice <= 1000)
                {
                    return OfferPrice + "Tr";
                }
                else
                {
                    decimal newOff = OfferPrice ?? 0;
                    decimal temp = Math.Round(newOff / 1000, 2);
                    return temp + " Tỷ";
                }

            }
        }
        public int? LastPrice { get; set; }
        public decimal? BonusPercent { get; set; }
        public int? BonusMoney { get; set; }
        public int? DirectionHouse { get; set; }
        public string? DirectionHouseChar { get; set; }
        public int? Source { get; set; }
        public string? SourceChar { get; set; }

        public string? SourcePhone { get; set; }
        public string? LinkFb { get; set; }
        public string? SellerPhone { get; set; }
        public string? Detail { get; set; }
        public Dictionary<int, List<DocumentDto>> DictionaryDocs { get; set; }

        public List<DocumentDto>? ListDocuments { get; set; }
        public CtvInforDto? CtvInfor { get; set; }
        public List<CtvInforDto>? ListContactCtvRE { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerCCCD { get; set; }
    }
    public class CtvInforDto
    {
        public string? Avatar { get; set; }
        public string? Company { get; set; }
        public string? Mobile { get; set; }
        public string? Name { get; set; }
        public int Id { get; set; }
    }

    public class RealEstateUpdateDto
    {
        public string Name { get; set; } = null!;
        public int IdType { get; set; }
        public int IdProvince { get; set; }
        public int IdDistrict { get; set; }
        public int IdWard { get; set; }
        public string Address { get; set; } = null!;
        public string? ImplementCompany { get; set; }
        public string? Manager { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int IdUser { get; set; }
        public decimal? S { get; set; }
        public decimal? FrontLenght { get; set; }
        public decimal? Lenght { get; set; }
        public decimal? EntranceLenght { get; set; }
        public byte? NumberFloor { get; set; }
        public byte? NumberBedRoom { get; set; }
        public byte? NumberWc { get; set; }
        public byte? NumberBalcony { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? LastPrice { get; set; }
        public decimal? BonusPercent { get; set; }
        public int? BonusMoney { get; set; }
        public int? DirectionHouse { get; set; }
        public int? Source { get; set; }
        public string? SourcePhone { get; set; }
        public string? LinkFb { get; set; }
        public string? SellerPhone { get; set; }
        public string? Detail { get; set; }
        public List<int>? SelectedContactList { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerCCCD { get; set; }


    }
    public class UpdateREParam
    {
        public int IdRE { get; set; }
        public int IdUserRequest { get; set; }
        public RealEstateUpdateDto data { get; set; }
    }
    public class CountDocType
    {
        public int CountType { get; set; }
        public int IdType { get; set; }
    }
    public class SaveREInsertDto
    {
        public int IdRE { get; set; }
        public int IdCtv { get; set; }
        public int Type { get; set; }
    }
    public class SearchListREParam
    {
        public int IdUserRequest { get; set; }
        public int? IdProvince { get; set; }
        public List<int>? IdDistrict { get; set; }
        public int? Status { get; set; }
        public List<int>? IdWards { get; set; }
        public string? Address { get; set; }
        public string? SearchKey { get; set; }
        public int? IdType { get; set; }
        public int? minPrice { get; set; }
        public int? maxPrice { get; set; }
        public decimal? minArg { get; set; }
        public decimal? maxArg { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public byte Style { get; set; }
        public bool SelectedProvince(int value)
        {
            if (value == IdProvince)
                return true;
            else
                return false;
        }
        public bool SelectedDistrict(int value)
        {
            if (IdDistrict.Contains(value))
                return true;
            else
                return false;
        }
        public bool SelectedWards(int value)
        {
            if (IdWards.Contains(value))
                return true;
            else
                return false;
        }
        public bool SelectedType(int value)
        {
            if (value == IdType)
                return true;
            else
                return false;
        }
    }

    public class ModelListRE
    {
        public SearchListREParam Param { get; set; }
        public List<ProvinceDto> Provinces { get; set; }

        public List<RealEstateDto> realEstateDtos { get; set; }
        public List<int>? Permission { get; set; }
        public int IdCtv { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
    public class UpdateStatusREParam
    {
        public int IdUserRequest { get; set; }
        public int IdRE { get; set; }
        public byte Status { get; set; }
        public string? Description { get; set; }
    }

    public class SearchForm
    {
        public string? SearchKey { get; set; }
        public byte Style { get; set; }
        public List<int> Permission { get; set; }
        public List<ProvinceDto> Provinces { get; set; }
    }
    public class REReportDto
    {
        public int TotalRE { get; set; }
        public int NumTransaction { get; set; }
        public int TotalValue { get; set; }
        public Dictionary<DateTime, TransactionByMonth> Statistics { get; set; }
    }
    public class TransactionByMonth
    {
        public int Count { get; set; }
        public int Money { get; set; }
    }
    public class ContactCtvREInsertDto
    {
        public int IdRE { get; set; }
        public int IdCtv { get; set; }
        public int IdUserRequest { get; set; }
    }
}
