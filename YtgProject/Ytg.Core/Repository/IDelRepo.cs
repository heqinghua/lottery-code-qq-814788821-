using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Repository
{
    /// <summary>
    /// 禁用对象的接口
    /// </summary>
    public interface IDelRepo<T> where T:BaseEntity
    {
        IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool showDeleted = false);

        IQueryable<T> GetAll();
       /// <summary>
       /// 还原，恢复为启用状态
       /// </summary>
       /// <param name="o"></param>
        void Restore(T o);
    }
}
