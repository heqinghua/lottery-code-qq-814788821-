using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Utg.ServerWeb.Admin.pages.Lottery
{
    public partial class Time : BasePage
    {
        public int lotteryId = 0;
        public List<Ytg.BasicModel.LotteryType> lotteryTypeList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["lotteryId"] != null)
                    int.TryParse(Request["lotteryId"].ToString(), out lotteryId);
                txtLotteryId.Value = lotteryId.ToString();
                
                InitData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            //获取彩种列表
            ILotteryTypeService lotteryTypeService = IoC.Resolve<ILotteryTypeService>();
            lotteryTypeList = lotteryTypeService.GetAll().ToList();

            //根据彩种获取玩法列表
            if (lotteryTypeList != null && lotteryTypeList.Count > 0)
            {
                //默认显示彩种
                if (lotteryId == 0)
                    lotteryId = lotteryTypeList[0].Id;

                string lotteryName = "";
                LotteryType lotteryType = lotteryTypeList.Where(m => m.Id == lotteryId).FirstOrDefault();
                if (lotteryType != null)
                {
                    lotteryName = lotteryType.LotteryName;
                    lotteryId = lotteryType.Id;
                }

                List<LotteryIssueModel> lotteryIssueModelList = new List<LotteryIssueModel>();
                ILotteryIssueService lotteryIssueService = IoC.Resolve<ILotteryIssueService>();
                List<LotteryIssue> lotteryIssueList = lotteryIssueService.GetNowDayIssue(lotteryId).ToList();
                if (lotteryIssueList != null && lotteryIssueList.Count > 0)
                {
                    foreach (var item in lotteryIssueList)
                    {
                        LotteryIssueModel model = new LotteryIssueModel();
                        model.Id=item.Id;
                        model.IssueCode = item.IssueCode;
                        model.StartTime = item.StartTime;
                        model.EndTime = item.EndTime;
                        model.LotteryTime = item.LotteryTime;
                        model.EndSaleTime = item.EndSaleTime;
                        model.LotteryId = lotteryId;
                        model.LotteryName = lotteryName;
                        lotteryIssueModelList.Add(model);
                    }
                }
                this.repList.DataSource = lotteryIssueModelList;
                this.repList.DataBind();
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
            {
                JsAlert("修改失败！");
                return;
            }

            lotteryId = Convert.ToInt32(txtLotteryId.Value);
            int lotteryIssueId = Convert.ToInt32(e.CommandArgument);
            string startTime = "";
            string endTime = "";
            string lotteryTime = "";

            //获取修改行数据
            bool flag = false;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                LinkButton lbtn = (LinkButton)repList.Items[i].FindControl("btnUpdate");
                if (lbtn != null)
                {
                    int id = Convert.ToInt32(lbtn.CommandArgument);
                    if (id == lotteryIssueId)
                    {
                        flag = true;
                        startTime = ((TextBox)repList.Items[i].FindControl("txtStartTime")).Text;
                        endTime = ((TextBox)repList.Items[i].FindControl("txtEndTime")).Text;
                        lotteryTime = ((TextBox)repList.Items[i].FindControl("TxtLotteryTime")).Text;
                        break;
                    }
                }
            }


            //修改数据
            if (flag)
            {
                ILotteryIssueService lotteryIssueService = IoC.Resolve<ILotteryIssueService>();
                var item = lotteryIssueService.Get(lotteryIssueId);

                item.StartSaleTime = Convert.ToDateTime(item.StartTime.ToString("yyyy/MM/dd") + " " + startTime);
                item.EndSaleTime = Convert.ToDateTime(item.EndTime.Value.ToString("yyyy/MM/dd") + " " + endTime);
                item.EndTime = Convert.ToDateTime(item.LotteryTime.Value.ToString("yyyy/MM/dd") + " " + lotteryTime);

                lotteryIssueService.Save();

                InitData();
                JsAlert("修改成功！");

            }
        }

        public string SetActive(int mLotteryId)
        {
            if (mLotteryId == this.lotteryId)
                return "btn btn-primary";
            return "btn";
        }
    }

    public class LotteryIssueModel : LotteryIssue
    {
        public string LotteryName { get; set; }
    }
}