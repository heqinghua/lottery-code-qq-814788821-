using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.LotteryBasic
{
    /// <summary>
    /// 游戏记录/投注记录查询列表
    /// </summary>
    [Serializable, DataContract]
    public class BetList
    {
        /// <summary>
        /// 投注记录Id 主键
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        [DataMember]
        public string IssueCode { get; set; }

        /// <summary>
        /// 注单编号
        /// </summary>
        [DataMember]
        public string BetCode { get; set; }

        /// <summary>
        /// 多少注
        /// </summary>
        [DataMember]
        public int BetCount { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [DataMember]
        public decimal TotalAmt { get; set; }

        /// <summary>
        /// 多少倍
        /// </summary>
        [DataMember]
        public int Multiple { get; set; }

        /// <summary>
        /// 模式，元 角 分
        /// </summary>
        [DataMember]
        public int Model { get; set; }

        /// <summary>
        /// 返点类型
        /// </summary>
        [DataMember]
        public int PrizeType { get; set; }

        /// <summary>
        /// 返点
        /// </summary>
        [DataMember]
        public decimal BackNum { get; set; }

        /// <summary>
        /// 投注内容
        /// </summary>
        [DataMember]
        public string BetContent { get; set; }

        /// <summary>
        /// 投注时间
        /// </summary>
        [DataMember]
        public DateTime OccDate { get; set; }

        /// <summary>
        /// 赢多少钱
        /// </summary>
        [DataMember]
        public decimal WinMoney { get; set; }

        /// <summary>
        /// 开奖结果
        /// </summary>
        [DataMember]
        public string OpenResult { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int Stauts { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 具体玩法
        /// </summary>
        [DataMember]
        public string PlayTypeRadioName { get; set; }

        /// <summary>
        /// 玩法单选编号
        /// </summary>
        [DataMember]
        public int RadioCode { get; set; }


        /// <summary>
        /// 第二层
        /// </summary>
        [DataMember]
        public string PlayTypeNumName { get; set; }

        /// <summary>
        /// 玩法类别名称
        /// </summary>
        [DataMember]
        public string PlayTypeName { get; set; }

        /// <summary>
        /// 彩种
        /// </summary>
        [DataMember]
        public string LotteryName { get; set; }




        /// <summary>
        /// 玩法单选编号
        /// </summary>
        [DataMember]
        public int PalyRadioCode { get; set; }

        /// <summary>
        /// 彩票编码
        /// </summary>
        [DataMember]
        public string LotteryCode { get; set; }

        /// <summary>
        /// 坠海
        /// </summary>
         [DataMember]
        public string CatchNumIssueCode { get; set; }

        /// <summary>
        /// 0为投注 1为追号
        /// </summary>
         [DataMember]
        public int tp { get; set; }
        [DataMember]
         public string PostionName { get; set; }

        /// <summary>
        /// 认购金额
        /// </summary>
        [DataMember]
        public decimal Subscription { get; set; }



    }
}
