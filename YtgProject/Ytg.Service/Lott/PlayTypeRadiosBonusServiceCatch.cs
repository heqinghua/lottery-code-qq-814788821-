using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    public class PlayTypeRadiosBonusServiceCatch
    {
        private static List<PlayTypeRadiosBonus> mPlayTypeRadio = null;

        public static List<PlayTypeRadiosBonus> GetAll()
        {
            if (mPlayTypeRadio == null)
            {
                IPlayTypeRadiosBonusService radioService = IoC.Resolve<IPlayTypeRadiosBonusService>();
                mPlayTypeRadio = radioService.GetAll().ToList();
            }
            return mPlayTypeRadio;
        }
    }
}
