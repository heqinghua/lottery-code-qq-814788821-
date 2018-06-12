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
    public partial class DnsManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["action"]))
            {

                var dns = Request.Params["dns"];
                var auto = Request.Params["auto"];
                if (string.IsNullOrEmpty(dns))
                {
                    Response.Write("-1");
                }
                else
                {
                    IDnsService dnsService = IoC.Resolve<IDnsService>();
                    switch (Request.Params["action"])
                    {
                        case "add":
                            dnsService.Create(new Ytg.BasicModel.SiteDns()
                            {
                                SiteDnsUrl = dns,
                                OccDate = DateTime.Now,
                                IsShowAutoRegist = auto == "0"?true:false
                            });
                            break;
                        case "update":
                            int id = Convert.ToInt32(Request.Params["id"]);
                            var item = dnsService.Get(id);
                            if (item != null)
                            {
                                item.SiteDnsUrl = dns;
                                item.IsShowAutoRegist = auto == "0" ? true : false;
                            }
                            dnsService.Save();
                            break;
                    }
                    dnsService.Save();
                    Response.Write("0");
                }
                Response.End();
            }

            if (!IsPostBack)
                this.Bind();
        }

        private void Bind()
        {
            IDnsService dnsService = IoC.Resolve<IDnsService>();
            var result= dnsService.GetAll().ToList();
            this.repList.DataSource = result;
            this.repList.DataBind();
        }

        protected void lbBtn_Command(object sender, CommandEventArgs e)
        {
            var cmdResult = e.CommandArgument;
            if (null == cmdResult)
                return;
            IDnsService dnsService = IoC.Resolve<IDnsService>();
            dnsService.Delete(Convert.ToInt32(cmdResult.ToString()));
            dnsService.Save();
            this.Bind();
        }
    }
}