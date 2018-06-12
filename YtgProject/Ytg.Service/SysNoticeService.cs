using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SysNoticeService : CrudService<SysNotice>, ISysNoticeService
    {
        protected readonly IHasher mHasher;
        public SysNoticeService(IRepo<SysNotice> repo, IHasher hasher)
            : base(repo)
        {
            this.mHasher = hasher;
            this.mHasher.SaltSize = 8;
        }
        public bool CreateNotice(SysNotice item)
        {
            if (null == item)
                return false;
            this.Create(item);
            this.Save();
            return true;
        }

        public IEnumerable<SysNotice> GetNotice(string sTitle, int? isHot, bool? isDel, int pageIndex, ref int totalCount)
        {
            var source = this.GetAll();
            if (!string.IsNullOrEmpty(sTitle))
                source = source.Where(item => item.Title.IndexOf(sTitle) != -1);
            if (isHot.HasValue)
                source = source.Where(item => item.IsHot == isHot.Value);
            if (isDel.HasValue)
                source = source.Where(item => item.IsDelete == isDel.Value);

            totalCount = source.Count();
            return source.Page(pageIndex, AppGlobal.ManagerDataPageSize);
        }

        public SysNotice GetForId(int id)
        {
            return this.Get(id);
        }

        public bool UpdateItem(int id, SysNotice item)
        {
            if (null == item)
                return false;
            this.Save();
            return true;
        }
    }
}
