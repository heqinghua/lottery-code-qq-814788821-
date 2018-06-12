using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ytg.ServerWeb.Lottery
{
    public partial class GameK3 : GameCenter
    {

        public override void BuilderDataJSon()
        {
            string key = LotteryId.ToString();
            JsonData = Ytg.ServerWeb.BootStrapper.RadioJsonCatch.CreateIntance().Get(key);
            if (string.IsNullOrEmpty(JsonData))
                JsonData = Ytg.ServerWeb.BootStrapper.LotterySturctHelper.Builder(this.LotteryId, this.LotteryCode, CookUserInfo.Rebate, (int)CookUserInfo.PlayType, null, ref lottery_methods);
        }

     
    }
}