﻿namespace NhaDat24h.DataAccess.Interface
{
	public interface IDBContext : IDisposable
	{
		int SaveChanges();
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, System.Threading.CancellationToken cancellationToken);
	}
}
