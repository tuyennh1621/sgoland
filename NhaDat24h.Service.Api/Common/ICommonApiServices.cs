using NhaDat24h.Common;
using NhaDat24h.DataDto.Common;

namespace NhaDat24h.Service.Api.Common
{
    public interface ICommonApiServices
    {
        public ResponseBase<ModelDashboard> GetDashBoard(int IdCtv);

    }
}
