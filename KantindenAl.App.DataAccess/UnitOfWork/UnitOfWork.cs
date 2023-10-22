using KantindenAl.App.DataAccess.Contexts;
using KantindenAl.App.DataAccess.Repositories;
using KantindenAl.App.Entity.Repositories;
using KantindenAl.App.Entity.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly KantindenAlDbContext _context;
		private bool disposed = false;

		public UnitOfWork(KantindenAlDbContext context)
		{
			_context = context;
		}

		public IRepository<T> GetRepository<T>() where T : class, new()
		{
			return new Repository<T>(_context);
		}

		public void Commit()
		{
			_context.SaveChanges();
		}

		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}


}

