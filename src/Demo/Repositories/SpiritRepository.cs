using Demo.Models.DB;
using Demo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repositories
{
	public class SpiritRepository : IDatabaseRepository<Spirit>
	{
		public IEnumerable<Spirit> GetAll
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Spirit GetByID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool Add(Spirit Item)
		{
			throw new NotImplementedException();
		}

		public bool Delete(Spirit Item)
		{
			throw new NotImplementedException();
		}

		public bool Update(Spirit Item)
		{
			throw new NotImplementedException();
		}
	}
}
