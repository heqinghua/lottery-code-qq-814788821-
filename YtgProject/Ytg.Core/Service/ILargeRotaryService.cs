
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    public interface ILargeRotaryService : ICrudService<LargeRotary>
    {
        /// <summary>
        /// 获取当天数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        LargeRotary GetNowItem(int uid);

        /// <summary>
        /// 当天是否存在数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool HasNowItem(int uid);
    }
}
