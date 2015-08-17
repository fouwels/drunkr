using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repositories
{
    public interface IDatabaseRepository<T>
    {
		IEnumerable<T> GetAll {	get; }
		T GetByID {	get; }
		bool Add(T Item);
		bool Update(T Item);
		bool Delete(T Item);
    }
}
