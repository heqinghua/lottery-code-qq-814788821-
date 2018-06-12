using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

public partial class help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpPostedFile postFile = Request.Files[0];
            //开始上传
            string _savedFileResult = UpLoadFile(postFile);
            Response.Write(_savedFileResult);
            

        }
        catch(Exception ex)
        {
            Response.Write("0|error|上传提交出错" + ex.Message);
        }
        Response.End();

    }
    public string UpLoadFile(HttpPostedFile str)
    {
        if (str.ContentLength > 524288)
        {
            return "0|errorfile|" + "文件不合法";
        }
        return UpLoadFile(str, "/UpLoadFile/");
    }
    public string UpLoadFile(HttpPostedFile httpFile, string toFilePath)
    {
        try
        {
            //获取要保存的文件信息
            string filerealname = httpFile.FileName;
            //获得文件扩展名
            string fileNameExt = System.IO.Path.GetExtension(filerealname);
            if (CheckFileExt(fileNameExt))
            {
                //检查保存的路径 是否有/结尾
                if (toFilePath.EndsWith("/") == false) toFilePath = toFilePath + "/";

                //按日期归类保存
                string datePath = DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd") + "/";
                if (true)
                {
                    toFilePath += datePath;
                }

                //物理完整路径                    
                string toFileFullPath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + toFilePath;

                //检查是否有该路径  没有就创建
                if (!System.IO.Directory.Exists(toFileFullPath))
                {
                    Directory.CreateDirectory(toFileFullPath);
                }

                //得到服务器文件保存路径
                string toFile = Server.MapPath("~" + toFilePath);
                string f_file = getName(filerealname);
                //将文件保存至服务器
                httpFile.SaveAs(toFile + f_file);
                return "1|" + toFilePath + f_file + "|" + "文件上传成功";
            }
            else
            {
                return "0|errorfile|" + "文件不合法";
            }
        }
        catch (Exception e)
        {
            return "0|errorfile|" + "文件上传失败,错误原因：" + e.Message;
        }
    }

    /// <summary>
    /// 获取文件名
    /// </summary>
    /// <param name="fileNamePath"></param>
    /// <returns></returns>
    private string getName(string fileNamePath)
    {
        return Ytg.Comm.Utils.BuilderNum() + System.IO.Path.GetExtension(fileNamePath);
        //string[] name = fileNamePath.Split('\\');
        //return name[name.Length - 1];
    }
    /// <summary>
    /// 检查是否为合法的上传文件
    /// </summary>
    /// <param name="_fileExt"></param>
    /// <returns></returns>
    private bool CheckFileExt(string _fileExt)
    {
        string[] allowExt = new string[] { ".gif", ".jpg", ".jpeg", ".rar",".png" };
        for (int i = 0; i < allowExt.Length; i++)
        {
            if (allowExt[i] == _fileExt) { return true; }
        }
        return false;

    }

    public static string GetFileName()
    {
        Random rd = new Random();
        StringBuilder serial = new StringBuilder();
        serial.Append(DateTime.Now.ToString("HHmmss"));
        serial.Append(rd.Next(100, 999).ToString());
        return serial.ToString();

    }
}
