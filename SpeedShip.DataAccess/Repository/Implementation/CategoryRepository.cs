using SpeedShip.DataAccess.Database;
using SpeedShip.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedShip.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly DbSpeedShipContext _dbSpeedShipContext;

		public CategoryRepository(DbSpeedShipContext dbSpeedShipContext) : base(dbSpeedShipContext)
		{
			_dbSpeedShipContext = dbSpeedShipContext;
		}
	}
}
