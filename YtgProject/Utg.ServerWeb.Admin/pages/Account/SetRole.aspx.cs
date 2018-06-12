using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Manager
{
    public partial class SetRole : BasePage
    {
        public int userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["userId"] != null)
                    int.TryParse(Request["userId"].ToString(), out userId);
                this.txtUserId.Value = userId.ToString();
                InitData();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            //获取当前管理员角色
            IAccountRoleService accountRole = IoC.Resolve<IAccountRoleService>();
            List<AccountRole> accountRoleList = accountRole.GetAccountRoleList(userId);
            

            //获取角色列表
            IRoleService roleService = IoC.Resolve<IRoleService>();
            List<Ytg.BasicModel.Role> roleList = roleService.GetAll().ToList();
            if (roleList != null && roleList.Count > 0)
            {
                List<AccountRoleModel> dataSource = new List<AccountRoleModel>();
                foreach (var item in roleList)
                {
                    AccountRoleModel model = new AccountRoleModel();
                    model.RoleId = item.Id;
                    model.RoleName = item.Name;
                    model.Descript = item.Descript;
                    model.IsChecked = false;
                    if (null != accountRoleList.Where(m => m.RoleId == item.Id).FirstOrDefault())
                        model.IsChecked = true;
                    dataSource.Add(model);
                }

                this.repList.DataSource = dataSource;
                this.repList.DataBind();
            }
        }

        //设置角色
        protected void btnSetRole_Click(object sender, EventArgs e)
        {
            userId = Convert.ToInt32(this.txtUserId.Value);
            IAccountRoleService accountRole = IoC.Resolve<IAccountRoleService>();
            List<int> roleIds = new List<int>();
            foreach (RepeaterItem item in repList.Items)
            {
                CheckBox cBox = (CheckBox)item.FindControl("cBox"); 
                if (cBox.Checked == true)
                    roleIds.Add(Convert.ToInt32(cBox.ToolTip));
            }

            if (accountRole.SetAccountRole(userId, roleIds))
            {
                JsAlert("设置成功！", true);
            }
            else
            {
                JsAlert("设置失败，请稍后再试！");
            }
        }

        protected void repList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            AccountRoleModel model = (AccountRoleModel)e.Item.DataItem;
            ((CheckBox)e.Item.FindControl("cBox")).Checked = model.IsChecked;
        }
    }


    public class AccountRoleModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Descript { get; set; }
        public bool IsChecked { get; set; }
    }
}