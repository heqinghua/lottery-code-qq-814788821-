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
    public interface IVipMentionApplyService : ICrudService<VipMentionApply>
    {
        bool CreateApply(bool isOpenVip, int userId);

        bool Exist(int userId);

        [OperationContract]
        List<VipMentionApplyVM> SelectBy(int isOpenVip, string sDate, string eDate, int pageIndex, ref int totalCount);

        [OperationContract]
        bool Audit(int applyId, bool pass);
    }
}
