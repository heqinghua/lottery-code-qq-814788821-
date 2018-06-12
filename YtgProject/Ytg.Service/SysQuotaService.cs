using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Comm;
using Ytg.Core.Repository;
using Ytg.Core.Service;
using Ytg.Data;
using Ytg.Service.YtgCache;

namespace Ytg.Service
{
    /// <summary>
    /// pei er
    /// </summary>
    public class SysQuotaService : CrudService<SysQuota>, ISysQuotaService
    {
        readonly ISysQuotaDetailService mSysQuotaDetailService;//账变详细信息


        public SysQuotaService(IRepo<SysQuota> repo,
            ISysQuotaDetailService sysQuotaDetailService)
            : base(repo)
        {
            this.mSysQuotaDetailService = sysQuotaDetailService;
        }

        public List<SysQuota> GetUserQuota(int uid)
        {
            return this.Where(item => item.SysUserId==uid && item.IsDelete==false).ToList();
        }

        public SysQuota GetUserQuota(int uid, double remb)
        {
            var result = this.GetUserQuota(uid);
            if (null == result)
                return null;
            var cdr = Math.Round(remb, 1).ToString();
            var rd = cdr == "0"?"0": cdr;
            return result.Where(x => x.QuotaType == (rd.Length==1?rd+".0":rd).ToString()).FirstOrDefault() ;
            //foreach (var item in result)
            //{
            //    double sD = 0;
            //    double eD = 0;
            //    if (item.QuotaType.IndexOf("-") < 0)
            //    {
            //        sD = 7.5;
            //        eD = 7.5;
            //    }
            //    else
            //    {
            //        var dicArray = item.QuotaType.Split('-');
            //        sD = Convert.ToDouble(dicArray[0]);
            //        eD = Convert.ToDouble(dicArray[1]);
            //    }
            //    if (remb >= sD)
            //    {
            //        return item;
            //    }
            //}

            //return null;
        }

        public List<SysQuota> GetUserQuotaMax(int uid)
        {
            return this.Where(item => item.SysUserId == uid && item.IsDelete == false && item.MaxNum > 0).ToList();
        }

        /// <summary>
        /// 修改用户配额
        /// </summary>
        /// <param name="parentUid"></param>
        /// <param name="quotaId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdateQuota(int parentUid, int quotaId, int value)
        {
            var quota = this.Get(quotaId);
            if (null == quota)
                return 0;

            //判断该用户当天是否修改过配额，若修改过，则不允许修改
            //this.mSysQuotaDetailService.Where(x=>x.)

            var pitem = this.Where(item => item.SysUserId == parentUid && item.QuotaType == quota.QuotaType).FirstOrDefault();
            if (null == pitem)
                return 0;

            //父配额剩余
            var pCurNum = pitem.MaxNum;
            if (value > pCurNum)
                return -1;//余额不够

            pitem.MaxNum -= value;
            quota.MaxNum +=value;
            this.Save();

            //存储配额变更
            this.mSysQuotaDetailService.AddDetail(quota.Id, ActionType.发放配额, (quota.MaxNum - value), quota.MaxNum, OpenUser.System);//父级配额变化记录
            this.mSysQuotaDetailService.AddDetail(pitem.Id, ActionType.编辑扣减, (pitem.MaxNum + value), pitem.MaxNum, OpenUser.System);//父级配额变化记录
            return 1;
        }

       
        #region 私有方法

        /// <summary>
        /// 根据用户id,配额类型获取配额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="quotatype"></param>
        /// <param name="value"></param>
        private SysQuota GetQuota(int uid, string quotatype)
        {
            return this.GetAll().Where(item=>item.SysUserId ==uid && item.QuotaType==quotatype).FirstOrDefault();
        }

        
        /// <summary>
        /// 当前剩余额度减去一个 -1 获取id,类型不存在 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="type">0:父代理用户编辑</param>
        /// <returns></returns>
        private int SubtractQuota(SysQuota item,int type)
        {
            int oldValue = item.MaxNum;
            ActionType actionType = ActionType.编辑返还;
            switch (type)
            { 
                case 0://回收
                    item.MaxNum = item.MaxNum + 1;
                    break;
                case 1://升点
                    item.MaxNum = item.MaxNum - 1;
                    break;
            }

            
          
            this.Save();
            this.mSysQuotaDetailService.AddDetail(item.Id, actionType, oldValue, item.MaxNum, OpenUser.System);//父级配额变化记录
            return 0;
        }

        #endregion

        /// <summary>
        /// 当前剩余额度减去一个 -1 获取id,类型不存在 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int SubtractQuota(int uid, double quoValue, ActionType actionType)
        {
            var xstrr=Math.Round(quoValue,1).ToString();
            var fs = this.Where(x => x.QuotaType == xstrr && x.SysUserId == uid).FirstOrDefault();
            if (fs == null)
                return -1;
            if (fs.MaxNum < 1)
                return -1;
            fs.MaxNum=fs.MaxNum-1;
            this.Save();
            this.mSysQuotaDetailService.AddDetail(fs.Id, ActionType.升点增加, (fs.MaxNum+1), fs.MaxNum, OpenUser.System);//父级配额变化记录
            return 0;
        }

        
        /// <summary>
        /// 初始化代理用户配额数据
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="parentid"></param>
        /// <param name="remb"></param>
        /// <returns>0 成功 1 非法请求（父对象没有配额信息） </returns>
        public int InintUserQuota(int uid, int? parentid, double remb)
        {
            if (remb < Convert.ToDouble(Ytg.Comm.YtgConfig.GetItem("NotQuotaNum") ?? "0")) // 6.1不限制
                return 0;
            //获取父所有配额信息
            var result = this.GetUserQuota(parentid.Value);
            if (null == result || result.Count < 1)
                return 1;
            int outState = 0;
            foreach (var item in result)
            {
                if (remb >= Convert.ToDouble(item.QuotaType))
                    outState = AddSysQuotaItem(remb, uid, parentid.Value, item, -1,(remb==Convert.ToDouble( item.QuotaType)));
            }

            return outState;
        }

        /// <summary>
        /// 新增配额数据
        /// </summary>
        /// <param name="remb"></param>
        /// <param name="uid"></param>
        /// <param name="parentid"></param>
        /// <param name="item"></param>
        /// <param name="type">0:编辑返点</param>
        /// <returns>0 成功，1 表示父没有足够的配额</returns>
        private int AddSysQuotaItem(double remb, int uid, int parentid, SysQuota item,int type,bool isSubParent)
        {
           
            SysQuota quota = new SysQuota();
            quota.OccDate = DateTime.Now;
            quota.QuotaType = item.QuotaType;
            quota.SysUserId = uid;
            quota.MaxNum = 0;
            this.Create(quota);
            this.Save();


            if (isSubParent)
            {
                //验证父是否有足够的配额
                if (item.MaxNum < 1)
                    return 1;
                int oldMaxNum = item.MaxNum;

                item.MaxNum = item.MaxNum - 1;//父配额减1

                ActionType acType = ActionType.开户扣减;
                if (type == 0)
                {
                    acType = ActionType.编辑扣减;
                }
                else if (type == 1)
                {
                    acType = ActionType.返还配额;
                }
                else if (type == 2)
                {
                    acType = ActionType.升点扣减;
                }

                this.mSysQuotaDetailService.AddDetail(item.Id, acType, oldMaxNum, item.MaxNum, OpenUser.System);
            }
            return 0;
        }

        /// <summary>
        /// 修改用户配额
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="parentid"></param>
        /// <param name="remb"></param>
        /// <param name="type">0：编辑 1：回收 2:升点</param>
        /// <returns>0 成功 1 父没有足够的配额</returns>
        public int UpdateQuota(int uid, int parentid, double remb,int type)
        {
            var subRem=Math.Round(Utils.MaxRemo-remb,1) ;
            
            if (subRem<=Convert.ToDouble(Ytg.Comm.YtgConfig.GetItem("NotQuotaNum") ?? "0")) // 6.1不限制
            {
                Restore(uid,parentid,type);

                //标记该用户的所有配额为删除
                var items= this.GetAll().Where(x => x.SysUserId == uid);
                foreach (var item in items)
                {
                    item.IsDelete = true;
                }
                this.Save();
                return 0;
            }
            var result = this.GetUserQuota(parentid);
            if (result == null || result.Count < 1)
                return 1;
            int outResult = 0;

            foreach (var item in result)
            {

                double doubleItem = Convert.ToDouble(item.QuotaType);//7.5
                //验证该用户是否存在该级别限额
                var fs = this.Where(q => q.QuotaType == item.QuotaType && q.SysUserId == uid, true).FirstOrDefault();
                
                if (fs == null && subRem>=doubleItem)//插入
                {
                    if (item.MaxNum < 1)
                        return 1;
                    outResult = AddSysQuotaItem(remb, uid, parentid, item, type,(subRem==doubleItem));
                }
                else if (null!=fs && fs.IsDelete && subRem >= doubleItem)
                {
                    //标记为未删除，
                    fs.IsDelete = false;
                    if (subRem == doubleItem)
                        SubtractQuota(item, type);
                }
                else if (null != fs && ((type == 0 && subRem < doubleItem) || (type == 1 && subRem <= doubleItem)) && !fs.IsDelete)
                {
                    //删除
                    int fsMaxNum=fs.MaxNum ;
                    fs.MaxNum = 0;
                    fs.IsDelete = true;
                    this.Save();
                    if (subRem == doubleItem)
                    {
                        //还原对应父配额值
                        item.MaxNum += fsMaxNum;
                        SubtractQuota(item, type);
                    }
                    else {
                        item.MaxNum += fsMaxNum;
                        this.Save();
                    }
                }
            }

            return outResult;
            
        }

        /// <summary>
        /// 还原父级返点
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="parentId"></param>
        public void Restore(int parentId, int type, double rebate)
        {
            //删除已经存在的配额数据
            var s = this.GetAll().Where(item => item.SysUserId == parentId && item.QuotaType == rebate.ToString()).FirstOrDefault();
            ActionType actionType = ActionType.编辑返还;
           switch(type){
               case 1:
                   actionType = ActionType.返还配额;
                   break;
           }
           if (s != null)
           {
               s.MaxNum += 1;
               this.Save();

               this.mSysQuotaDetailService.AddDetail(s.Id, actionType, s.MaxNum - 1, s.MaxNum, OpenUser.System);
           }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IEnumerable<SysQuota> GetUserQuota(int uid, int pid)
        {
            int[] array = new int[] { uid, pid };
            return this.Where(item => array.Contains(item.SysUserId));
        }


        /// <summary>
        /// 修改配额额度
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int UpdateQuota(BindSysQuotaDTO dto)
        {
            //验证数据是否通过
            //获取父对象剩余配额
            var pitem= this.Get(dto.ParentId);
            if (null == pitem)
                return -1;
            //计算剩余项
            int cur = pitem.MaxNum;
            if (dto.SurplusNum+dto.AppendNum > cur)
                return -1;//验证失败
            var item= this.Get(dto.Id);
            if (null == item)
                return -1;
            int oldMax=item.MaxNum;
            item.MaxNum = dto.SurplusNum+dto.AppendNum;
            //保存父记录
            pitem.MaxNum = pitem.MaxNum - dto.AppendNum;
            this.Save();
            //保存父记录
            this.mSysQuotaDetailService.AddDetail(pitem.Id, ActionType.升点扣减, cur, pitem.MaxNum, OpenUser.System);
            //保存记录子
            this.mSysQuotaDetailService.AddDetail(item.Id, ActionType.升点增加, oldMax, item.MaxNum, OpenUser.ParentUser);

            return 1;
        }

        /// <summary>
        /// 回收配额 
        /// </summary>
        /// <param name="uid"></param>
        public void ClearQuota(int uid,int pid)
        {
            var source=this.Where(item=>item.SysUserId==uid);
            foreach (var item in source)
            {
                int oldMaxNum = item.MaxNum;
                item.MaxNum = 0;

                this.mSysQuotaDetailService.AddDetail(item.Id, ActionType.回收配额, oldMaxNum, item.MaxNum, OpenUser.ParentUser);

                var pitem = this.Where(q => q.QuotaType == item.QuotaType && q.SysUserId == pid).FirstOrDefault();
                if (null != pitem)
                {
                    var pOldMaxNum = pitem.MaxNum;
                    pitem.MaxNum = pitem.MaxNum + (oldMaxNum == 0 ? 1 : oldMaxNum);
                    this.Save();
                    this.mSysQuotaDetailService.AddDetail(pitem.Id, ActionType.返还配额, pOldMaxNum, pitem.MaxNum, OpenUser.System);
                }
            }

            this.Save();
        }


        /// <summary>
        /// 配额查询
        /// </summary>
        /// <param name="code"></param>
        /// <param name="quotaType"></param>
        /// <param name="quotaWhere"></param>
        /// <param name="quotaValue"></param>
        /// <returns></returns>
        public List<FilterSysQuotaDTO> FilterQuota(int uid,string code, string quotaType, int quotaWhere, int quotaValue, int pageIndex, int pageSize, ref int totalCount, ref int pageCount)
        {
            string procName="[sp_FilterQuota]";
            string sqlWhere = "";
            if (!string.IsNullOrEmpty(code))
                sqlWhere += string.Format(" and su.Code like '%{0}%'", code);
            if (!string.IsNullOrEmpty(quotaType))
                sqlWhere += string.Format(" and QuotaType='{0}'", quotaType);

            string whereStr = "";
            switch (quotaWhere)
            {
                case 0:
                    whereStr = " = ";
                    break;
                case 1:
                    whereStr = " > ";
                    break;
                case 2:
                    whereStr = " >= ";
                    break;
                case 3:
                    whereStr = " < ";
                    break;
                case 4:
                    whereStr = " <= ";
                    break;
            }
            if (quotaWhere >= 0)
                sqlWhere += string.Format(" and MaxNum{0}{1}", whereStr, quotaValue);
            var fc = IoC.Resolve<IDbContextFactory>();
            System.Data.Common.DbParameter[] dbParameters=new System.Data.Common.DbParameter[]{
                fc.GetDbParameter("@uid", System.Data.DbType.Int32),
                fc.GetDbParameter("@pageSize", System.Data.DbType.Int32),
                fc.GetDbParameter("@pageIndex", System.Data.DbType.Int32),
                fc.GetDbParameter("@sqlWhere", System.Data.DbType.String),
                fc.GetDbParameter("@totalCount", System.Data.DbType.Int32),
            };
            dbParameters[0].Value = uid;
            dbParameters[1].Value = pageSize;
            dbParameters[2].Value = pageIndex;
            dbParameters[3].Value = sqlWhere;
            dbParameters[4].Value = 0;
            dbParameters[4].Direction = System.Data.ParameterDirection.Output;


            var source= this.ExProc<FilterSysQuotaDTO>(procName, dbParameters);
            totalCount =Convert.ToInt32(dbParameters[4].Value);
            pageCount = totalCount % pageSize == 0 ? totalCount / pageSize : (totalCount / pageSize) + 1;

            //组织数据

            return source;
        }


        public void InintPrxyQuota(SysUser user, int? value)
        {
            var qus = YtgConfig.GetQus();
            foreach (var q in qus)
            {
                SysQuota sysQuota = new SysQuota();
                if (value == null)
                    sysQuota.MaxNum = Convert.ToInt32(q.Value);
                else
                    sysQuota.MaxNum = 0;

                sysQuota.OccDate = DateTime.Now;
                sysQuota.QuotaType = q.Key;
                sysQuota.SysUserId = user.Id;
                this.Create(sysQuota);
            }
            this.Save();
        }

        /// <summary>
        /// 初始化总代配额
        /// </summary>
        /// <param name="uid"></param>
        public void InintPrxyQuota(SysUser user)
        {
            InintPrxyQuota(user, null);
        }

        /// <summary>
        /// 行转列，配额查询
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userType"></param>
        /// <param name="playType"></param>
        /// <param name="otherWhere"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<SysQuotas_View_Ref> FilterQuotas(string userCode, int userType, int playType, string otherWhere, int pageIndex, int pageSize, ref int totalCount)
        {

            string sql = "select w.SysUserid,w.[9.0] as _90,w.[8.9] as _89,w.[8.8] as _88,w.[8.7] as _87,w.[8.6] as _86,w.[8.5] as _85,w.[8.4] as _84,w.[8.3] as _83,w.[8.2] as _82,w.[8.1] as _81,w.[8.0] as _80,w.[7.9] as _79,w.[7.8] as _78,w.[7.7] as _77,w.[7.6] as _76,w.[7.5] as _75,u.Code,u.Rebate,u.UserType,u.PlayType from SysQuotas_View as w inner join SysYtgUser as u on w.SysUserId=u.Id where 1=1";
            if (!string.IsNullOrEmpty(userCode))
                sql += " and u.Code like '%" + userCode + "%'";
            if (userType != -1)
                sql += " and u.UserType=" + userType;
            if (playType != -1)
                sql += " and u.PlayType=" + playType;
            if (!string.IsNullOrEmpty(otherWhere))
                sql += " " + otherWhere;
            sql = "(" + sql + ") as t1";
            int pageCount = 0;
            return this.GetEntitysPage<SysQuotas_View_Ref>(sql, "Code", "*","Code", ESortType.ASC, pageIndex, pageSize, ref pageCount, ref totalCount);
        }
    }
}