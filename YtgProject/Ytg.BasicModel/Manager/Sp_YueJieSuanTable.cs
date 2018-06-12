using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.BasicModel.Manager
{
    /// <summary>
    /// 月结算报表
    /// </summary>
    public class Sp_YueJieSuanTable : Sp_JieSuanTable
    {
        /// <summary>
        /// 月结算
        /// </summary>

        private string mOccMonth;
        public string OccMonth
        {
            get
            {
                return this.mOccMonth;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.mOccMonth = value.Insert(3, "-");
                }
                this.mOccMonth = value;

            }
        }

    }
}
