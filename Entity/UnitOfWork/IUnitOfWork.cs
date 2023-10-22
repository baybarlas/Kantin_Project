using KantindenAl.App.Entity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.Entity.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<T> GetRepository<T>() where T : class, new();
		void Commit(); 
		Task CommitAsync();
	}
}
