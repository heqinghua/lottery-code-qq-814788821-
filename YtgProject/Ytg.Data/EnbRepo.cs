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
    /// <summary>
    /// 启/禁用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnbRepo<T> : IEnbRepo<T> where T : EnaEntity
    {

        protected readonly DbContext mDbContext;

        public EnbRepo(IDbContextFactory factory)
        {
            this.mDbContext = factory.GetDbContext();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="showDeleted"></param>
        /// <returns></returns>
        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate, bool isEnabled = false)
        {
            var res = mDbContext.Set<T>().Where(predicate);
            if (isEnabled) res = res.Where(o => o.IsEnable == isEnabled);
            return res;
        }
        /// <summary>
        /// 查询 所有启用
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return mDbContext.Set<T>().Where(item => item.IsEnable);
        }

        /// <summary>
        /// 修改为启用状态
        /// </summary>
        /// <param name="o"></param>
        public void Restore(T o)
        {
            o.IsEnable = true;
        }



        public void Disabled(T o)
        {
            o.IsEnable = false;
        }
    }
}
