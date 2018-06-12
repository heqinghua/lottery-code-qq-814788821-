using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.Core.Repository;
using Ytg.Core.Service.Lott;

namespace Ytg.Service.Lott
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PlayTypeRadioService : CrudService<PlayTypeRadio>, IPlayTypeRadioService
    {
        public PlayTypeRadioService(IRepo<PlayTypeRadio> repo)
            : base(repo)
        {
        }


        public List<BasicModel.LotteryBasic.DTO.PlayRado> GetPattRado(string lotteryCode,string radioCode)
        {

            string sql = @" select * from [dbo].[Lottery_Vw] where 1=1 ";
            if (!string.IsNullOrEmpty(lotteryCode))
                sql += string.Format(" and lotteryCode ='{0}' ", lotteryCode);
            if (!string.IsNullOrEmpty(radioCode))
                sql += string.Format("  and PlayTypeRadioName like '%{0}%'", radioCode);

            return this.GetSqlSource<BasicModel.LotteryBasic.DTO.PlayRado>(sql);
        }

        public List<PlayRadioHtmlDTO> GetRadios(int playTypeId)
        {
            string sql = "select '('+PlayTypeName+')'+playTypeRadioName as playTypeRadioName,RadioCode from lottery_vw  where id=" + playTypeId;
            return this.GetSqlSource<PlayRadioHtmlDTO>(sql);
        }

        public bool UpdateStatus(int playTypeRadioId,bool isEnable)
        {
            var p = this.Get(playTypeRadioId);
            if (p == null)
                return false;
            p.IsEnable = isEnable;
            this.Save();
            return true;
        }


        /// <summary>
        /// 修改玩法单选
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdatePlayRadio(BasicModel.LotteryBasic.DTO.PlayRado item)
        {
            if (null == item)
                return false;

            var info = this.Where(c => c.RadioCode == item.RadioCode).FirstOrDefault();
            if (null == info)
                return false;

            info.BonusBasic = item.BonusBasic;
            info.BonusBasic17 = item.BonusBasic17;
            info.IsEnable = item.radioIsEnable;
            info.IsFixed = item.IsFixed;
            info.MaxBetCount = item.MaxBetCount;
            info.MaxBetMonery = item.MaxBetMonery;
            info.MaxBonus = item.MaxBonus;
            info.MaxBonus17 = item.MaxBonus17;
            info.MaxRebate = item.MaxRebate;
            info.PlayTypeRadioName = item.PlayTypeRadioName;
            info.MaxRebate1700 = item.MaxRebate1700;

            this.Save();
            return true;
        }


        public IEnumerable<PlayTypeRadio> GetPlayTypeRadios()
        {
            return this.GetAll();
        }

        /// <summary>
        /// 查询lottery_vw数据
        /// </summary>
        /// <returns></returns>
        public List<Lottery_VwDTO> GetLottery_VwDTO()
        {
            string sql = "select Id,LotteryCode,PlayCode,NumCode,RadioCode,PlayTypeRadioName from Lottery_Vw";

            return GetSqlSource<Lottery_VwDTO>(sql);
        }
    }
}
