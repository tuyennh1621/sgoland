using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Ctv;
using NhaDat24h.DataDto.RealEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaDat24h.DataDto.Statistics
{
    public class StatisticDto
    {
        public REReportDto REReport { get; set; }
        public HRReportDto HrReport{ get; set; }
        public List<CompanyDto> ListCompany { get; set; }

    }
}
