using NhaDat24h.DataDto.RealEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaDat24h.DataDto.Common
{
    public partial class ModelDashboard
    {
        public int? TotalMember { get; set; }
        public int? NumMemWaitingToAccept { get; set; }
        public int? NumMemWaitingToAppoint { get; set; }
        public int? TotalCompany { get; set; }
        public int? TotalREPj { get; set; }
        public int? TotalRERe { get; set; }

        public List<RealEstateDto>? ListSaveRE { get; set; }
        public List<RealEstateDto>? ListNewREPj { get; set; }
        public List<RealEstateDto>? ListTopREPj { get; set; }
        public List<RealEstateDto>? ListTopHN { get; set; }
        public List<RealEstateDto>? ListTopQN { get; set; }
        public List<RealEstateDto>? ListTopPY { get; set; }
        public List<RealEstateDto>? ListTopHP { get; set; }
        public List<RealEstateDto>? ListTopBD { get; set; }
    }
}
