using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Demo_core.Repositories
{
	public interface IGenericRepository<T>
	{
		IEnumerable<T> GetAll();
		IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate);
		T Add(T Item);
		T Update(T Item);
		int Delete(T Item);
	}
}
