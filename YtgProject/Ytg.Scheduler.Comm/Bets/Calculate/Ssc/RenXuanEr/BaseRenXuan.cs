using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate.Ssc.RenXuanEr
{
    public abstract class BaseRenXuan : BaseCalculate
    {
        /// <summary>
        /// 拆分任选投注内容
        /// </summary>
        /// <param name="content">原始内容</param>
        /// <param name="postionStr">位置结果集</param>
        /// <returns>原始投注内容</returns>
        protected string SplitRenXuanContent(string content, ref string postionStr)
        {
            if (string.IsNullOrEmpty(content))
                return string.Empty;
            var contentArry = content.Split('_');
            if (contentArry.Length == 2)
            {
                postionStr = contentArry[1];
                return contentArry[0];
            }

            return "";
        }

        protected virtual int PostionLen {

            get {
                return 2;
            }
        }

        /// <summary>
        /// 验证位置数值是否正确
        /// </summary>
        /// <param name="postionStr"></param>
        /// <returns></returns>
        protected bool VerificationPostion(string postionStr)
        {
            if (string.IsNullOrEmpty(postionStr))
                return false;
            foreach (var p in postionStr)
            {
                int postion = 0;
                if (!int.TryParse(p.ToString(), out postion)
                    || postion < 1
                    || postion > 5)
                    return false;
            }
            return postionStr.Length >= PostionLen;
        }

       
    }
}
