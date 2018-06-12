using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class NewsEdit : BasePage
    {
        private int user_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!int.TryParse(Request.Params["id"], out user_id))
                user_id = -1;
            if (!IsPostBack)
            {

                this.Bind();
            }
        }

        private void Bind()
        {
            if (user_id <= 0)
                return;
            INewsService userService = IoC.Resolve<INewsService>();
            var item = userService.Get(this.user_id);
            if (null == item)
            {
                Response.End();
                return;
            }
            this.txtTitle.Text = item.Title;
            this.drpIsShowDialog.SelectedValue = item.IsShow.ToString();
            this.txtContent.Text =System.Web.HttpUtility.UrlDecode( item.Content);
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SysNews user = null;
            INewsService userService = IoC.Resolve<INewsService>();
            if (user_id > 0)//修改
                user = userService.Get(this.user_id);
            else
            {
                user = new SysNews();
            }

            if (this.txtTitle.Text.Trim()=="")
            {
                Warning("请输入新闻标题！");
                return;
            }
            if (this.txtContent.Text.Trim()=="")
            {
                Warning("请输入新闻内容！");
                return;
            }

            user.Title = this.txtTitle.Text.Trim();
            user.Content = this.txtContent.Text.Trim();
            user.IsShow =Convert.ToInt32(drpIsShowDialog.SelectedValue);

            bool isCompled = false;
            if (user_id > 0)
            {
                userService.Save();
                isCompled = true;
                this.txtContent.Text = System.Web.HttpUtility.UrlDecode(user.Content);
            }
            else
            {
                isCompled = userService.AddNews(user);
            }
            if (isCompled)
            {
                JsAlert("保存成功！", true);
              
                if (user_id > 0)
                {
                 //   Response.Redirect("/pages/NewsList.aspx");
                    JsAlert("保存成功！");
                    
                }
                if (user_id <= 0)
                {
                    this.txtTitle.Text = string.Empty;
                    this.txtContent.Text = string.Empty;
                }
            }
            else
            {
                JsAlert("保存失败，请稍后再试！");
                //
            }
        }
    }
}