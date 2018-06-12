using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ytg.ServerWeb.Views.Help
{
    public partial class Help_ : BasePage
    {
       public string helpTitle = "";
        public string helpText = "";
        public string action = "zc";

        public Dictionary<string, List<HelpModel>> helpList = new Dictionary<string, List<HelpModel>>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
                action = Request["action"].ToString();   
             
            if (action == "zc")
            {
                helpTitle = "注册事项";
                GetHelpText(new List<HelpModel>() { 
                         new HelpModel(){ Title="如何注册？",Text="用户寻找乐诚网代理进行开户注册，提供给代理账号•密码•昵称等， 乐诚网代理将会给您开设账户。"},
                         new HelpModel(){ Title="注册时用户需要注意什么？",Text="用户名一旦提交，不可更改，请选择容易记忆且安全级别高的用户名，并妥善保管； 初始密码是不安全的，进入平台后，请先修改您的密码。"}
                    });
            }
            else if (action == "dl")
            {
                helpTitle = "登录事项";
                GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="登录时为什么看不到验证码或总是提示验证码不正确？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="看不到验证码或者总是提示验证码输入错误，可能的原因有： "},
                                new HelpModel(){Text="A：网速过慢，请多刷新几次页面； "},
                                new HelpModel(){Text="B：输入错误，数字有全角和半角之分，请换用半角方式输入；"},
                                new HelpModel(){Text="C：安全级别设置过高，请将IE的安全级别设为“默认级别”； "},
                                new HelpModel(){Text="D：网页没有自动更新，请先设置浏览器的“Internet临时文件”为“自动”更新或使用Ctrl+F5刷新网页。 "},
                                new HelpModel(){Text="E：建议您使用IE8及以上的浏览器，最好选择火狐浏览器和谷歌浏览器登录平台 "}
                             }
                         },
                         new HelpModel()
                         { 
                             Title=" 登录时忘记了密码怎么办？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：如果忘记平台登录密码，请点击用户登录区的“忘记密码”，输入资金密码，也可登陆乐诚网平台，为保障账户安全，建议用户登录后立即修改密码； "},
                                new HelpModel(){Text="B：如果忘记平台登陆密码和资金密码，请联系您的上级或者平台客服，提供绑定的银行卡信息进行核实，如果属实，可重新设定密码； "},
                                new HelpModel(){Text="C：如果…如果忘记与平台所相关的所有账号信息，请参考帮助中心的“如何注册”，重新申请账号。 "}
                             }
                         },
                         new HelpModel()
                         { 
                             Title="如何修改登录密码？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：登录后进入“个人中心”，点击“修改密码”。 "},
                                new HelpModel(){Text="B：在“修改密码”条目下选择“修改登录密码”选项。"},
                                new HelpModel(){Text="C：用户输入旧密码，并设置新密码。"},
                                new HelpModel(){Text="D：提交信息，修改密码成功，系统提示成功信息。 "}
                             }
                         },
                         new HelpModel()
                         { 
                             Title="注册登录需要注意什么？",
                             Text="请认准乐诚网平台域名验证地址：http://www.boyuesite.com"
                         },
                         new HelpModel()
                         { 
                             Title="登录过程中为什么会跳转到谷歌界面？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：进行登陆时的用户名输入错误； "},
                                new HelpModel(){Text="B：您的账号不存在； "},
                                new HelpModel(){Text="C：当前域名为非系统指定域名，请联系上级或平台客服获取最新域名地址。 "}
                             }
                         }
                    });
            }
        }

        private void GetHelpText(List<HelpModel> list)
        {
            System.Text.StringBuilder sbStr = new System.Text.StringBuilder();
            sbStr.Append("<ul class=\"help-list-normal clearfix\">");
            for (int i = 0; i < list.Count; i++)
            {
                sbStr.Append("<li>");
                sbStr.Append("<p class=\"help-list-name\"><span class=\"help-list-num\">" + (i + 1).ToString() + "</span><a href=\"#\">" + list[i].Title + "</a></p>");
                if (string.IsNullOrEmpty(list[i].Text) && list[i].CNode != null)
                {
                    sbStr.Append("<div class=\"help-list-text\">");
                    for (int j = 0; j < list[i].CNode.Count; j++)
                    {
                        sbStr.Append("<p>" + list[i].CNode[j].Text + "</p>");
                    }
                    sbStr.Append("</div>");
                }
                else
                {
                    sbStr.Append("<p class=\"help-list-text\">");
                    sbStr.Append(list[i].Text);
                    sbStr.Append("</p>");
                }
                sbStr.Append("</li>");            
            }
            sbStr.Append("</ul>");
            helpText = sbStr.ToString();
        }
    }


  
}