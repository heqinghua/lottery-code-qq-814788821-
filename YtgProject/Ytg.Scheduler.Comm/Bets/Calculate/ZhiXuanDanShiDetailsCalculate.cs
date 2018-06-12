using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    /// <summary>
    /// 文本框 ，单式
    /// </summary>
    public abstract class ZhiXuanDanShiDetailsCalculate : BaseCalculate
    {
        /// <summary>
        /// 每组分组后长度
        /// </summary>
        protected virtual int GroupLen
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// 是否允许重复
        /// </summary>
        protected virtual bool IsRept {
            get {
                return false;
            }
        }

        /// <summary>
        /// 内容长长度
        /// </summary>
        protected virtual int ItemLen
        {
            get
            {
                return 5;
            }
        }
        /// <summary>
        /// 是否需验证排序后重复
        /// </summary>
        protected virtual bool IsSort
        {
            get {
                return false;
            }
        }

        public override string HtmlContentFormart(string betContent)
        {
            return VerificationBetContentOneNumSsc(betContent.Replace("&", ","), GroupLen, ItemLen, IsRept, IsSort);
        }

        
    }
}
