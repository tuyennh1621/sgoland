using NhaDat24h.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace NhaDat24h.DataAccess.Base
{
	public abstract class PDataContext : DbContext, IDBContext
	{
		protected PDataContext(DbContextOptions options) : base(options)
		{

		}
	}
}
