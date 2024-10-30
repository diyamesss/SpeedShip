using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpeedShip.DataAccess.Repository
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		IQueryable<T> Get(Expression<Func<T, bool>> filter);
		void Create(T obj);
		void Update(T obj);
		void Delete(T obj);
		Task SaveAsync();
	}
}
