using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Utg.ServerWeb.Admin.pages.Setting
{
    public partial class DataBaseBak : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.Bind();
        }

        private void Bind()
        {
            IDataBaseBakService dataBaseBakService = IoC.Resolve<IDataBaseBakService>();
             var result= dataBaseBakService.GetAll().OrderByDescending(x=>x.OccDate);
             this.repList.DataSource = result;
             this.repList.DataBind();
        }

        protected void lkDel_Click(object sender, EventArgs e)
        {
            var lkButton = sender as LinkButton;
            if (lkButton == null)
                return;
            int id = Convert.ToInt32(lkButton.CommandArgument);

            IDataBaseBakService dataBaseBakService = IoC.Resolve<IDataBaseBakService>();
            dataBaseBakService.Delete(id);
            dataBaseBakService.Save();
            this.Bind();
        }

        public string BuilderDownloadUrl(object fileName)
        {
            string path = Server.MapPath("~/bak/") + fileName;
            return path + fileName;
        }

        private void Backups()
        {
            string fileName=DateTime.Now.ToString("yyyyMMddHHmmss");
            string path = Server.MapPath("~/bak/") + fileName;
            //数据库备份
            string SqlStr2 = "backup database YtgProject to disk='" + path + ".rar'";
            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["YtgConnection"].ConnectionString);
            con.Open();
            try
            {
                if (File.Exists(path))
                {
                    Response.Write("<script language=javascript>alert('数据库备份文件已存在！');</script>");
                    return;
                }
                SqlCommand com = new SqlCommand(SqlStr2, con);
                com.ExecuteNonQuery();
                //保存记录
                IDataBaseBakService dataBaseBakService = IoC.Resolve<IDataBaseBakService>();
                dataBaseBakService.Create(new Ytg.BasicModel.DataBaseBak()
                {
                    FileName = fileName,
                    OccDate = DateTime.Now,
                    OpenUser = LoginUser.Code
                });
                dataBaseBakService.Save();
                Response.Write("<script language=javascript>alert('备份数据成功！');</script>");
                this.Bind();
            }
            catch (Exception error)
            {
                Response.Write(error.Message);
                Response.Write("<script language=javascript>alert('备份数据失败！')</script>");
            }
            finally
            {
                con.Close();
            }
        }

        protected void lkBack_Click(object sender, EventArgs e)
        {
            this.Backups();
        }
    }
}