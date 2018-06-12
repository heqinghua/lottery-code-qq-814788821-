using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;

namespace Ytg.Service
{
    public class MentionQueusService : CrudService<Ytg.BasicModel.MentionQueu>, Ytg.Core.Service.IMentionQueusService
    {
        log4net.ILog log = null;
        public MentionQueusService(IRepo<MentionQueu> repo)
            : base(repo)
        {
            log = log4net.LogManager.GetLogger("errorLog");
        }

        public List<MentionDTO> SelectBy(string userCode, string mentionCode, string sDate, string eDate, decimal beginMonery, decimal endMonery, int type, int pageIndex, ref int totalCount)
        {
            string moneryWhere = string.Empty;
            if (beginMonery > 0 && endMonery > 0)
            {
                moneryWhere += string.Format(" and m.MentionAmt between {0} and {1}", beginMonery, endMonery);
            }
            if (type != -1)
                moneryWhere += string.Format(" and m.Status={0}", type);
            string sql = string.Format(@"SELECT m.Id, m.MentionAmt,m.QueuNumber,m.MentionCode,m.SendTime,m.Poundage,m.RealAmt,m.Audit,(CASE m.Status WHEN 1 THEN '提现成功' when 2 then '提现失败' when 3 then '撤销' ELSE '处理中' END) IsEnableDesc,m.OccDate,
	u.Code,u.Sex,ub.BankNo,ub.BankOwner,b.BankName,b.BankWebUrl,ub.Branch,p.ProvinceName,c.CityName
FROM dbo.MentionQueus m
LEFT JOIN dbo.SysYtgUser u ON u.Id=m.UserId
LEFT JOIN dbo.SysUserBanks ub ON ub.Id=m.UserBankId
LEFT JOIN dbo.SysBankTypes b ON b.Id=ub.BankId
LEFT JOIN dbo.S_Province p ON p.ProvinceID=ub.ProvinceId
LEFT JOIN dbo.S_City c ON c.CityID= ub.CityId
where  ('{0}'='' or u.Code='{0}') and ('{1}'='' or m.MentionCode='{1}') {2} and m.SendTime>'{3}' {4}", userCode, mentionCode, moneryWhere, sDate, string.IsNullOrEmpty(eDate) ? "" : string.Format("and m.SendTime<'{0}'", eDate));
            sql = "("+sql+") as t1";
           
            int pageCount = 0;
            return this.GetEntitysPage<MentionDTO>(sql, "OccDate", "*", "OccDate DESC", ESortType.DESC, pageIndex, AppGlobal.ManagerDataPageSize, ref pageCount, ref totalCount);
        }


        public bool MentionDone(int id, int status, decimal realAmt, decimal poundage, string errorMsg)
        {
            var item = this.Get(id);
            item.ErrorMsg = errorMsg;
            if (null == item || status > 3)
                return false;
            item.Status = status;
            item.SendTime = DateTime.Now;
            if (status == 1)
            {
                item.RealAmt = realAmt;
                item.Poundage = poundage;
                
            }
            try
            {
                this.Save();
                //存储消息
                string title = string.Empty;
                string message = string.Empty;
                TradeType tradeType = TradeType.提现失败;
                switch (item.Status)
                {
                    case 1:

                        title = string.Format("编号为【{0}】提现成功", item.MentionCode);
                        message = string.Format("编号为【{0}】的提现申请已成功处理，请注意查看您的帐变信息，如有任何疑问请联系在线客服。", item.MentionCode);
                        break;
                    case 2:
                        title = string.Format("编号为【{0}】的提现申请失败", item.MentionCode);
                        message = string.Format("编号为【{0}】的提现申请失败！原因【"+ errorMsg + "】，如有任何疑问请联系在线客服。", item.MentionCode);
                        break;
                    case 3:
                        title = string.Format("编号为【{0}】的提现申请已撤销", item.MentionCode);
                        message = string.Format("编号为【{0}】的提现申请已撤销，如果有问题请联系我们的在线客服。", item.MentionCode);
                        tradeType = TradeType.撤销提现;
                        break;
                }

                //插入账变,提现失败，返回账号
                if (item.Status != 1)
                {
                    ISysUserBalanceService userBalanceService = IoC.Resolve<ISysUserBalanceService>();
                    userBalanceService.UpdateUserBalance(new SysUserBalanceDetail()
                    {
                        RelevanceNo = item.MentionCode,
                        SerialNo = "d" + Utils.BuilderNum(),
                        TradeAmt = realAmt,
                        TradeType = tradeType,
                        UserId = item.UserId,
                    }, item.MentionAmt);

                }

                //提现成功，修改排队记录
                if (item.Status == 1)
                {
                    string sql = "update MentionQueus set QueuNumber=QueuNumber-1  where Status=0 and QueuNumber>0";
                    this.mRepo.GetDbContext.Database.ExecuteSqlCommand(sql);
                }

                //插入消息
                IMessageService messageServer = IoC.Resolve<IMessageService>();
                messageServer.Create(new Message()
                {
                    FormUserId = -1,
                    MessageContent = message,
                    MessageType = 1,
                    Status = 0,
                    Title = title,
                    ToUserId = item.UserId
                });
                messageServer.Save();
                //修改提款限制为0
                ISysUserService userService= IoC.Resolve<ISysUserService>();
                userService.DefaultUserMinMinBettingMoney(item.UserId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("MentionDone", ex);
                return false;
            }
        }

        /// <summary>
        /// 提现审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Audit(int id)
        {
            MentionQueu mentionQueu = this.Get(id);
            if (null == mentionQueu)
                return false;
            if (mentionQueu.Audit != 0 || mentionQueu.MentionAmt < 5000)
                return false;
            mentionQueu.Audit = 1;
            this.Save();
            return true;
        }
    }
}
