using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Windows;
using Ytg.BasicModel.DTO;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 绑定用户配额列表
    /// </summary>
    public class BindSysQuotaDTO : SysQuotaDTO
    {
        /// <summary>
        /// 父用户id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// old神域开户额
        /// </summary>
        public int OldSurplusNum { get; set; }

        private int mSurplusNum;

        /// <summary>
        /// 剩余开户额
        /// </summary>
        public int SurplusNum
        {
            get
            {
                return mSurplusNum;
            }
            set
            {
                mSurplusNum = value;
            }

        }


        private int mParentSurplusNum;
        /// <summary>
        /// 父级别对应剩余开户额
        /// </summary>
        public int ParentSurplusNum
        {
            get
            {

                return this.mParentSurplusNum;
            }
            set
            {

                this.mParentSurplusNum = value;
            }
        }
        /// <summary>
        /// 增加的配额数
        /// </summary>
        public int AppendNum { get; set; }

    }
}
