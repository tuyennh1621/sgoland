using NhaDat24h.DataDto.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaDat24h.DataDto.User
{


    public class GHrReportSynthesisRequestDto
    {
        public int IdCompany { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GHrReportSynthesisDto
    {

        public int IdCompany { get; set; }

        public string Company { get; set; }
        public string Department { get; set; }
        public int CurrentNumberHr { get; set; }

        public int NumberHrNewIncreased { get; set; }

        public int NumberHrDecreased { get; set; }

        public string Note { get; set; }
        public int SumCurrentNumberHr { get; set; }
        public int SumNumberHrNewIncreased { get; set; }
        public int SumNumberHrDecreased { get; set; }
        public int TotalRow { get; set; }
    }

    public class GetHrReportSynthesisKey
    {
        public int IdCompany { get; set; }
        public string Company { get; set; }
    }
    public class GetHrReportSynthesisData
    {
        public int i { get; set; }
        public GetHrReportSynthesisKey Key { get; set; }
        public List<GHrReportSynthesisDto> Value { get; set; }
    }

    public class HrReportSynthesisModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int i { get; set; }
        public List<GetHrReportSynthesisData> data { get; set; }
        public List<CompanyDto> ListCompany { get; set; }
    }



}
