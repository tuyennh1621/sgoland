using NhaDat24h.Data.Base;
using NhaDat24h.DataDto.Users;

namespace NhaDat24h.Services.Users
{
    public interface IUserServices
    {
        public ResponseBase InserUser(UserInsertDataDto param);
    }
}
