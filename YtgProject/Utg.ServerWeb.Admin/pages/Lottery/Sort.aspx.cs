using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Utg.ServerWeb.Admin.pages.Lottery
{
    public partial class Sort : BasePage
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
            int sort = 0;

            bool isInt = false;
            for (int i = 0; i < this.repList.Items.Count; i++)
            {
                TextBox tb = (TextBox)repList.Items[i].FindControl("txtSort");
                if (tb != null)
                {
                    int id = Convert.ToInt32(tb.ToolTip);
                    if (id == lotteryId)
                    {
                        isInt = Utils.IsInt(tb.Text.Trim());
                        if (isInt)
                            sort = Convert.ToInt32(tb.Text.Trim());

                        break;
                    }
                }
            }

            if (isInt == false)
            {
                JsAlert("请输入正整数！");
                return;
            }

            bool result = lotteryTypeService.Sort(lotteryId, sort);
            if (result == true)
            {
                InitData();
                JsAlert("修改成功！");
            }
            else
            {
                JsAlert("修改失败！");
            }
        }
        public string SetActive(string mLotteryCode)
        {
            if (mLotteryCode == this.lotteryCode)
                return "btn btn-primary";
            return "btn";
        }
    }
}