using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Utg.ServerWeb.Admin.pages.Lottery
{
    public partial class Switch : BasePage
    {
        private ILotteryTypeService lotteryTypeService = null;
        public List<GroupNameType> GroupNameTypes = new List<GroupNameType>();

        public string lotteryCode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            lotteryTypeService = IoC.Resolve<ILotteryTypeService>();
            if (!IsPostBack)
            {
                if (Request["lotteryCode"] != null)
                    lotteryCode = Request["lotteryCode"].ToString();
                txtLotteryCode.Value = lotteryCode;

                InitData();
            }
        }

        public void InitData()
        {
            IGroupNameTypeService groupService = IoC.Resolve<IGroupNameTypeService>();
            GroupNameTypes = groupService.GetAll().OrderBy(x => x.OrderNo).ToList();
            int gid = GroupNameTypes.FirstOrDefault().Id;
            if (string.IsNullOrEmpty(lotteryCode))
                lotteryCode = gid.ToString();
            gid = Convert.ToInt32(lotteryCode);
            this.repList.DataSource = lotteryTypeService.GetAll().Where(x => x.GroupName == gid).ToList();
            this.repList.DataBind();

        }

        protected void btnUpdate_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandArgument == null)
            {
                JsAlert("修改失败！");
                return;
            }

            int lotteryId = Convert.ToInt32(e.CommandArgument);
            bool isCheck = false;
            TimeSpan? beginDate = null;
            TimeSpan? endDate = null;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)repList.Items[i].FindControl("cBox");
                TextBox txtBegin = (TextBox)repList.Items[i].FindControl("txtBegin");
                TextBox txtEnd = (TextBox)repList.Items[i].FindControl("txtEnd");

                if (chk != null && txtBegin != null && txtEnd != null)
                {
                    int id = Convert.ToInt32(chk.ToolTip);
                    if (id == lotteryId)
                    {
                        isCheck = chk.Checked;
                        TimeSpan b;
                        TimeSpan ed;
                        if (TimeSpan.TryParse(txtBegin.Text.Trim(), out b) && TimeSpan.TryParse(txtEnd.Text.Trim(), out ed))
                        {
                            beginDate = b;
                            endDate = ed;
                        }
                        break;
                    }
                }
            }
            var item = lotteryTypeService.Get(lotteryId);
            if (item == null)
            {
                JsAlert("参数错误！");
                return;
            }
            item.IsEnable = isCheck;
            item.BeginScallDate = beginDate;
            item.endSAcallDate = endDate;
            lotteryTypeService.Save();
            JsAlert("保存成功！");
        }

        public string SetActive(string mLotteryCode)
        {
            if (mLotteryCode == this.lotteryCode)
                return "btn btn-primary";
            return "btn";
        }
    }
}