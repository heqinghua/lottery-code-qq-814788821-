using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;

namespace Ytg.Core.Service
{
    [ServiceContract]
    public interface IBannerService : ICrudService<Banner>
    {
        /// <summary>
        /// 获取Banner列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<Banner> GetBanners();

        [OperationContract]
        /// <summary>
        /// 添加Banner
        /// </summary>
        /// <param name="banner"></param>
        /// <returns></returns>
        int AddBanner(Banner banner);

        [OperationContract]
        /// <summary>
        /// 修改Banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool UpdataBanner(int id, string title, string fileName);

        [OperationContract]
        /// <summary>
        /// 删除Banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DelBanner(int id);
    }
}
