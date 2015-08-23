using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo_core.Models.DB;

namespace Demo_core.Repositories
{
	public class ManufacturerRepository : IDatabaseRepository<Manufacturer>
	{
		public Manufacturer Add(Manufacturer Item)
		{
			throw new NotImplementedException();
		}

		public int Delete(Guid ID)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Manufacturer> GetAll()
		{
			throw new NotImplementedException();
		}

		public Manufacturer GetByID(Guid ID)
		{
			throw new NotImplementedException();
		}

		public Manufacturer Update(Manufacturer Item)
		{
			throw new NotImplementedException();
		}
	}
}
