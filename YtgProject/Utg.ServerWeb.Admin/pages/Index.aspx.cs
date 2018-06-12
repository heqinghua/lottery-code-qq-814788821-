using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Comm.Security;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages
{
    public partial class Index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["type"]))
            { 
              //注销
                FormsPrincipal<CookUserInfo>.SignOut();
                Response.Redirect("/Login.aspx");
            }

            if (!IsPostBack)
            {
                if (Request["action"] == null)
                {
                    this.LoadMenu();
                }
                
            }

            //获取提现,充值人数
            if (Request["action"] != null)
            {
                GetBusinessCount();
            }
        }

        /// <summary>
        /// 加载菜单
        /// </summary>
        private void LoadMenu()
        {
            IPermissionService permissionService = IoC.Resolve<IPermissionService>();
            List<Permission> permissionList = permissionService.GetMenuList(this.LoginUser.Id);

            List<MenuModel> menuList = new List<MenuModel>();
            if (null != permissionList && permissionList.Count > 0)
            {
                var pNodes = permissionList.Where(m => m.PId == 0).ToList();
                foreach (var pNode in pNodes)
                {
                    if (pNode.PId == 0)
                    {
                        MenuModel menuModel = new MenuModel();
                        menuModel.Id = pNode.Id;
                        menuModel.Name = pNode.Name;
                        menuModel.ChildNodes = new List<MenuModel>();
                        var xd=permissionList.Where(m => m.PId != 0).OrderBy(x=>x.OccDate);
                        foreach (var cNode in xd)
                        {
                            if (cNode.PId == pNode.Id)
                            {
                                menuModel.ChildNodes.Add(new MenuModel()
                                {
                                    Id = cNode.Id,
                                    Name = cNode.Name,
                                    Url = cNode.Url,
                                });
                            }
                        }
                        menuList.Add(menuModel);
                    }
                }

                rptList.DataSource = menuList;
                rptList.DataBind();
            }
        }

        /// <summary>
        /// 构建url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        public string BuilderUrl(object url, object id)
        {
            if (url == DBNull.Value || url == null)
            {
                return string.Empty;
            }
            string strUrl = url.ToString();
            if (strUrl.IndexOf("?") != -1)
                return strUrl += "&menuId=" + id.ToString();
            return strUrl + "?menuId=" + id.ToString();
        }

        /// <summary>
        /// 获取提现，充值人数
        /// </summary>
        public void GetBusinessCount()
        {
            string result = "";
            int count = 0;
            ISysUserBalanceDetailService sysUserBalanceDetailService = IoC.Resolve<ISysUserBalanceDetailService>();
            WithdrawRechargePersonNumberDTO r = sysUserBalanceDetailService.GetWithdrawRechargePersonNumber();
            if (r != null)
            {
                count = r.WithdrawPeopleNumber + r.RechargePersonNumber+r.notOpen;
                if (count > 0)
                {
                    //result = result + "<a href='/pages/Business/MentionList.aspx' target='main'>提现未处理人数【" + r.WithdrawPeopleNumber + "】个</a>";
                    //result = result + "<br/><br/>";
                    //result = result + "<a href='/pages/Business/RechargeManager.aspx' target='main'>充值未处理人数【" + r.RechargePersonNumber + "】个</a>";
                    result = r.WithdrawPeopleNumber.ToString()+"," + r.RechargePersonNumber+","+r.notOpen;
                }
            }

            Response.Write(result);
            Response.End();
        }

        /// <summary>
        /// 行绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rptList1") as Repeater;//找到里层的repeater对象  
                rep.DataSource = ((MenuModel)e.Item.DataItem).ChildNodes;
                rep.DataBind();
            }  
        }
    }


    public class MenuModel
    {
         public int Id{get;set;}

         public string Name { get; set; }

         public string Url { get; set; }

         public List<MenuModel> ChildNodes { get; set; }
    }
}