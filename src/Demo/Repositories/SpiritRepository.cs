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
		private DatabaseContext _db = new DatabaseContext();
		public Spirit Add(Spirit Item)
		{
			_db.Add(Item);
			_db.SaveChanges();
			return Item;
		}

		public int Delete(Guid ID)
		{
			var spirit = _db.Spirits.FirstOrDefault(x => x.Id == ID);
			if (spirit == null)
			{
				return 0;
			}
			_db.Spirits.Remove(spirit);
			var rowsChanged = _db.SaveChanges();

			return rowsChanged;
		}

		public IEnumerable<Spirit> GetAll()
		{
			return _db.Spirits.OrderBy(x => x.Id).ToList();
		}

		public Spirit GetByID(Guid ID)
		{
			return _db.Spirits.Where(x => x.Id == ID).FirstOrDefault();
		}

		public Spirit Update(Spirit Item)
		{
			var original = _db.Spirits.Where(x => x.Id == Item.Id).FirstOrDefault();
			if (original == null){ return null; }
			_db.Spirits.Update(original);
			return original;
		}
	}
}
