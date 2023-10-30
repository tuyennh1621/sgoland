using NhaDat24h.DataDto.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaDat24h.DataDto.User
{
    public class ReportBusinessRequestDto
    {
        public int IdCompany { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class ReportBusinessDto
    {
        public int Id { get; set; }
        public string ReferralCode { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public int NumberTransactions { get; set; }
        public decimal TransactionValue { get; set; }
        public string Rating { get; set; }
        public string Note { get; set; }
        public string Company { get; set; }

    }
    public class GetReportBusinessKey
    {
        public int IdCompany { get; set; }
        public string Company { get; set; }
    }
    public class GetReportBusinessData
    {
        public int i { get; set; }
        public GetReportBusinessKey Key { get; set; }
        public List<ReportBusinessDto> Value { get; set; }
    }

    public class HrReportBusinessModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int i { get; set; }
        public List<GetReportBusinessData> data { get; set; }

        public List<CompanyDto> ListCompany { get; set; }


    }





}
