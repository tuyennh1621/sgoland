using NhaDat24h.DataAccess.DBContext;
using NhaDat24h.DataAccess.Interface;

namespace NhaDat24h.DataAccess.UnitOfWork
{
	public class CommonUoW : UnitOfWork<CommonDBContext>, ICommonUoW
	{


		public CommonUoW(CommonDBContext context) : base(context)
		{
		}

	}
}
