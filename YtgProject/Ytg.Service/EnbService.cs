using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    public class EnbService<T> : IEnbService<T> where T : EnaEntity
    {
        protected IEnbRepo<T> mRepo;

        public EnbService(IEnbRepo<T> repo)
        {
            this.mRepo = repo;
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool isEnabled = true)
        {
            return this.mRepo.Where(predicate, isEnabled);
        }

        public IQueryable<T> GetAll()
        {
            return this.mRepo.GetAll();
        }

        public void Restore(T o)
        {
            this.mRepo.Restore(o);
        }

        public void Disabled(T o)
        {
            this.mRepo.Disabled(o);
        }
    }
}
