using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel
{
    [Table("RecordTemp")]
    public class RecordTemp : EnaEntity
    {
        public RecordTemp()
        {
            this.IsCompled = false;
        }


        /// <summary>
        /// 充值全球唯一值 充值编号
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 对应银行id
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        ///用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal TradeAmt { get; set; }


        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCompled { get; set; }


        #region my18

        /// <summary>
        /// 到帐时间
        /// </summary>
        public string MY18DT { get; set; }
        /// <summary>
        /// 支付宝/财付通/网银交易流水号
        /// </summary>
        public string MY18oid { get; set; }
        /// <summary>
        /// 打款人
        /// </summary>
        public string MY18JYF { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string MY18FY { get; set; }
        /// <summary>
        /// 打款金额
        /// </summary>
        public string MY18M { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public string MY18HF { get; set; }

        /// <summary>
        /// 收款人
        /// </summary>
        public string MY18SKR { get; set; }

        /// <summary>
        /// 支付方式 
        /// </summary>
        public string MY18PT { get; set; }

        #endregion

    }
}
