using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;

namespace Ytg.Data
{
    public class DelRepo<T>:IDelRepo<T> where T:DelEntity
    {
        protected readonly DbContext mDbContext;

        public DelRepo(IDbContextFactory factory)
        {
            this.mDbContext = factory.GetDbContext();
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool showDeleted = false)
        {
            var res = mDbContext.Set<T>().Where(predicate);
            if (!showDeleted) res = res.Where(o => o.IsDelete == false);
            return res;
        }

        public IQueryable<T> GetAll()
        {
            return mDbContext.Set<T>();
        }

        public void Restore(T o)
        {
            o.IsDelete = false;
        }
    }
}
