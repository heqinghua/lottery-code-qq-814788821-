using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Mobile.userCenter
{
    public partial class OpenQuota : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int uid;
                if (!int.TryParse(Request.Params["uid"], out uid))
                {
                    Response.End();
                    return;
                }

               
                BuilderQuo(uid);
            }
        }

        /// <summary>
        /// kai hu er
        /// </summary>
        private void BuilderQuo(int uid)
        {
            ISysUserService userServices = IoC.Resolve<ISysUserService>();
            var user = userServices.Get(uid);
            if (null == user)
            {
                Response.End();
                return;
            }
            lbCode.Text = user.Code;
            var nowRebate = Math.Round(Utils.MaxRemo - user.Rebate, 1);
            lbBackNum.Text = nowRebate.ToString();

            ISysQuotaService quotaService = IoC.Resolve<ISysQuotaService>();
            var result = quotaService.GetUserQuota(CookUserInfo.Id);
            if (result == null || result.Count() < 1)
                return;
            var childResult = quotaService.GetUserQuota(uid).Select(c => new ListEntity
            {
                QuoId=c.Id,
                QuoType = c.QuotaType,
                ChildQuoValue = c.MaxNum
            }).ToList().Where(x => (Convert.ToDouble(x.QuoType)) <= nowRebate);

            //组织数据
            foreach (var cd in result)
            {
                var fs = childResult.Where(x => x.QuoType == cd.QuotaType).FirstOrDefault();
                if (fs != null)
                {
                    fs.QuoValue = cd.MaxNum;
                }
            }
            this.rpt.DataSource = childResult.OrderByDescending(x=>x.QuoType);
            this.rpt.DataBind();
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }

    public class ListEntity
    {
        public int QuoId { get; set; }

        public string QuoType { get; set; }

        public double QuoValue { get; set; }

        public double ChildQuoValue { get; set; }

        
    }
}