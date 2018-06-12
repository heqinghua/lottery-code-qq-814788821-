using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.ServerWeb.BootStrapper;
using Ytg.Service.Logic;
using Ytg.Comm.Security;

namespace Ytg.ServerWeb.Page.PageCode.Messages
{
    public class MessageRequestManager : BaseRequestManager
    {
        private readonly IMessageService mMessageService;

        private readonly IBetDetailService mBetDetailService;

        private readonly ILotteryIssueService mLotteryIssueService;

        public MessageRequestManager(IMessageService messageService,
            IBetDetailService mbetDetailService,ILotteryIssueService lotteryIssueService)
        {
            this.mMessageService = messageService;
            this.mBetDetailService = mbetDetailService;
            this.mLotteryIssueService = lotteryIssueService;
        }

        protected override bool Validation()
        {
            return true;
        }


        protected override void Process()
        {
            switch (this.ActionName.ToLower())
            {
                case "sendmessage":
                    //发送消息
                    this.SendMessage();
                    break;
                case "sendmessagetosubordinate":
                    this.SendMessageToSubordinate();
                    break;
                case "selectmessages":
                    //加载所有消息
                    this.SelectMessages();
                    break;
                case "readmessage"://读取消息
                    this.ReadMessage();
                    break;
                case "loadmessage":
                    this.LoadMessage();
                    break;
                case "noreadwinmsg"://获取未读中奖消息
                    GetNoReadWinMsg();//获取未读中奖消息
                    break;
                case "wintts"://中奖播报数据
                    WinTTS();
                    break;
                case "chartmsg"://聊天消息
                    ChartMsg();
                    break;
                case "delmessage"://删除消息
                    DelMsg();
                    break;
                case "setread":
                    SetReadMsg();//消息标记为已读
                    break;
                case "getonemsg"://根据id获取消息
                    GetOneMsg();
                    break;
            }
        }

        private void GetOneMsg()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Params["mid"]))
                {
                    AppGlobal.RenderResult(ApiCode.Exception);
                    return;
                }
                var id = Convert.ToInt32(Request.Params["mid"]);
                var item = mMessageService.Get(id);
                AppGlobal.RenderResult<Message>(ApiCode.Success, item);
            }
            catch
            {
                AppGlobal.RenderResult(ApiCode.Exception);
            }
            
            
        }

        #region 将消息标记为已读
        private void SetReadMsg()
        {
            int msgId = 0;
            if (!int.TryParse(Request.Params["mid"], out msgId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            var item = this.mMessageService.Get(msgId);
            if (null != item)
            {
                item.Status = 1;
                this.mMessageService.Save();
            }
            AppGlobal.RenderResult(ApiCode.Success);
        }
        #endregion

        #region 删除消息
        private void DelMsg()
        {
            int msgId = 0;
            if (!int.TryParse(Request.Params["id"], out msgId))
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            mMessageService.Delete(msgId);
            AppGlobal.RenderResult(ApiCode.Success);
        }
        #endregion

        #region Action

        private void SendMessage()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var messageContent = Request.QueryString["messageContent"];
                var toUserId = Convert.ToInt32(Request.QueryString["toUserId"]);

                var result = mMessageService.SendMessage(userId, toUserId, messageContent);
                AppGlobal.RenderResult(ApiCode.Success);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SendMessage", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        /// <summary>
        /// 给下级发送消息
        /// </summary>
        private void SendMessageToSubordinate()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var messageContent = Request.QueryString["messageContent"];
                var toUserId = Convert.ToInt32(Request.QueryString["toUserId"]);

                var result = mMessageService.SendMessageToSubordinate(userId, messageContent);
                AppGlobal.RenderResult(ApiCode.Success);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SendMessageToSubordinate", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ReadMessage()
        {
            int userId=this.LoginUserId;
          
            try
            {
                int msgId = 0;
                if (!int.TryParse(Request.Params["mid"], out msgId))
                {
                    AppGlobal.RenderResult(ApiCode.ParamEmpty);
                    return;
                }

                var result = mMessageService.ReadMessage(msgId);
                AppGlobal.RenderResult<List<MessageDTO>>(ApiCode.Success, result);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("ReadMessage", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }
        private void SelectMessages()
        {
            int userId = this.LoginUserId;

            int pageIndex;
            int pageSize;
            int totalCount = 0;
            bool ispageIndex = int.TryParse(Request.Params["pageIndex"], out pageIndex);
            if (!ispageIndex) pageIndex = 1;
            bool ispageSize = int.TryParse(Request.Params["pageSize"], out pageSize);
            if (!ispageSize) pageSize = 20;
            
            try
            {
                var messageType = Convert.ToInt32(Request.Params["messageType"]);
                var status = Convert.ToInt32(Request.Params["status"]);

                var result = mMessageService.SelectBy(pageIndex, pageSize, userId, messageType, status, ref totalCount);
                AppGlobal.RenderResult<List<MessageDTO>>(ApiCode.Success, result, "", pageIndex,totalCount);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("SelectMessages", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        private void LoadMessage()
        {
            int userId;
            bool isUserId = int.TryParse(Request.QueryString["uid"], out userId);
            if (!isUserId)
            {
                AppGlobal.RenderResult(ApiCode.ParamEmpty);
                return;
            }
            try
            {
                var toUserId = Convert.ToInt32(Request.QueryString["toUserId"]);
                var result = mMessageService.LoadMessage(userId, toUserId);
                AppGlobal.RenderResult<List<MessageDTO>>(ApiCode.Success, result);
            }
            catch (Exception ex)
            {
                Ytg.Scheduler.Comm.LogManager.Error("LoadMessage", ex);
                AppGlobal.RenderResult(ApiCode.Exception);
            }
        }

        #endregion

        #region 获取系统中奖消息

        /// <summary>
        /// 获取开奖未读消息
        /// </summary>
        private void GetNoReadWinMsg()
        {
            var source = this.mMessageService.RefUserMessage(LoginUserId.ToString());
            //每次最多取5条数据
            if (null != source && source.Count() > 0)
            {
                string updateReadIds = "";
                foreach (var s in source)
                {
                    updateReadIds +=s.Id+",";
                }
                if (!string.IsNullOrEmpty(updateReadIds))
                {
                    updateReadIds = updateReadIds.Substring(0, updateReadIds.Length - 1);
                    //修改为已读
                    this.mMessageService.UpdateReadMessgae(updateReadIds);
                }
                AppGlobal.RenderResult<List<Message>>(ApiCode.Success, source);
            }
            else
            {
                AppGlobal.RenderResult(ApiCode.Fail);
            }
        }
        #endregion

        #region 
        
        private void WinTTS()
        {
            var sings = FormsPrincipal<CookUserInfo>.TryParsePrincipal(Request);
            if (null == sings)
            {
                AppGlobal.RenderResult(ApiCode.ExitLogin);
                return;
            }
            var userdata = sings.UserData;
            if (null == userdata)
            {
                AppGlobal.RenderResult(ApiCode.ExitLogin);
                return;
            }

            //必须验证用户登录

            //真是中奖数据
            var result = mBetDetailService.GetRecentlyWin();
            if (null == result)
            {
                result = new List<RecentlyWinDTO>();
            }
            var wins = Ytg.ServerWeb.BootStrapper.SiteHelper.GetWinsMonery();

            if (wins != null)
            {
                int length = wins.Count;//虚拟数据总条数
                var config = SiteHelper.GetLotteryIdName();
                var keyArray = config.Keys.ToList();
                for (var i = 0; i < keyArray.Count; i++)
                {
                    var configIdStr = keyArray[i];
                    var opendIssue = this.mLotteryIssueService.GetTop5OpendIssue(configIdStr);
                    if (opendIssue.Count < 1)
                        continue;

                    foreach (var tt in opendIssue)
                    {
                        if (wins.Count > 0)
                        {
                            var fs = wins[0];
                            result.Add(new RecentlyWinDTO()
                            {
                                Code = fs.UserCode,
                                WinMoney = fs.UserWinMonery,
                                IssueCode = tt.IssueCode,
                                LotteryName = config[configIdStr]
                            });
                            wins.RemoveAt(0);
                        }
                    }

                }

            }

            result = result.OrderBy(x => x.Code).ToList();


            AppGlobal.RenderResult<List<RecentlyWinDTO>>(ApiCode.Success, result);
        }
        #endregion

        #region 

        private void ChartMsg()
        {
            int state = -1;
            if (!int.TryParse(Request.Params["state"], out state))
                state = -1;
            int pageIndex = 1;
            if (!int.TryParse(Request.Params["pageindex"], out pageIndex))
                pageIndex = 1;

            int fid=-1;
            //if (!int.TryParse(Request.Params["fid"], out fid))
            //{
            //    AppGlobal.RenderResult(ApiCode.ParamEmpty);
            //    return;
            //}
            DateTime? lastdate = null;
            DateTime outDate;
            if (DateTime.TryParse(Request.Params["lastdate"], out outDate))
                lastdate = outDate;

            int pageCount = 0;
            int totalCount = 0;

            var source = this.mMessageService.GetChartMsg(LoginUserId,fid,state,lastdate,pageIndex, ref pageCount, ref totalCount);
            AppGlobal.RenderResult<List<Message>>(ApiCode.Success, source, "", pageCount, totalCount);
        }

        #endregion
    }
}