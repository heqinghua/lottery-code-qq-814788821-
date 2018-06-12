using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Service.YtgCache
{
    public class DictionaryCache
    {
        private static DictionaryCache mDictionaryCache;

        private List<SysDictionary> mSysDictionarys = new List<SysDictionary>();

        public static DictionaryCache CreateInstance()
        {
            if (null == mDictionaryCache)
                mDictionaryCache = new DictionaryCache();
            return mDictionaryCache;
        }

        private DictionaryCache()
        {

        }

        /// <summary>
        /// 是否有数据
        /// </summary>
        public bool HasValue
        {
            get
            {
                return this.mSysDictionarys.Count > 0;
            }
        }

        /// <summary>
        /// 改变数据源
        /// </summary>
        /// <param name="source"></param>
        public void ChangeSource(IEnumerable<SysDictionary> source)
        {
            mSysDictionarys.Clear();
            foreach (var item in source)
                mSysDictionarys.Add(item);
        }

        /// <summary>
        /// 根据字典组获取字典数据
        /// </summary>
        /// <param name="groupNmae"></param>
        /// <returns></returns>
        public List<SysDictionary> GetGroup(string groupNmae)
        {
            return this.mSysDictionarys.Where(item => item.Group == groupNmae).ToList();
        }
    }
}
