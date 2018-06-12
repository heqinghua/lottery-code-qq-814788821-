using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Customer
{
    public partial class UserList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["ajx"] == "group")
            {

                ISysUserService sysUserService = IoC.Resolve<ISysUserService>();
                decimal? rendValue = sysUserService.GroupUserAmt(Convert.ToInt32(Request.Params["uid"]));
                Response.Write(rendValue);
                Response.End();
                return;
            }
            if (!IsPostBack)
            {

                this.BindList();
            }


        }

        public bool GetCaiwu1()
        {
            return this.LoginUser.Code.ToLower() == "caiwu1";
        }

        private void BindList(bool isCli=false)
        {
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            int userType = Convert.ToInt32(drpuserType.SelectedValue);
            int userstate = Convert.ToInt32(drpuserState.SelectedValue);
            int totalCount = 0;
            double? beginQuo = null;
            double? endQuo = null;
            double b = 0;
            double e = 0;
            if (double.TryParse(this.txtBeginRebate.Text.Trim(), out b) && double.TryParse(this.txtEndRebate.Text.Trim(), out e))
            {
                beginQuo = 8.0 - b;
                endQuo = 8.0 - e;
                if (beginQuo < 0 || endQuo < 0)
                {
                    beginQuo = null;
                    endQuo = null;
                }
            }
            //clear()
            if (isCli)
            {
                if (string.IsNullOrEmpty(txtNickName.Text.Trim())
               && string.IsNullOrEmpty(txtUserCode.Text.Trim())
               && drpuserState.SelectedValue == "-1"
               && drpuserType.SelectedValue == "-1"
               && string.IsNullOrEmpty(txtBeginRebate.Text.Trim())
               && string.IsNullOrEmpty(txtEndRebate.Text.Trim()))
                    SetCurParentId(null);
                else
                    SetCurParentId(-1);
            }

            var result = userService.SelectBy(this.txtUserCode.Text.Trim(), userType, this.txtNickName.Text.Trim(), userstate, beginQuo, endQuo, GetCurParentId(), pagerControl.CurrentPageIndex, pagerControl.PageSize, ref totalCount);
            this.pagerControl.RecordCount = totalCount;
            this.repList.DataSource = result;
            this.repList.DataBind();
            BindLink();

        }

        public bool GetSex(object obj) {
            if (null == obj)
                return false;
            if (obj.ToString() == "1")
                return true;
            return false;
        }

        private int? GetCurParentId()
        {
            return ViewState["parent"] == null ? (int?)null : Convert.ToInt32(ViewState["parent"]);
        }

        private void SetCurParentId(int? parentId)
        {
            ViewState["parent"] = parentId;
        }

        private List<CatchEntity> GetClickList()
        {
            if (ViewState["catch_Entity"] == null)
                return new List<CatchEntity>();
            return ViewState["catch_Entity"] as List<CatchEntity>;
        }

        private void SaveClickList(List<CatchEntity> source)
        {
            ViewState["catch_Entity"] = source;
        }

        public string ToUserStateString(string state)
        {
            string toStr = "普通会员";
            switch (state)
            {
                case "Proxy":
                    toStr = "代理";
                    break;
                case "BasicProy":
                    toStr = "总代理";
                    break;
                case "Main":
                    toStr = "主管";
                    break;

            }
            return toStr;
        }

        public bool IsEnabe(object id)
        {
            string pid = GetCurParentId() == null ? "-1" : GetCurParentId().Value.ToString();
            return !((id.ToString() == hidNowParentId.Value) || pid == id.ToString());

        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
           // this.SaveClickList(new List<CatchEntity>());
            this.BindList(false);
           
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.SaveClickList(new List<CatchEntity>());
            this.BindList(true);
           
        }

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (sender as CheckBox);
            int id = Convert.ToInt32(chk.ToolTip);
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            if (chk.Checked)//禁用
                userService.Disable(id);
            else
                userService.Enable(id);
            this.BindList();
        }
        //资金
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (sender as CheckBox);
            int id = Convert.ToInt32(chk.ToolTip);
            ISysUserBalanceService userService = IoC.Resolve<ISysUserBalanceService>();

            if (chk.Checked)//禁用
                userService.Frozen(id);
            else
                userService.UnFrozen(id);
            this.BindList();
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {
            this.BindList();
        }

        private void EmptyResult()
        {
            this.txtUserCode.Text = string.Empty;
            this.txtNickName.Text = string.Empty;
            this.txtEndRebate.Text = "";
            this.txtBeginRebate.Text = "";
            this.drpuserState.SelectedValue = "-1";
            this.drpuserType.SelectedValue = "-1";
        }

        protected void lkbChildren_Click(object sender, EventArgs e)
        {
            EmptyResult();

            var lkButton = (sender as LinkButton);
            if (null == lkButton || lkButton.CommandArgument == null)
            {
                return;
            }
            int pid = Convert.ToInt32(lkButton.CommandArgument.ToString());
            SetCurParentId(pid);
            var newSource = new List<CatchEntity>();

            ISysUserService userService = IoC.Resolve<ISysUserService>();
            var rx = userService.GetParentUsers(Convert.ToInt32(pid));
            foreach (var item in rx)
            {
                newSource.Add(new CatchEntity()
                {
                    Code = item.Code,
                    Uid = item.Id
                });
            }
            newSource = newSource.OrderBy(x => x.Uid).ToList();

            this.SaveClickList(newSource);
            this.BindList();
            BindLink();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {EmptyResult();
            var parentId = this.hidNowParentId.Value;//当前点击的数据
            var newSource = new List<CatchEntity>();
            if (!string.IsNullOrEmpty(parentId))
            {
                ISysUserService userService = IoC.Resolve<ISysUserService>();
                var rx = userService.GetParentUsers(Convert.ToInt32(parentId));
                foreach (var item in rx)
                {
                    //if (item.Id == Convert.ToInt32(parentId))
                    //    continue;
                    newSource.Add(new CatchEntity()
                    {
                        Code = item.Code,
                        Uid = item.Id
                    });
                }
            }
            newSource = newSource.OrderBy(x => x.Uid).ToList() ;
            int? pid = null;
            if (!string.IsNullOrEmpty(parentId))
                pid = Convert.ToInt32(parentId);
            this.SetCurParentId(pid);
            this.SaveClickList(newSource);
            BindLink();
            this.BindList();
        }

        private void BindLink()
        {
            this.rptLink.DataSource = this.GetClickList();
            this.rptLink.DataBind();
        }

        protected void lbkDel_Command(object sender, CommandEventArgs e)
        {
            //var cmdArg = e.CommandArgument;
            //if (cmdArg == null)
            //    return;
            //try
            //{
            //    ISysUserService userService = IoC.Resolve<ISysUserService>();
            //    userService.Delete(Convert.ToInt32(cmdArg.ToString()));
            //    userService.Save();
            //    this.BindList();
            //    JsAlert("删除成功！");
            //}
            //catch {
            //    JsAlert("删除失败，请先确保该账户下无其他下级用户！");
            //}


            var cmdArg = e.CommandArgument;
            if (cmdArg == null)
                return;

            int userid = Convert.ToInt32(cmdArg.ToString());
            try
            {
                ISysUserService userService = IoC.Resolve<ISysUserService>();
                //严重当前会员是否存在下级
                SysUser user = userService.Where(m => m.ParentId == userid).FirstOrDefault();
                if (user == null)
                {
                    int result = userService.delCustomer(Convert.ToInt32(cmdArg.ToString()));
                    if (result == 0)
                    {
                        JsAlert("删除失败！");
                    }
                    else
                    {
                        this.BindList();
                        JsAlert("删除成功！");
                    }
                }
                else
                {
                    JsAlert("当前账号存在下级，不允许删除！");
                }
            }
            catch
            {
                JsAlert("删除失败，请先确保该账户下无其他下级用户！");
            }
        }

        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {
            var cmdArg = e.CommandArgument;
            if (cmdArg == null)
                return;

            int userid = Convert.ToInt32(cmdArg.ToString());
            try
            {
                ISysUserService userService = IoC.Resolve<ISysUserService>();
                //严重当前会员是否存在下级
               var rowCount= userService.DefaultUserMinMinBettingMoney(userid);
                if (rowCount > 0)
                {
                    JsAlert("处理成功！");
                }
                else
                {
                    JsAlert("用户不存在！");
                }
            }
            catch
            {
                JsAlert("取款限制取消失败，请稍后再试！");
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (sender as CheckBox);
            int id = Convert.ToInt32(chk.ToolTip);
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            if (chk.Checked)//禁用
                userService.InputMobile(id);
            else
                userService.InputNotMobile(id);
            this.BindList();
        }

        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
            var cmdArg = e.CommandArgument;
            if (cmdArg == null)
                return;

            int userid = Convert.ToInt32(cmdArg.ToString());
            try
            {
                ISysUserService userService = IoC.Resolve<ISysUserService>();
                //严重当前会员是否存在下级
                SysUser user = userService.Get(userid);
                if (user != null)
                {
                    user.MoTel = "";
                    user.Qq = "";
                    userService.Save();
                    JsAlert("处理成功！");
                    this.BindList();
                }
                else
                {
                    JsAlert("用户不存在！");
                }
            }
            catch
            {
                JsAlert("数据清空失败，请稍后再试！");
            }
        }

        protected void chktest_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (sender as CheckBox);
            int id = Convert.ToInt32(chk.ToolTip);
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            if (chk.Checked)//禁用
                userService.SetTest(id);
            else
                userService.SetUnTest(id);
            this.BindList();
        }
    }
    [Serializable]
   public class CatchEntity {
        public int Uid { get; set; }

        public string Code { get; set; }
    } 
}