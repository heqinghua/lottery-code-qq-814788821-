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
    /// <summary>
    /// 
    /// </summary>
    public class CrudService<T> : QueryService<T>, ICrudService<T> where T : BaseEntity, new()
    {

        public CrudService(IRepo<T> repo)
            : base(repo)
        {
            this.mRepo = repo;
        }

        /// <summary>
        /// 创建对象，并返回成功后的id
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual T Create(T item)
        {
            return this.mRepo.Insert(item);
        }

        public virtual void Save()
        {
            this.mRepo.Save();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int id)
        {
            this.mRepo.Delete(this.mRepo.Get(id));
            this.mRepo.Save();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="o"></param>
        public virtual void Delete(T o)
        {
            this.mRepo.Delete(o);
        }


        /// <summary>
        /// 返回单个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            return this.mRepo.Get(id);
        }

      
        public virtual IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> func, bool showDeleted = false)
        {
            return this.mRepo.Where(func,showDeleted);
        }

        /// <summary>
        /// 还原删除
        /// </summary>
        /// <param name="id"></param>
        public virtual void Restore(int id)
        {
            this.mRepo.Restore(this.mRepo.Get(id));
            this.mRepo.Save();
        }

    }
}
