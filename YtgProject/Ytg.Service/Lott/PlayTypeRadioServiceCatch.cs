using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    public class PlayTypeRadioServiceCatch
    {
        private static List<PlayTypeRadio> mPlayTypeRadio = null;

        public static List<PlayTypeRadio> GetAll()
        {
            if (mPlayTypeRadio == null)
            {
                IPlayTypeRadioService radioService = IoC.Resolve<IPlayTypeRadioService>();
                mPlayTypeRadio = radioService.GetAll().ToList();
            }
            return mPlayTypeRadio;
        }
    }
}
