using Microsoft.AspNetCore.Http;
using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;

namespace NhaDat24h.DataDto.Users
{
    public class SetAuthorParam
    {
        public int UserId { get; set; }
        public int Usertype { get; set; }
    }
    public class CheckPermisionParam
    {
        public int[] Permission { get; set; }
        public int IdUser { get; set; }
    }
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public class UserInsertDataDto
    {
        public int Id { get; set; }
        public int IdUserRequest { get; set; }
        public string IdU { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public int? IdTt { get; set; }
        public bool? Kh { get; set; }
        public string? Weburl { get; set; }
        public byte? Doituong { get; set; }
        public string? Address { get; set; }
        public string? NickYahoo { get; set; }
        public string? Ip { get; set; }
        public string? NickSkype { get; set; }
        public int? GioiTinh { get; set; }
        public string? Avatar { get; set; }
        public DateTime? Dateupdateavatar { get; set; }

    }

    public class FormCTV
    {
        public IFormFile filesAvatar1 { get; set; }
        public IFormFile filesAvatar2 { get; set; }
        public IFormFile filesCV { get; set; }
        public IFormFile filesCMT1 { get; set; }
        public IFormFile filesCMT2 { get; set; }
        public string Address { get; set; }
        public string? GIOITHIEU { get; set; }
        public string Mobile { get; set; }
        public int? GioiTinh { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CCCD { get; set; }
        public string Fullname { get; set; }
        public int? Refid { get; set; }
        public int? IdCongty { get; set; }
        public string Avatar1 { get; set; }
        public string Avatar2 { get; set; }
        public string FrontIDImg { get; set; }
        public string BackIDImg { get; set; }
        public List<CompanyDto>? ListCongty { get; set; }
        public List<PositionDepartmentDto>? ListPhongBan { get; set; }
        public int? DepartmentId { get; set; }
        public string? NgaySinh { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public bool SelectedCompany(int value)
        {
            if (value == IdCongty)
                return true;
            else
                return false;
        }

    }

    public partial class MulltiKeyValue
    {
        public string Id { get; set; }
        public string? Title { get; set; }
    }

    public partial class MultiSelectGroup
    {
        public string? Label { get; set; }
        public List<MulltiKeyValue>? Options { get; set; }
    }

    public class WardsDto
    {
        public int Id { get; set; }
        public string? Tenphuong { get; set; }
        public string? TenQuan { get; set; }
        public int? IdQuan { get; set; }
        public byte? Stt { get; set; }
        public string? Tenphuong2 { get; set; }
    }

    public class UpdateAvatarUserDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
    public class InsertUserOutputDto
    {
        public int Id { get; set; }
        public string IdU { get; set; }
    }
    public class CheckRegisterOutput
    {
        public int Status { get; set; }
        public int Id { get; set; }

    }
    public class UserSearchOutputDto
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public string? GIOITHIEU { get; set; }
        public string? Position { get; set; }
        public DateTime? DateIn { get; set; }
        public List<string> PositionName { get; set; } = new List<string>();

        public string? Mobile { get; set; }
        public byte? Status { get; set; }
        public string? FrontImgId { get; set; }
        public string? BackImgId { get; set; }
        public string? CvUrl { get; set; }
        public string? Avatar2 { get; set; }
        public string? DateOfBirth { get; set; }
        public int? RefId { get; set; }
        public string? NumberId { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int? Sex { get; set; }
        public string? ExpectedPosition { get; set; }
        public DateTime? LastVisited { get; set; }
        public string? CtvPermission
        {
            get
            {
                var pms = "";
                if (Permissions.Contains(5))
                {
                     pms+="Đăng dự án; ";
                }

                if (Permissions.Contains(6))
                {
                    pms+="Duyệt dự án; ";
                }
                if (Permissions.Contains(9))
                {
                    pms+="Đăng thổ Cư; ";
                }
                if (Permissions.Contains(10))
                {
                    pms+="Thêm video; ";
                }
                if (Permissions.Contains(11))
                {
                    pms+="Sửa video; ";
                }
                return pms;
            }
        }
        public List<int>? Permissions { get; set; } = new List<int>();
        public string strLastVisited
        {
            get
            {
                if (LastVisited != null)
                {
                    DateTime startDate = DateTime.Now;
                    TimeSpan t = startDate - (DateTime)LastVisited;

                    if (t.Days <= 0)
                    {
                        if (t.Hours < 1)
                        {
                            if (t.Minutes <= 2)
                            {
                                return "Vừa xong";
                            }
                            else
                            {
                                return t.Minutes + " phút trước.";
                            }

                        }
                        else
                        {
                            return t.Hours + " giờ trước.";
                        }
                    }
                    else
                    {
                        return t.Days + " ngày trước.";
                    }
                }
                else
                {
                    return "";
                }

            }
        }
        public bool? IsOnline { get; set; }
        public bool? reIsOnline
        {
            get
            {
                if (strLastVisited == "Vừa xong")
                {
                    return true;
                }
                else
                {
                    return IsOnline;
                }
            }
        }
        public bool Selected(byte value)
        {
            if (value == Status)
                return true;
            else
                return false;
        }
        public bool DepartmentSelected(decimal value)
        {
            if (value == DepartmentId.Min())
                return true;
            else
                return false;
        }
        public bool Apply { get; set; }
        public List<int>? PositionId { get; set; } = new List<int>();
        public List<int>? CompanyId { get; set; } = new List<int>();
        public List<int>? DepartmentId { get; set; } = new List<int>();

        public List<string> DepartmentName { get; set; } = new List<string>();
        public List<string> CompanyName { get; set; } = new List<string>();
        public List<int>? PoDepId { get; set; } = new List<int>();

    }
    public class ModelListCtv
    {
        public List<UserSearchOutputDto> ListCtv { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public List<int>? Permission { get; set; }
        public int IdCongty { get; set; }
        public List<DepartmentCtvDto> ListDepartment { get; set; }
        public List<CompanyDto>? ListCompany { get; set; }
        public bool isAdmin { get; set; }
        public bool SelectedCompany(int value)
        {
            if (value == IdCongty)
                return true;
            else
                return false;
        }
    }

    public class ModelFormPheDuyet
    {
        public int IdCtv { get; set; }
        public List<CompanyDto>? ListCongTy { get; set; }
        public List<int>? ListIDCongTySelected { get; set; }

        public List<MultiSelectGroup>? ListPhongBan { get; set; }
        public List<int>? ListIDPhongBanSelected { get; set; }

        public List<MultiSelectGroup>? ListVitri { get; set; }
        public List<int>? ListIDViTriSelected { get; set; }
    }

    public class CardUser
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
    }

    public class ChangeOnOffUser
    {
        public int IdUser { get; set; }
        public bool OnOff { get; set; }
    }

    public class UpdatePasswordUserParam
    {
        public int IdUser { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }

    }
    public class SetPermisionParam
    {
        public List<int> Permission { get; set; } = new List<int>();
        public int IdUser { get; set; }
        public int IdCtv { get; set; }
    }
    public class SearchCtvParam
    {
        public int idUser { get; set; }
        public int? idctv { get; set; }
        public string? searchkey { get; set; }
        public int? status { get; set; }
        public int? idCompany { get; set; }
        public int? idDepartment { get; set; }
        public int? numdayoff { get; set; }
    }
}
