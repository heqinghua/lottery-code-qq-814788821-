using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.ServerWeb.BootStrapper.RenXuanPostion;

namespace Ytg.ServerWeb.BootStrapper
{
    /// <summary>
    /// 拆分投注内容
    /// </summary>
    public static class PostionSplitHelper
    {
        static double Percentage = 80 / 100;
        static PostionSplitHelper()
        {
            Percentage = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["percentage"]) / 100;
        }
      

        public static List<BetDetailDTO> Split(BetDetailDTO dto)
        {
            List<BetDetailDTO> res = new List<BetDetailDTO>();
            //是否需要拆分
            var entity = Ytg.Scheduler.Comm.Bets.RadioContentFactory.PostionLottery(dto.PalyRadioCode);
            int betMaxCount = 0;
            if (null != entity)
                betMaxCount =(int) (entity.MaxBetCount * Percentage);
            if (betMaxCount == 0)
                betMaxCount = 9999;
            dto.MaxBetCount = betMaxCount;

            if (string.IsNullOrEmpty(entity.Postion))
            {
                res.Add(dto);
                return res;
            }

            switch (entity.Postion)
            {
                case "DingWeiDanSSC":
                    res = SplitDingWeiDanSSC(dto,0); //拆分定位胆
                    break;
                case "DingWeiDan11x5":
                    res = SplitDingWeiDanSSC(dto,0,","); //拆分定位胆
                    break;
                case "DingWeiDan3d":
                    if (string.IsNullOrEmpty(dto.BetContent))
                        dto.BetContent = "&,&," + dto.BetContent;
                    res = SplitDingWeiDanSSC(dto,2); //拆分定位胆
                    break;
                case "RenXuanFuShi":  //任选复试
                    res = RenXuanFushi.Split(dto,entity.MinPos);
                    break;
                case "RenX":
                    res = RenXuanFushi.DanShiSplit(dto, entity.MinPos);
                    break;
                case "PK10DingWeiDanSSC"://pk10定位胆
                    //dto.PostionName = "定位胆";
                    //res.Add(dto);
                     res = SplitPk10DingWeiDan(dto, 0,"&"); 
                    break;
            }
            
            return res;

        }
        #region 定位胆
        //1 2 3 4 5  最多5位万 千 百 十 个  11选5 福彩是3位百 十 个    11选5 第一位（万） 第二位（千）  第三为（百）
        private static List<BetDetailDTO> SplitDingWeiDanSSC(BetDetailDTO dto, int postionIndex = 0, string res = "")
        {
            List<BetDetailDTO> result = new List<BetDetailDTO>();
            string content = dto.BetContent;
            if (string.IsNullOrEmpty(content))
            {
                result.Add(dto);
                return result;
            }
            var array = content.Replace("&", res).Split('|');
            foreach (var str in array)
            {
                postionIndex++;
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                var newItem = dto.Copy();
                newItem.BetContent = str + "_" + postionIndex;
                newItem.PostionName = Utils.GetPostionName(postionIndex);
                result.Add(newItem);
            }

            return result;
        }

        #endregion

        #region pk10定位胆
        private static List<BetDetailDTO> SplitPk10DingWeiDan(BetDetailDTO dto, int postionIndex = 0, string res = "")
        {
            List<BetDetailDTO> result = new List<BetDetailDTO>();
            string content = dto.BetContent;
            if (string.IsNullOrEmpty(content))
            {
                result.Add(dto);
                return result;
            }
            var array = content.Replace("&", res).Split('|');
            foreach (var str in array)
            {
                postionIndex++;
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                var newItem = dto.Copy();
                newItem.BetContent = str + "_" + postionIndex;
                newItem.PostionName = Utils.GetPostionPk10Name(postionIndex);
                result.Add(newItem);
            }

            return result;
        }

        #endregion


    }
}