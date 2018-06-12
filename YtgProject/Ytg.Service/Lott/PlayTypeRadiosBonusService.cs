using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Core.Repository;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    /// <summary>
    /// 玩法单选详细奖金
    /// </summary>
     [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PlayTypeRadiosBonusService : CrudService<PlayTypeRadiosBonus>, IPlayTypeRadiosBonusService
    {

        public PlayTypeRadiosBonusService(IRepo<PlayTypeRadiosBonus> repo)
            : base(repo)
        {

        }



        public List<PlayTypeRadiosBonus> GetPlayTypeRadiosBonus(int radioCode)
        {
            return this.Where(c => c.RadioCode == radioCode).ToList();
        }


        public bool UpdateRadioBonus(PlayTypeRadiosBonus item)
        {
            var c= this.Get(item.Id);
            if (c == null)
                return false;
            c.MaxBonus = item.MaxBonus;
            c.MaxBonus17 = item.MaxBonus17;
            c.RadioCode = item.RadioCode;
            c.BonusBasic = item.BonusBasic;
            c.BonusBasic17 = item.BonusBasic17;
            c.BonusCount = item.BonusCount;
            c.BonusTitle = item.BonusTitle;

            return true;
        }
    }
}
