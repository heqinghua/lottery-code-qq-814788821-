using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class MessageList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Bind();
            }

        }

        private void Bind()
        {

            IMessageService messageService = IoC.Resolve<IMessageService>();
            int totalCount = 0;
            var result = messageService.SelectBy(pagerControl.CurrentPageIndex,pagerControl.PageSize,-1,-1,-1, ref totalCount);
            this.repList.DataSource = result;
            this.repList.DataBind();
            this.pagerControl.RecordCount = totalCount;
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void pagerControl_PageChanged(object sender, EventArgs e)
        {
            this.Bind();
        }

        protected void btnDel_Command(object sender, CommandEventArgs e)
        {
            var arg = e.CommandArgument;
            if (null == arg)
                return;
            IMessageService newsService = IoC.Resolve<IMessageService>();
            newsService.Delete(Convert.ToInt32(arg));
            this.Bind();
        }

        public string MessageType(object type)
        {
            if (null == type)
                return string.Empty;
            //1系统消息 2 私人消息 4 中奖消息 8充提信息
            switch (type.ToString())
            {
                case "1":
                    return "系统消息";
                case "2":
                    return "私人消息";
                case "4":
                    return "中奖消息";
                case "8":
                    return "充提信息";
            }
            return string.Empty;
        }


        public string SubStr(object content)
        {
            if (content == null)
                return string.Empty;

            if (content.ToString().Length > 30)
                return content.ToString().Substring(0, 30);
            return content.ToString();
        }
        

    }
}