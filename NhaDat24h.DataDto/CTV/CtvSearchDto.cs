using NhaDat24h.DataDto.Company;
using NhaDat24h.DataDto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaDat24h.DataDto.Ctv
{
    public class CtvSearchDto
    {
        public List<UserSearchOutputDto> ListCtv { get; set; }  
        public List<CompanyDto> ListCompany { get; set; }
    }
}
