using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic;
using Ytg.Core.Service.Lott;
using Ytg.Data;

namespace Ytg.Service.Lott
{
    public class BaseDataCatch
    {
        static List<PlayTypeRadio> mPlayTypeRadios = null;

        static List<PlayTypeRadiosBonus> mPlayTypeRadiosBonus = null;

        public static List<PlayTypeRadio> GetPalyTypeRadio(IPlayTypeRadioService playTypeRadioService)
        {
            if(mPlayTypeRadios==null)
                mPlayTypeRadios = playTypeRadioService.GetAll().ToList();
            return mPlayTypeRadios;

            
        }

        public static List<PlayTypeRadio> GetPalyTypeRadio()
        {
            if (mPlayTypeRadios == null)
            {
                IDbContextFactory factory = new DbContextFactory();
                PlayTypeRadioService playTypeRadioService = new PlayTypeRadioService(new Repo<PlayTypeRadio>(factory));
                mPlayTypeRadios = playTypeRadioService.GetAll().ToList();
            }
            return mPlayTypeRadios;
        }

        /// <summary>
        /// 获取玩法单选奖金
        /// </summary>
        /// <returns></returns>
        public static List<PlayTypeRadiosBonus> GetPlayTypeRadiosBonus()
        {
            if (mPlayTypeRadiosBonus == null)
            {
                IDbContextFactory factory = new DbContextFactory();
                PlayTypeRadiosBonusService playTypeRadioService = new PlayTypeRadiosBonusService(new Repo<PlayTypeRadiosBonus>(factory));
                mPlayTypeRadiosBonus = playTypeRadioService.GetAll().ToList();
            }
            return mPlayTypeRadiosBonus;
        }
    }
}
