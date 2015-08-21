using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repositories
{
    public interface IDatabaseRepository<T>
    {
		IEnumerable<T> GetAll();
		T GetByID(Guid ID);
		T Add(T Item);
		T Update(T Item);
		int Delete(Guid ID);
    }
}
