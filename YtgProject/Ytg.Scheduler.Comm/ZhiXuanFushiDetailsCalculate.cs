using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 直选复试 内容格式化
    /// </summary>
    public abstract class ZhiXuanFushiDetailsCalculate : BaseCalculate
    {
        /// <summary>
        /// 返回位数长度
        /// </summary>
        protected virtual int GetLen
        {
            get { return 5; }
        }

        public override string HtmlContentFormart(string betContent)
        {
            betContent = betContent.Replace("&", " ").Replace("|", ",");
            if (betContent.Split(',').Length == GetLen)
            {
                betContent = VerificationSelectBetContentSscMany(betContent);
                if (!string.IsNullOrEmpty(betContent))
                    return betContent.Replace(" ", "");
            }
            return string.Empty;
        }
    }
}
