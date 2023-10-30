using Microsoft.AspNetCore.Http;
using NhaDat24h.DataDto.Company;

namespace NhaDat24h.DataDto.Ctv
{
    public class CtvInsertDataDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CCCD { get; set; }
        public string? Address { get; set; }
        public string? GIOITHIEU { get; set; }
        public string? Phone { get; set; }
        public string FrontImageId { get; set; }
        public string BackImageId { get; set; }
        public int? Refid { get; set; }
        public int? IdCongty { get; set; }
        public string CvUrl { get; set; }
        public string Avatar2 { get; set; }
        public int? DepartmentId { get; set; }
        public int? Ngay { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
    }
    public class UpdateImageIdCtvDto
    {
        public int Id { get; set; }
        public string UrlFront { get; set; }
        public string UrlBack { get; set; }
    }
    public class UpdateStatusCtvDto
    {
        public int IdUser { get; set; }
        public int IdCtv { get; set; }
        public byte Status { get; set; }
        public string? Description { get; set; }

    }
    public class UpdateDepartmentCtvDto
    {
        public int IdUser { get; set; }
        public int IdCtv { get; set; }
        public decimal IdDepartmentNew { get; set; }
        public decimal IdDepartmentOld { get; set; }
        public string? Description { get; set; }

    }
    public class DepartmentCtvDto
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public decimal? IdSuperiors { get; set; }
    }
    public class CtvEditDto
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdUserRequest { get; set; }
        public bool isAdmin { get; set; }
        public string? FullName { get; set; }
        public int? IdcongTy { get; set; }
        public string? Socmtnd { get; set; }
        public string? Ngaysinh { get; set; }
        public string? Diachi { get; set; }
        public string? Mobile { get; set; }
        public int? Refid { get; set; }
        public string? Frontimageid { get; set; }
        public string? Backimageid { get; set; }
        public byte Status { get; set; }
        public decimal DepartmentId { get; set; }
        public string? Avatar { get; set; }
        public string? Avatar2 { get; set; }
        public string? CvUrl { get; set; }
        public int? GioiTinh { get; set; }
        public string? Email { get; set; }
        public IFormFile? AvtEdit { get; set; }
        public IFormFile? Avt2Edit { get; set; }
        public IFormFile? CVEdit { get; set; }
        public IFormFile? FrontIdEdit { get; set; }
        public IFormFile? BackIdEdit { get; set; }
        public string? Gioithieu { get; set; }
        public string? FaceBook { get; set; }
        public string? Twitter { get; set; }
        public string? Position { get; set; }
        public bool SelectedCompany(int value)
        {
            if (value == IdcongTy)
                return true;
            else
                return false;
        }
        public List<CompanyDto>? ListCongty { get; set; }
    }
    public class HRReportDto
    {
        public int IdCompany { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Total { get; set; }
        public int NewRegister { get; set; }
        public int Online { get; set; }
        public int Offline3DaysAgo { get; set; }
        public Dictionary<DateTime, VolatilityHr> VolatilityByMonth { get; set; }
        public Dictionary<DateTime, RegisterHr> RegiterByMonth { get; set; }
    }
    public class VolatilityHr
    {
        public int StaffIn { get; set; }
        public int StaffOut { get; set; }
    }
    public class RegisterHr
    {
        public int Register { get; set; }
        public int Accepted { get; set; }
        public int Reject { get; set; }
    }
    public class CtvInCompanyDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int Total { get; set; }
    }
    public class CtvUpdateLonLatDataDto
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public int Id { get; set; }
    }
    public class CounterTimeOnlineDto
    {
        public int Id { get; set; }
        public int IdCtv { get; set; }
        public DateTime Date { get; set; }
        public int Counter { get; set; }
    }
    public class CtvTopTimeOnlineDto
    {
        public int Id { get; set; }
        public int Counter { get; set; }

    }

}
