using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class MessageEdit : BasePage
    {
        private int messageId = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"], out messageId))
                messageId = -1;

            if (!IsPostBack)
            {
                this.Bind();
            }
        }

        private void Bind()
        {
            if (messageId < 0)
                return;
            palel.Visible = false;
            this.btnSave.Text = "保存";
            IMessageService messageService = IoC.Resolve<IMessageService>();
            var item = messageService.Get(this.messageId);
            if (null == item)
                return;
            this.txtTitle.Text = item.Title;
            this.txtContent.Text = item.MessageContent;
            this.drpIsShowDialog.SelectedValue = item.MessageType.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            IMessageService messageService = IoC.Resolve<IMessageService>();
            //保存或修改
            Ytg.BasicModel.Message msg = null;
            if (this.messageId > 0)
                msg = messageService.Get(messageId);
            else
                msg = new Ytg.BasicModel.Message();

            msg.Title = this.txtTitle.Text.Trim();
            msg.MessageContent = this.txtContent.Text.Trim();
            msg.MessageType =Convert.ToInt32(this.drpIsShowDialog.SelectedValue);

            if (this.messageId > 0)
            {
                messageService.Save();
                JsAlert("保存成功！");
            }
            else
            {
                messageService.InstatMessage(msg.MessageType, msg.Title, msg.MessageContent, Convert.ToInt32(this.drpTo.SelectedValue));
                JsAlert("消息发送成功！");
            }
            
            

        }
    }
}