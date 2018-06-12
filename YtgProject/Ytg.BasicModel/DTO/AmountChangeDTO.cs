using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 帐变列表
    /// </summary>
    public class AmountChangeDTO
    {
        /// <summary>
        /// 用户余额明细表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 帐变编号 用户余额明细表中的 SerialNo
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 用户名/User表里面的Code
        /// </summary>
        public string UserAccount { get; set; }

        /// <summary>
        /// 时间，用户余额表中的OccDate
        /// </summary>
        public DateTime OccDate { get; set; }

        /// <summary>
        /// 类型，用户余额表中的记录
        /// </summary>
        public int TradeType { get; set; }

        /// <summary>
        /// 游戏名称
        /// </summary>
        public string LotteryName { get; set; }

        //
        public string PlayTypeName { get; set; }

        /// <summary>
        /// 具体玩法
        /// </summary>
        public string PlayTypeRadioName { get; set; }

        /// <summary>
        /// 期号，投注记录表里面的IssueCode
        /// </summary>
        public string IssueCode { get; set; }

        /// <summary>
        /// 模式，这个是投注记录里面的Model
        /// </summary>
        public int? Model { get; set; }

        /// <summary>
        /// 收入，用户余额表中的TradeAmt，大于0表示收入
        /// </summary>
        public decimal InAmt { get; set; }

        /// <summary>
        /// 支出，用户余额表中的TradeAmt，小于0表示支出
        /// </summary>
        public decimal OutAmt { get; set; }

        /// <summary>
        /// 余额，用户余额表中的UserAmt
        /// </summary>
        public decimal UserAmt { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 关联编号
        /// </summary>
        public string RelevanceNo { get; set; }

        public string PostionName { get; set; }
    }
}
