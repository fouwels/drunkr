using Demo_core.Models.DB;
using Demo_core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_core.Repositories
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private DatabaseContext DataContext;

		public GenericRepository()
		{
			DataContext = new DatabaseContext();
		}
		public virtual T Add(T Item)
		{
			DataContext.Set<T>().Add(Item);
			DataContext.SaveChanges();
			return Item;
		}

		public virtual int Delete(T Item)
		{
			DataContext.Set<T>().Remove(Item);
			return DataContext.SaveChanges();
		}

		public virtual IEnumerable<T> GetAll()
		{
			return DataContext.Set<T>().ToList();
		}

		public IEnumerable<T> GetBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{
			IQueryable<T> query = DataContext.Set<T>().Where(predicate);
			return query.ToList();
		}

		public virtual T Update(T Item)
		{
			DataContext.Update(Item);
			DataContext.SaveChanges();
			return Item;
		}
	}
}
