using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Service.YtgCache;

namespace Ytg.Service
{
    /// <summary>
    /// 系统字典
    /// </summary>
    public class SysDictionaryService : QueryService<SysDictionary>, ISysDictionaryService
    {
        /**
         字典缓存
         */

        

        public SysDictionaryService(IRepo<SysDictionary> repo)
            : base(repo)
        {

        }



        public IEnumerable<SysDictionary> GetSysDictionary(string group)
        {

            if (!DictionaryCache.CreateInstance().HasValue)
            {
                DictionaryCache.CreateInstance().ChangeSource(GetAll());
            }

            return DictionaryCache.CreateInstance().GetGroup(group);
        }

    }
}
