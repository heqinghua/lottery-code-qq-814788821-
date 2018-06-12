using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ytg.ServerWeb
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string actionName = context.Request["actionName"].ToString();
            switch (actionName)
            {
                case "UploadBannerImage":
                    UploadBannerImage(context); //首页Banner上传
                    break;
                case "UpdateBannerImage":
                    UpdateBannerImage(context); //修改首页Banner图片
                    break;
                case "DelBannerImage":
                    DelBannerImage(context); //删除首页Banner图片
                    break;
            }
        }

        #region 首页Banner上传

        /// <summary>
        /// 首页Banner图片上传
        /// </summary>
        private void UploadBannerImage(HttpContext context)
        {
            SaveImage(context, "~/ClientBin/Resources/Banner/");
        }

        #endregion

        #region 修改首页Banner图片

        /// <summary>
        /// 修改首页Banner图片
        /// </summary>
        /// <param name="context">上下午</param>
        /// <param name="oldFileName">原始图片文件名</param>
        private void UpdateBannerImage(HttpContext context)
        {
            string savePath = "~/ClientBin/Resources/Banner/";

            //删除图片
            DelBannerImage(context);

            //保存图片
            SaveImage(context, savePath);
        }

        #endregion

        #region 删除首页Banner图片

        /// <summary>
        /// 删除首页Banner图片
        /// </summary>
        /// <param name="fileName"></param>
        public void DelBannerImage(HttpContext context)
        {
            string savePath = "~/ClientBin/Resources/Banner/";
            string fileName = context.Request.QueryString["filename"].ToString();
            DelImage(savePath + fileName);
        }

        #endregion

        #region 保存图片

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="savePath">文件保存路径</param>
        /// <returns></returns>
        private void SaveImage(HttpContext context, string savePath)
        {
            try
            {
                string fileName = context.Request.QueryString["filename"].ToString();
                savePath = context.Server.MapPath(savePath + fileName);

                byte[] buffer = new byte[1024];
                int bytesRead;

                if (File.Exists(savePath))
                    File.Delete(savePath);

                using (FileStream fs = File.Create(savePath))
                {
                    while ((bytesRead = context.Request.InputStream.Read(buffer, 0,buffer.Length)) != 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                    }
                }
            }
            catch
            {
               
            }
        }

        #endregion

        #region 删除图片

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        private void DelImage(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        #endregion
    }
}