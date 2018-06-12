using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.Core.Repository;
using Omu.ValueInjecter;
using System.Data.Entity;

namespace Ytg.Data
{
    public class UniRepo:IUniRepo
    {
        protected readonly DbContext mDbContext;

        public UniRepo(IDbContextFactory factory)
        {
            this.mDbContext = factory.GetDbContext();
        }

        public T Insert<T>(T o) where T : BasicModel.BaseEntity, new()
        {
            var t = new T();
            t.InjectFrom(o);
            mDbContext.Set<T>().Add(t);
            return t;
        }

        public void Save()
        {
            mDbContext.SaveChanges();
        }

        public T Get<T>(int id) where T : BasicModel.BaseEntity
        {
            return mDbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll<T>() where T : BasicModel.BaseEntity
        {
            return mDbContext.Set<T>();
        }
    }
}
