using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Repository
{
    /// <summary>
    /// 必须基础baseEntity的实体
    /// </summary>
    public interface IUniRepo
    {
        T Insert<T>(T o) where T : BaseEntity, new();
        void Save();
        T Get<T>(int id) where T : BaseEntity;
        IEnumerable<T> GetAll<T>() where T : BaseEntity;
    }
}
