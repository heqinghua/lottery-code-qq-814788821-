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
    public partial class Number : BasePage
    {
        public string lotteryCode = "";
        public int lotteryId = 0;
        public List<Ytg.BasicModel.LotteryType> lotteryTypeList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["lotteryCode"] != null)
                    lotteryCode = Request["lotteryCode"].ToString();
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
                //默认加载第一个彩种
                if (string.IsNullOrEmpty(lotteryCode))
                    lotteryCode = lotteryTypeList[0].LotteryCode;

                
                LotteryType lotteryType = lotteryTypeList.Where(m => m.LotteryCode == lotteryCode).FirstOrDefault();
                if (lotteryType == null)
                    return;

                string lotteryName = lotteryType.LotteryName;
                txtLotteryCode.Value = lotteryType.LotteryCode;
                txtLotteryId.Value = lotteryType.Id.ToString();

                List<LotteryIssueModel> lotteryIssueModelList = new List<LotteryIssueModel>();
                ILotteryIssueService lotteryIssueService = IoC.Resolve<ILotteryIssueService>();
                List<LotteryIssue> lotteryIssueList = lotteryIssueService.GetNowDayIssue(lotteryType.Id).ToList().OrderBy(x=>x.IssueCode).ToList();
                if (lotteryIssueList != null && lotteryIssueList.Count > 0)
                {
                    foreach (var item in lotteryIssueList)
                    {
                        LotteryIssueModel model = new LotteryIssueModel();
                        model.Id = item.Id;
                        model.IssueCode = item.IssueCode;
                        model.Result = item.Result;
                        model.LotteryTime = item.LotteryTime;
                        model.LotteryId = lotteryType.Id;
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

            lotteryCode = txtLotteryCode.Value.ToString();
            lotteryId = Convert.ToInt32(txtLotteryId.Value);
            int lotteryIssueId = Convert.ToInt32(e.CommandArgument);
            string issueCode = "";
            string result = "";

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
                        issueCode = ((Label)repList.Items[i].FindControl("txtIsscueCode")).Text.Trim();
                        result = ((TextBox)repList.Items[i].FindControl("txtResult")).Text.Trim();
                        break;
                    }
                }
            }


            //修改数据
            if (flag)
            {
                ILotteryIssueService lotteryIssueService = IoC.Resolve<ILotteryIssueService>();
                if (lotteryIssueService.UpdateOpenResult(issueCode,result,DateTime.Now, lotteryId))
                {
                    InitData();
                    JsAlert("修改成功！");
                }
                else
                {
                    JsAlert("修改失败！");
                }
            }
        }

        public string SetActive(string mLotteryCode)
        {
            if (mLotteryCode == this.lotteryCode)
                return "btn btn-primary";
            return "btn";
        }

        protected void btnOpen_Command(object sender, CommandEventArgs e)
        {
            this.lotteryCode=txtLotteryCode.Value;
            InitData();
        }
    }
}