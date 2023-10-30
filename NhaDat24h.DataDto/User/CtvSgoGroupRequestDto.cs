using NhaDat24h.DataDto.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaDat24h.DataDto.User
{
    public class CtvSgoGroupRequestDto
    {
        public int IdCompany { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Company { get; set; }
    }
    public class CtvSgoGroupDto
    {
        public int Id { get; set; }
        public int IdCompany { get; set; }

        public string ReferralCode { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string DateOfBirth { get; set; }
        public int Sex { get; set; }
        public string Gioitinh
        {
            get
            {
                if (Sex == 1) return "Nam";
                if (Sex == 2) return "Nữ";
                else return "Unisex";
            }
        }
        public string NumberId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Total { get; set; }
    }

    public class GetHrReportKey
    {
        public int IdCompany { get; set; }
        public string Company { get; set; }
    }
    public class GetHrReportData
    {
        public int i { get; set; }
        public GetHrReportKey Key { get; set; }
        public List<CtvSgoGroupDto> Value { get; set; }
    }
    //public class HrReportModel
    //{
    //    public DateTime StartDate { get; set; }
    //    public DateTime EndDate { get; set; }
    //    public List<GetHrReportData> data { get; set; }
    //}
    public class ModelUserReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int i { get; set; }
        public List<GetHrReportData> data { get; set; }
        public List<CompanyDto> ListCompany { get; set; }
    }
}
