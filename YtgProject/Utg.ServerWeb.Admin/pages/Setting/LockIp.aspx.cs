using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Setting
{
    public partial class LockIp : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["action"] == "lock")
            {
                if (string.IsNullOrEmpty(Request.Params["ip"]))
                {
                    Response.Write("-1");
                    Response.End();
                }
                ILockIpInfoService lockIpInfoService = IoC.Resolve<ILockIpInfoService>();
                lockIpInfoService.Create(new Ytg.BasicModel.LockIpInfo()
                {
                    Ip = Request.Params["ip"],
                    LockReason = Request.Params["des"],
                    OccDate = DateTime.Now,
                    IpCityName = Utils.GetCityByIp(Request.Params["ip"])
                });
                lockIpInfoService.Save();
                Response.Write("0");
                Response.End();
            }
            if (!IsPostBack)
            {
                this.Bind();
            }
        }

        private void Bind()
        {
            ILockIpInfoService lockIpInfoService = IoC.Resolve<ILockIpInfoService>();
            this.repList.DataSource = lockIpInfoService.GetAll().ToList() ;
            this.repList.DataBind();
        }


        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            var cmd = e.CommandArgument;
            if (null == cmd)
            {
                JsAlert("操作失败！");
                return;
            }
            ILockIpInfoService lockIpInfoService = IoC.Resolve<ILockIpInfoService>();
            lockIpInfoService.Delete(Convert.ToInt32(cmd.ToString()));
            lockIpInfoService.Save();
            JsAlert("删除成功");
            this.Bind();
        }
    }
}