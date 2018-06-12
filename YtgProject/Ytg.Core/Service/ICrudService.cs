using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    /// <summary>
    /// 增删改查接口
    /// </summary>
    
    public interface ICrudService<T> : IQueryService<T> where T : BaseEntity, new()
    {
        T Create(T item);
        void Save();
        void Delete(int id);
        void Delete(T o);

        T Get(int id);
        IEnumerable<T> Where(Expression<Func<T, bool>> func, bool showDeleted = false);
        void Restore(int id);

        
    }
}
