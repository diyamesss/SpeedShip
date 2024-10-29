using Microsoft.EntityFrameworkCore;
using SpeedShip.DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedShip.DataAccess.DbInitializer
{
	public class DbInitializer : IDbInitializer
	{
		public DbSpeedShipContext _dbSpeedShipContext;

        public DbInitializer(DbSpeedShipContext dbSpeedShipContext)
        {
			_dbSpeedShipContext = dbSpeedShipContext;

		}
        public void Initialize()
		{
			try
			{
				if (_dbSpeedShipContext.Database.GetPendingMigrations().Count() > 0)
				{
					_dbSpeedShipContext.Database.Migrate();
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}
