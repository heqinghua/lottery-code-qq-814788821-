using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Core.Service.Lott;
using Ytg.Data;
using Ytg.Service.Lott;

namespace Ytg.Scheduler.Comm.Bets.Calculate
{
    public abstract class BaseCalculate : ICalculate
    {
        public BaseCalculate()
        {
           
        }

        public void Calculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item)
        {
            if (null == item
                || string.IsNullOrEmpty(issueCode)
                || string.IsNullOrEmpty(openResult)
                || item.IssueCode != issueCode
                || string.IsNullOrEmpty(item.BetContent))
                return;
            this.IssueCalculate(issueCode, openResult, item);
        }

        /// <summary>
        /// 计算金额
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual decimal SumWinMoney(BasicModel.LotteryBasic.BetDetail item)
        {
            return item.TotalAmt + item.TotalAmt * item.BackNum;
        }


        protected abstract void IssueCalculate(string issueCode, string openResult, BasicModel.LotteryBasic.BetDetail item);



        public virtual int TotalBetCount(BasicModel.LotteryBasic.BetDetail item)
        {
            if (null == item || string.IsNullOrEmpty(item.BetContent)) return 1;
            else
            {
                var count = 1;
                item.BetContent.Split(',').ToList().ForEach(m => count = count * m.Length);
                return count;
            }
        }

        
        /// <summary>
        /// 根据 投注信息获取奖金集合
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual List<PlayTypeRadiosBonus> GetBaseAmtLst(BasicModel.LotteryBasic.BetDetail item,ref PlayTypeRadio radio)
        {
            var source = BaseDataCatch.GetPalyTypeRadio();
            var palyTypeRadio = source.Where(c => c.RadioCode == item.PalyRadioCode).FirstOrDefault();
            if (null == palyTypeRadio)
                return null;
            radio = palyTypeRadio;
            return BaseDataCatch.GetPlayTypeRadiosBonus().Where(c => c.RadioCode == palyTypeRadio.RadioCode).ToList();
        }

        /// <summary>
        /// 获取奖金
        /// </summary>
        /// <param name="rcount"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual decimal GetBaseAmtLstItem(int bonusCount, BasicModel.LotteryBasic.BetDetail item,ref decimal stepAmt )
        {
            PlayTypeRadio outPlayTypeRadio=null;
            var res = this.GetBaseAmtLst(item,ref outPlayTypeRadio);
            if (res == null)
                return 0;
            var it = res.Where(c => c.BonusCount == bonusCount).FirstOrDefault();
            if (it == null)
                return 0;

            decimal baseAmt = 0;
            if (item.BonusLevel == 1700)
            {
                baseAmt = item.PrizeType == 1 ? it.BonusBasic17 : GetbackNumMonery(it.MaxBonus17, outPlayTypeRadio.MaxRebate1700,it.StepAmt1700, item.BackNum);// GetbackNumMonery(it.MaxBonus17,item.PalyRadioCode,item.BackNum); ;
                stepAmt = it.StepAmt1700;
            }
            else
            {
                baseAmt = item.PrizeType == 1 ? it.BonusBasic : GetbackNumMonery(it.MaxBonus,outPlayTypeRadio.MaxRebate,it.StepAmt, item.BackNum); //it.MaxBonus;
                stepAmt = it.StepAmt;
            }

            return baseAmt;
        }



        /// <summary>
        /// 获取奖金
        /// </summary>
        /// <param name="rcount"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual decimal GetBaseAmtLstItem(int bonusCount, BasicModel.LotteryBasic.BetDetail item)
        {
            decimal stepAmt=0;
            return GetBaseAmtLstItem(bonusCount, item, ref stepAmt);
        }

        /// <summary>
        /// 根据 投注信息获取奖金
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual decimal GetBaseAmt(BasicModel.LotteryBasic.BetDetail item)
        {
            return GetBaseAmt(item.PalyRadioCode, item.PrizeType, item.BonusLevel,item.BackNum);
        }

        /// <summary>
        /// 根据投注信息获取对应奖金
        /// </summary>
        /// <param name="playRadioCode">玩法单选编号</param>
        /// <param name="PrizeType">是否舍弃返点</param>
        /// <param name="BonusLevel">玩法 1700/1800</param>
        /// <returns></returns>
        protected virtual decimal GetBaseAmt(int playRadioCode, int PrizeType, int BonusLevel,decimal backNum)
        {
            decimal stepAmt=0;
            return this.GetBaseAmt(playRadioCode, PrizeType, BonusLevel, backNum, ref stepAmt);
        }

   

        /// <summary>
        /// 根据 投注信息获取奖金
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual decimal GetBaseAmt(BasicModel.LotteryBasic.BetDetail item,ref decimal stepAmt)
        {
            return GetBaseAmt(item.PalyRadioCode, item.PrizeType, item.BonusLevel, item.BackNum, ref stepAmt);
        }

        /// <summary>
        /// 根据投注信息获取对应奖金
        /// </summary>
        /// <param name="playRadioCode">玩法单选编号</param>
        /// <param name="PrizeType">是否舍弃返点</param>
        /// <param name="BonusLevel">玩法 1700/1800</param>
        /// <returns></returns>
        protected virtual decimal GetBaseAmt(int playRadioCode, int PrizeType, int BonusLevel,decimal backNum, ref decimal stepAmt)
        {
            var source = BaseDataCatch.GetPalyTypeRadio();
            var palyTypeRadio = source.Where(c => c.RadioCode == playRadioCode).FirstOrDefault();
            if (null == palyTypeRadio)
                return 0;

            decimal baseAmt = 0;
            if (BonusLevel == 1700)
            {
                baseAmt = PrizeType == 1 ? palyTypeRadio.BonusBasic17 : GetbackNumMonery1700(palyTypeRadio.MaxBonus17, palyTypeRadio, backNum); //palyTypeRadio.MaxBonus17;
                
                stepAmt = palyTypeRadio.StepAmt1700;
            }
            else
            {
                baseAmt = PrizeType == 1 ? palyTypeRadio.BonusBasic : GetbackNumMonery(palyTypeRadio.MaxBonus, palyTypeRadio, backNum);//palyTypeRadio.MaxBonus;//1800
                stepAmt = palyTypeRadio.StepAmt;
            }
            return baseAmt;
        }

        private decimal GetbackNumMonery(decimal BonusBasic, PlayTypeRadio radio, decimal backNum)
        {
            return GetbackNumMonery(BonusBasic,radio.MaxRebate,radio.StepAmt,backNum);
        }

        private decimal GetbackNumMonery1700(decimal BonusBasic, PlayTypeRadio radio, decimal backNum)
        {
            return GetbackNumMonery(BonusBasic, radio.MaxRebate1700, radio.StepAmt1700, backNum);
        }

        public decimal GetbackNumMonery(decimal BonusBasic, double maxRebate,decimal stepAmt, decimal backNum)
        {
            return BonusBasic - backNum * 10 * stepAmt;
        }

        /// <summary>
        /// 计算中奖金额,stepAmt * 10 * item.BackNum *10得意思是：除以0.1相当于*10
        /// </summary>
        /// <param name="item">投注详情</param>
        /// <param name="baseAmt">基础奖金</param>
        /// <param name="stepAmt">每增加0.1的返点 增加多少奖金</param>
        /// <param name="count">中多少注</param>
        /// <returns></returns>
        protected decimal TotalWinMoney(BasicModel.LotteryBasic.BetDetail item, decimal baseAmt, decimal stepAmt, int count)
        {
            decimal[] models = { 1M, 0.1M, 0.01M,0.001M };

            var total = count * baseAmt * models[item.Model] * item.Multiple;

            return total;
        }

        /// <summary>
        /// 计算中奖金额,stepAmt * 10 * item.BackNum *10得意思是：除以0.1相当于*10
        /// </summary>
        /// <param name="item">投注详情</param>
        /// <param name="baseAmt">基础奖金</param>
        /// <param name="stepAmt">每增加0.1的返点 增加多少奖金</param>
        /// <param name="count">中多少注</param>
        /// <returns></returns>
        protected decimal BiaoZhunTotalWinMoney(BasicModel.LotteryBasic.BetDetail item, decimal baseAmt)
        {
            decimal[] models = { 1M, 0.1M, 0.01M,0.001M };

            var total = baseAmt * models[item.Model] * item.Multiple;

            return total;
        }

        /// <summary>
        /// html 投注内容格式化
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual string HtmlContentFormart(string betContent)
        {
            return betContent;
        }



        /// <summary>
        /// 验证手动输入是否合法，每项数据长度为2
        /// 01 02 03,04 05 06
        /// </summary>
        /// <param name="betContent"></param>
        /// <param name="len"></param>
        /// <param name="groupLen"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public virtual string VerificationBetContent(string betContent, int groupLen, int len, int minNum = 0, int maxNum = 9, char split = ',')
        {
            var array = betContent.Split(split);
        
            var list = new List<string>();
            foreach (var item in array)
            {
                var nums = item.Split(' ');
                if (nums.Distinct().Count() != groupLen) continue;
                if (nums.All(m => m.Length == len && (Convert.ToInt32(m) >= minNum && Convert.ToInt32(m) <= maxNum))) list.Add(item);
            }
            return string.Join(split.ToString(), list);
        }

        /// <summary>
        /// 验证11x5内容是否合法
        /// </summary>
        /// <param name="betContent"></param>
        /// <param name="groupLen"></param>
        /// <returns></returns>
        public virtual string VerificationBetContent(string betContent, int groupLen, char split = ',')
        {
            return this.VerificationBetContent(betContent, groupLen, 2,1,11,split);
        }

        /// <summary>
        /// 验证选择项是否为合法数字
        /// </summary>
        /// <param name="betContent"></param>
        /// <returns></returns>
        public virtual string VerificationSelectBetContent(string betContent,int minNum=0,int MaxNum=9,int len=1,bool isRepeat=true,char split = ',')
        {
            var contentArray= betContent.Split(split);
            if (!isRepeat)//不允许出现重复的数字 ,.在服务器端出现此情况，绝对是非法请求
                if (contentArray.Distinct().Count() != contentArray.Length)
                    return "";
            
            var list = new List<string>();
            foreach (var item in contentArray)
            {
                var nums = item.Split(' ');
                
                if (nums.All(m => (len==-1 || m.Length == len) && Convert.ToInt32(m) >= minNum && Convert.ToInt32(m) <= MaxNum)) list.Add(item);
            }
            return string.Join(split.ToString(), list);
        }

        
        /// <summary>
        /// 验证11选5选择项是否为合法数字
        /// </summary>
        /// <param name="betContent"></param>
        /// <param name="isRepeat">是否允许有重复的数字</param>
        /// <param name="split"></param>
        /// <returns></returns>
        public virtual string VerificationSelectBetContent11x5(string betContent, bool isRepeat, char split = ',')
        {
            return this.VerificationSelectBetContent(betContent, 1, 11, 2, isRepeat, split);
        }

        /// <summary>
        /// 选择项， 多位，万/千/百/十/个，通过,分割，
        /// 每项通过' '分割
        /// </summary>
        /// <returns></returns>
        private  string VerificationSelectBetContentMany(string betContent, int minNum, int maxNum, int len, bool isRepeat, char split)
        {
            var array = betContent.Split(',');
            foreach (var item in array)
            {
                var contentArray = item.Split(split);
                if (!isRepeat)//不允许出现重复的数字 ,.在服务器端出现此情况，绝对是非法请求
                    if (contentArray.Distinct().Count() != contentArray.Length)
                        return "";
                if (contentArray.Any(m => m.Length != len || Convert.ToInt32(m) < minNum || Convert.ToInt32(m) > maxNum)) return string.Empty;
            }
            return betContent;
        }

        /// <summary>
        /// 11x5多位验证
        /// </summary>
        /// <param name="betContent"></param>
        /// <returns></returns>
        public virtual string VerificationSelectBetContent11x5Many(string betContent)
        {
            return this.VerificationSelectBetContentMany(betContent,1,11,2,false,' ');
        }

        /// <summary>
        /// ssc多位验证
        /// </summary>
        /// <param name="betContent"></param>
        /// <returns></returns>
        public virtual string VerificationSelectBetContentSscMany(string betContent)
        {
            return VerificationSelectBetContentMany(betContent,0,9,1,false,' ');
        }

        /// <summary>
        /// k3多位验证 1-6
        /// </summary>
        /// <param name="betContent"></param>
        /// <returns></returns>
        public virtual string VerificationSelectBetContentK3Many(string betContent)
        {
            return this.VerificationSelectBetContentMany(betContent,1, 6, 1, false, ' ');
        }
        
        
        /// <summary>
        /// 验证输入内容是否合法 
        /// 支持0-9组成 通过,分割每项
        /// </summary>
        /// <param name="betContent"></param>
        /// <param name="groupLen"></param>
        /// <param name="minNum"></param>
        /// <param name="maxNum"></param>
        /// <param name="isrept">是否允许有重复，默认不允许</param>
        /// <returns></returns>
        public virtual string VerificationBetContentOneNum(string betContent, int groupLen, int minNum = 0, int maxNum = 9,bool isrept=false)
        {
            var array = betContent.Split(',');
            var list = new List<string>();
            foreach (var item in array)
            {
                if (!isrept && list.Contains(string.Join("", item.OrderBy(f => Convert.ToString(f)))))//是否有重复组合
                    continue;
                var nums = item.Select(x => Convert.ToInt32(x.ToString()));
                if (nums.Distinct().Count() != groupLen) continue;
                if (nums.All(m => (m >= minNum && m <= maxNum))) list.Add(string.Join("",item.OrderBy(f=>Convert.ToString(f))));
            }
            return string.Join(",", list);
        }

        /// <summary>
        /// 验证输入内容是否合法 
        /// </summary>
        /// <param name="betContent"></param>
        /// <param name="groupLen"></param>
        /// <param name="isrept"></param>
        /// <returns></returns>
        public virtual string VerificationBetContentOneNumSsc(string betContent, int groupLen,int len, bool isrept = false,bool isOrderBy=false)
        {

            var array = betContent.Split(',');
            var list = new List<string>();
            foreach (var item in array)
            {
                if (!isrept)//是否有重复组合
                {
                    if (isOrderBy && list.Contains(string.Join("", item.OrderBy(f => Convert.ToString(f)))))
                        continue;
                    else if (list.Contains(item))
                        continue;
                }
                var nums = item.Select(x => Convert.ToInt32(x.ToString()));
                if (nums.Distinct().Count() != groupLen) continue;
                if (nums.All(m =>nums.Count()==len && (m >= 0 && m <= 9)))
                {
                    if (isOrderBy)
                        list.Add(string.Join("", item.OrderBy(f => Convert.ToString(f))));
                    else
                        list.Add(item);
                }
            }
            return string.Join(",", list);

        }


    }
}
