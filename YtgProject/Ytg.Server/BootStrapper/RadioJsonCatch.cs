using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ytg.ServerWeb.BootStrapper
{
    public class RadioJsonCatch
    {
        Dictionary<string, string> mCatchDictionary = new Dictionary<string, string>();//缓存集合

        private static RadioJsonCatch mRadioJsonCatch=null;

        public static RadioJsonCatch CreateIntance()
        {
            if (mRadioJsonCatch == null)
                mRadioJsonCatch = new RadioJsonCatch();
            return mRadioJsonCatch;
        }

        /// <summary>
        /// 增加项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public  void Put(string key, string content)
        {
            if (!mCatchDictionary.ContainsKey(key))
                mCatchDictionary.Add(key,content);
        }

        public  string Get(string key)
        {
            if (mCatchDictionary.ContainsKey(key))
                return mCatchDictionary[key];
            return string.Empty;
        }
    }
}