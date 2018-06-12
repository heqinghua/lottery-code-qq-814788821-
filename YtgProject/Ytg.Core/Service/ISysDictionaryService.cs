using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    public interface ISysDictionaryService : IQueryService<SysDictionary>
    {
   
        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        IEnumerable<SysDictionary> GetSysDictionary(string group);
    }
}
