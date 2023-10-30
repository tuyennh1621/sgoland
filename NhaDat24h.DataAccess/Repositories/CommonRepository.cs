using NhaDat24h.DataAccess.DBContext;
using NhaDat24h.DataAccess.Interface;

namespace NhaDat24h.DataAccess.Repositories
{
	public class CommonRepository<T> : Repository<CommonDBContext, T>, ICommonRepository<T> where T : class
	{
		public CommonRepository(CommonDBContext context) : base(context)
		{

		}
	}
}
