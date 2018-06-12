using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Customer
{
    public partial class EditQuotas : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BuilderQuo();
            }
        }

        /// <summary>
        /// kai hu er
        /// </summary>
        private void BuilderQuo()
        {
            int uid = -1;
            if (!int.TryParse(Request.Params["id"], out uid))
            {
                return;
            }
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
            var result = quotaService.GetUserQuota(uid);
            if (result == null || result.Count() < 1)
                return;
            var childResult = quotaService.GetUserQuota(uid).Select(c => new ListEntity
            {
                QuoId = c.Id,
                QuoType = c.QuotaType,
                ChildQuoValue = c.MaxNum
            }).ToList().Where(x => (Convert.ToDouble(x.QuoType)) <= nowRebate);

           
            this.rpt.DataSource = childResult;
            this.rpt.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int uid = -1;
            if (!int.TryParse(Request.Params["id"], out uid))
            {
                return;
            }
            //存储开户额
            string data =this.hidVal.Value;
            if (string.IsNullOrEmpty(data))
            {
                JsAlert("请填写开户额");
                return;
            }
            var array = data.Split(',');
            int compledCount = 0;
            ISysQuotaService sysQuotaService = IoC.Resolve<ISysQuotaService>();
            foreach (var item in array)
            {
                //处理
                if (string.IsNullOrEmpty(item))
                    continue;
                var res = item.Split('_');
                int qid;//开户额id
                int quoValue;//开户额值
                if (!int.TryParse(res[0], out qid) || !int.TryParse(res[1], out quoValue))
                    continue;
                var quo= sysQuotaService.Get(qid);
                quo.MaxNum=quo.MaxNum+quoValue;
                compledCount ++;
                sysQuotaService.Save();
            }
            if (compledCount > 0)
            {
                JsAlert("开户额调整成功！",true);
            }
            else {
                JsAlert("开户额调整失败，请稍后再试！", true);
            }
        }
    }
}