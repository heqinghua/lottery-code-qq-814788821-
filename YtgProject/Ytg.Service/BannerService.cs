using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    /// <summary>
    /// 首页Banner图片服务
    /// </summary>
    public class BannerService : CrudService<Banner>, IBannerService
    {
        log4net.ILog log = null;
        public BannerService(IRepo<Banner> repo)
            : base(repo)
        {
            log = log4net.LogManager.GetLogger("errorLog");
        }

        /// <summary>
        /// 获取Banner列表
        /// </summary>
        /// <returns></returns>
        public IList<Banner> GetBanners()
        {
            return this.GetAll().ToList();
        }

        /// <summary>
        /// 添加Banner
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        public int AddBanner(Banner banner)
        {
            this.Create(banner);
            this.Save();
            return banner.Id;
        }

        /// <summary>
        /// 修改Banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool UpdataBanner(int id,string title,string fileName)
        {
            try
            {
                Banner banner = this.Get(id);
                if (banner != null)
                {
                    banner.Title = title;
                    banner.FileName = fileName;
                    this.Save();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                log.Error("UpdataBanner",ex);
                return false;
            
            }
        }

        /// <summary>
        /// 删除Banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelBanner(int id)
        {
            try
            {
                this.Delete(this.Get(id));
                this.Save();
                return true;
            }
            catch(Exception ex)
            {
                log.Error("DelBanner", ex);
                return false;

            }
        }
    }
}
