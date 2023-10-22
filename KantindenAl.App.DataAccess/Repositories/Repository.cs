using KantindenAl.App.DataAccess.Contexts;
using KantindenAl.App.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KantindenAl.App.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T : class, new()
	{
		private readonly KantindenAlDbContext _context;
		private readonly DbSet<T> _dbSet;

		public Repository(KantindenAlDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task Add(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}

		public void Delete(int id)
		{
			var entity = _dbSet.Find(id);
			this.Delete(entity);
		}

		public void Delete(T entity)
		{
			if (entity.GetType().GetProperty("IsDeleted") != null)
			{
				entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
				_dbSet.Update(entity);
			}
			else
			{
				_dbSet.Remove(entity);
			}
		}

		public async Task<T> Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null)
				query = query.Where(filter);
			foreach (var table in includes)
			{
				query = query.Include(table);
			}
			return await query.AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;
			if (filter != null)
				query = query.Where(filter);
			if (orderby != null)
				query = orderby(query);
			foreach (var table in includes)
			{
				query = query.Include(table);
			}
			return await query.AsNoTracking().ToListAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetById(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		
	}
}
