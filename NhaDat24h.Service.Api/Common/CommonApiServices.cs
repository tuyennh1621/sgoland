using NhaDat24h.Common;
using NhaDat24h.DataDto.Common;

namespace NhaDat24h.Service.Api.Common
{ 
    public class CommonApiServices : ApiServiceBase, ICommonApiServices
    {
		public ResponseBase<ModelDashboard> GetDashBoard(int IdCtv)
        {
			var response = Get<ModelDashboard>("common/dashboard"
				, new KeyValuePair<string, object>("IdCtv", IdCtv));
			return response;
		}
    }
}
