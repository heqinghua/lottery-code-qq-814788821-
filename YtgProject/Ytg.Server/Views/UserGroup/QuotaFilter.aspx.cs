using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.Comm;

namespace Ytg.ServerWeb.Views.UserGroup
{
    public partial class QuotaFilter : BasePage
    {
        public double UserRemo = 0.0;


        /// <summary>
        /// option html
        /// </summary>
        public string OptionHtm = string.Empty;

        /// <summary>
        /// table head html
        /// </summary>
        public string ThHtm = string.Empty;

        /// <summary>
        /// rows array
        /// </summary>
        public string rowsStr = string.Empty;

        public int UserPlayType = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserPlayType = (int)CookUserInfo.PlayType;
                this.UserRemo = Utils.MaxRemo - CookUserInfo.Rebate;

                //获取配合数据
                var qus = YtgConfig.GetQus();

                foreach (var item in qus.Keys)
                {
                    double configRemo = Convert.ToDouble(item);
                    if (this.UserRemo < configRemo)
                        continue;
                    string showTitle = CookUserInfo.PlayType == BasicModel.UserPlayType.P1800 ? item : Utils.Get1700(item);
                    OptionHtm += string.Format("<option value='{0}'>{1}</option>", item,showTitle);
                    ThHtm += string.Format("<th style='windth:10%'>{0}</th>", showTitle);
                    rowsStr += item + ",";
                }
                if (!string.IsNullOrEmpty(rowsStr))
                    rowsStr = rowsStr.Substring(0, rowsStr.Length - 1);
            }
        }


        

        
    }
}