using Microsoft.EntityFrameworkCore;
using SpeedShip.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpeedShip.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly DbSpeedShipContext _dbSpeedShipContext;
		private DbSet<T> _dbSet;
		public Repository(DbSpeedShipContext dbSpeedShipContext)
		{
			_dbSpeedShipContext = dbSpeedShipContext;
			_dbSet = dbSpeedShipContext.Set<T>();
		}

		public void Create(T obj)
		{
			 _dbSet.Add(obj);
		}

		public void Delete(T obj)
		{
			_dbSet.Remove(obj);
		}

		public IQueryable<T> Get(Expression<Func<T, bool>> filter)
		{
			return _dbSet.Where(filter).AsQueryable();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			try
			{
                return await _dbSet.ToListAsync();

            }
            catch (Exception ex)
			{

				throw;
			}
		}

		public async Task SaveAsync()
		{
			await _dbSpeedShipContext.SaveChangesAsync();
		}

		public void Update(T obj)
		{
			_dbSet.Update(obj);
		}
	}
}
