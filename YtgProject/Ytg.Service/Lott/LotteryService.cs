using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.Data;

namespace Ytg.Service.Lott
{

    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LotteryService : CrudService<Lottery>, ILotteryService
    {
        
        public LotteryService(IRepo<Lottery> repo)
            : base(repo)
        {
        }

        /// <summary>
        /// 方法说明: 获取开奖数据
        /// 创建时间：2015-05-23
        /// 创建者：GP
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="issueCode"></param>
        /// <param name="date"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<LotteryResultDTO> GetOpenLotteryResult(string lotteryCode, string issueCode, DateTime date, int pageIndex, int pageSize, ref int totalCount)
        {
            //参数设置
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@lotteryCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@issueCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@date",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = lotteryCode;
            parameters[1].Value = issueCode;
            parameters[2].Value = date;
            parameters[3].Value = pageIndex;
            parameters[4].Value = pageSize;

            List<LotteryResultDTO> list = this.ExProc<LotteryResultDTO>("sp_GetOpenLotteryResult", parameters);
            totalCount = Convert.ToInt32(parameters[5].Value);

            return list;
        }

        /// <summary>
        /// 方法说明: 获取未开奖数据
        /// 创建时间：2015-06-07
        /// 创建者：GP
        /// </summary>
        /// <param name="lotteryCode"></param>
        /// <param name="date"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<LotteryResultDTO> GetNoLotteryResult(string lotteryCode,DateTime date, int pageIndex, int pageSize, ref int totalCount)
        {
            //参数设置
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@lotteryCode",System.Data.DbType.String),                
                new DbContextFactory().GetDbParameter("@date",System.Data.DbType.DateTime),
                new DbContextFactory().GetDbParameter("@pageIndex",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@pageSize",System.Data.DbType.Int32),
                new DbContextFactory().GetDbParameter("@totalCount",System.Data.DbType.Int32,System.Data.ParameterDirection.Output)
            };

            parameters[0].Value = lotteryCode;
            parameters[1].Value = date;
            parameters[2].Value = pageIndex;
            parameters[3].Value = pageSize;

            List<LotteryResultDTO> list = this.ExProc<LotteryResultDTO>("sp_GetNoLotteryResult", parameters);
            totalCount = Convert.ToInt32(parameters[4].Value);

            return list;
        }

        /// <summary>
        /// 手动开奖
        /// </summary>
        /// <param name="issueCode"></param>
        /// <param name="openResult"></param>
        /// <returns></returns>
        public void ManualOpen(string lotteryCode,string issueCode, string openResult)
        {
            //参数设置
            DbParameter[] parameters = new DbParameter[]{
                new DbContextFactory().GetDbParameter("@lotteryCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@issueCode",System.Data.DbType.String),
                new DbContextFactory().GetDbParameter("@openResult",System.Data.DbType.String)
            };
            parameters[0].Value = lotteryCode;
            parameters[1].Value = issueCode;
            parameters[2].Value = openResult;
            this.ExProc<Object>("SP_ManualOpen", parameters);
        }
    }
}
