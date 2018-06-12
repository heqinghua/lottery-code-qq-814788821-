using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ytg.ServerWeb.Views.pay
{
    public partial class Payment : System.Web.UI.Page
    {
        public decimal Min = 50;//充值最小值
        public decimal Max = 2000;//充值最大值
        protected void Page_Load(object sender, EventArgs e)
        {
            Min = Comm.Utils.ZfbMin;
            Max = Comm.Utils.ZfbMax;
            var sp = !IsShoping();
           
            if (sp)
            {
                this.noShowTitle.Visible = true;
                tabs.Visible = false;
                
            }
            Ytg.Scheduler.Comm.LogManager.Info(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")+" -- "+ sp);
        }

        /// <summary>
        /// 当前时间是否销售
        /// </summary>
        /// <returns></returns>
        public static bool IsShoping()
        {
            var nowDates = DateTime.Now;
            var hour = nowDates.Hour;//时
            var mis = nowDates.Minute;//分
            //
            int[] hours = new int[] {9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 0, 1 };
            
            return hours.Contains(hour);
        }
    }
}