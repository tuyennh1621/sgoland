﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaDat24h.DataAccess.Interface
{
	public interface IRepository<T> : IQueryRepository<T>, ICommandRepository<T> where T : class
	{

	}
}
