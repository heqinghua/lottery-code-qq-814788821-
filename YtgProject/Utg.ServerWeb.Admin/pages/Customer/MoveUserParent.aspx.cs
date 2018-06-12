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
    public partial class MoveUserParent : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string result = string.Empty;
            switch (Request.Params["action"])
            {
                case "move":
                    result = MoveUserParentAction();
                    break;
            }
            if (!string.IsNullOrEmpty(result))
            {
                Response.Write(result);
                Response.End();
            }

            if (!IsPostBack)
            {
                this.txtCode.Text = Request.Params["cd"];
                this.txtNickName.Text = Request.Params["nk"];
            }
        }

        private string MoveUserParentAction()
        {
            string uid = Request.Params["uid"];
            string parentUser = Request.Params["user"];
            ISysUserService userService = IoC.Resolve<ISysUserService>();
            var item = userService.Get(Convert.ToInt32(uid));
            if (item == null)
                return "-1";
            var parentUserItem = userService.Get(parentUser);
            if (parentUserItem == null)
            {
                return "-2";
            }

            item.ParentId = parentUserItem.Id;
            userService.Save();
            return "0";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}