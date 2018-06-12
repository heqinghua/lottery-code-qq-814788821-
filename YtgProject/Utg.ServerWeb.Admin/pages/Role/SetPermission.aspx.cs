using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Role
{
    public partial class SetPermission : BasePage
    {
        private int roleId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                treeT.Attributes.Add("onclick", "CheckEvent()");
                if (!int.TryParse(Request.Params["roleId"], out roleId))
                    roleId = -1;

                this.txtRoleId.Value = roleId.ToString();

                InitTree();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitTree()
        {
            //获取当前角色的权限
            IRolePermissionService rolePermissionService = IoC.Resolve<IRolePermissionService>();
            List<RolePermission> rolePermissonList = rolePermissionService.GetRolePermissionList(roleId);
            
            //获取权限列表
            IPermissionService permissionService = IoC.Resolve<IPermissionService>();
            List<Ytg.BasicModel.Permission> list = permissionService.GetAll().ToList();

            if (list.Count > 0)
            {
                List<Ytg.BasicModel.Permission> pList = list.Where(m => m.PId == 0).ToList();//获取根节点数据
                foreach (var item in pList)
                {
                    TreeNode node = new TreeNode(); //声明节点
                    node.ExpandAll();
                    node.Value = item.Id.ToString();
                    node.Text = item.Name;
                    if (rolePermissonList.Where(m => m.PermissionId == item.Id).FirstOrDefault() != null)
                        node.Checked = true;

                    AddTree(Convert.ToInt32(item.Id), node, list, rolePermissonList);
                    this.treeT.Nodes.Add(node);
                }
            }
        }

        //递归方法
        public void AddTree(int id, TreeNode pNode, List<Ytg.BasicModel.Permission> list, List<RolePermission> rolePermissonList)
        {
            List<Ytg.BasicModel.Permission> pList = list.Where(m => m.PId == id).ToList();
            if (list.Count > 0)
            {
                foreach (var item in pList)
                {
                    TreeNode node = new TreeNode(); //声明节点
                    node.ExpandAll();
                    node.Value = item.Id.ToString();
                    node.Text = item.Name;
                    if (rolePermissonList.Where(m => m.PermissionId == item.Id).FirstOrDefault() != null)
                        node.Checked = true;
                    AddTree(Convert.ToInt32(item.Id), node, list, rolePermissonList);
                    pNode.ChildNodes.Add(node);
                }
            }
        }

        //设置角色权限
        protected void btnSave_Click(object sender, EventArgs e)
        {
            IRolePermissionService rolePermissionService = IoC.Resolve<IRolePermissionService>();

            List<int> permissionIds = new List<int>();
            foreach (TreeNode t in treeT.CheckedNodes)
            {
                permissionIds.Add(Convert.ToInt32(t.Value));
            }

            this.roleId = Convert.ToInt32(this.txtRoleId.Value);
            if (rolePermissionService.SetRolePermission(roleId, permissionIds))
            {
                //JsAlert("设置成功！", true);
                ClientScript.RegisterStartupScript(this.GetType(), "warning_alert_ks", "<script>alert('设置成功');window.history.go(-1);</script>", false);
            }
            else
            {
                JsAlert("设置失败，请稍后再试！");
            }
        }
    }
}