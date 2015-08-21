using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models.DB;

namespace Demo.Repositories
{
	public class LiquidRepository : IDatabaseRepository<Liquid>
	{
		public Liquid Add(Liquid Item)
		{
			throw new NotImplementedException();
		}

		public int Delete(Guid ID)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Liquid> GetAll()
		{
			throw new NotImplementedException();
		}

		public Liquid GetByID(Guid ID)
		{
			throw new NotImplementedException();
		}

		public Liquid Update(Liquid Item)
		{
			throw new NotImplementedException();
		}
	}
}
