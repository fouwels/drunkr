using Demo_core.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_core.Repositories
{
	public class BottleRepository : IDatabaseRepository<Bottle>
	{
		public Bottle Add(Bottle Item)
		{
			throw new NotImplementedException();
		}

		public int Delete(Guid ID)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Bottle> GetAll()
		{
			throw new NotImplementedException();
		}

		public Bottle GetByID(Guid ID)
		{
			throw new NotImplementedException();
		}

		public Bottle Update(Bottle Item)
		{
			throw new NotImplementedException();
		}
	}
}
