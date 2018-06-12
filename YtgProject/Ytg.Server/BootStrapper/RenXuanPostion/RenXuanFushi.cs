using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ytg.BasicModel.LotteryBasic.DTO;
using Ytg.Comm;
using Ytg.Scheduler.Comm.Bets.Calculate;

namespace Ytg.ServerWeb.BootStrapper.RenXuanPostion
{
    public class RenXuanFushi
    {
        /// <summary>
        /// 拆分复试
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static List<BetDetailDTO> Split(BetDetailDTO dto, int minPos)
        {
            List<BetDetailDTO> dtos = new List<BetDetailDTO>();
            if (string.IsNullOrEmpty(dto.BetContent))
                return dtos;

            var contentArray = dto.BetContent.Split('|');
            List<int> indexSource = new List<int>();
            for (var i = 1; i <= contentArray.Length; i++)
            {
                var cont = contentArray[i - 1];
                if (!string.IsNullOrEmpty(cont))
                {
                    indexSource.Add(i);
                }
            }

            Combinations<int> v = new Combinations<int>(indexSource, minPos);

            foreach (var item in v._combinations)
            {
                string writerPos = "";
                string writerContent = "";
                foreach (var x in item)
                {
                    writerPos += x;
                    writerContent += contentArray[x-1]+"|";
                }
                if (writerContent.EndsWith("|"))
                    writerContent = writerContent.Substring(0, writerContent.Length-1);

                var detail = dto.Copy();
                detail.PostionName = Utils.GetPostionName(writerPos).Replace("位","");
                detail.BetContent = writerContent+"_"+ writerPos;
                dtos.Add(detail);
            }

            return dtos;
        }

        /// <summary>
        /// 拆分其他 单式+位置
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="minPos"></param>
        /// <returns></returns>
        public static List<BetDetailDTO> DanShiSplit(BetDetailDTO dto, int minPos)
        {
            List<BetDetailDTO> res = new List<BetDetailDTO>();
            //12&34&56_12345
            if (string.IsNullOrEmpty(dto.BetContent))
                return res;
            var contentArray = dto.BetContent.Split('_');
            if (contentArray.Length != 2)
                return res;
            var postion = contentArray[1].Select(x => Convert.ToInt32(x.ToString())).Where(c=>c>=1 && c<=5).ToList();
            if (postion.Distinct().Count() != postion.Count)
                return res;
            if (postion.Count() < minPos)
            {

                return res;
            }
            Combinations<int> v = new Combinations<int>(postion, minPos);
            foreach (var item in v._combinations)
            {
                string newContent = contentArray[0];
                string postr = "";
                for (var i = 0; i < minPos; i++)
                {
                    postr += item[i].ToString();
                }

                var detail = dto.Copy();
                detail.BetContent = newContent + "_" + postr;
                detail.PostionName = Utils.GetPostionName(postr).Replace("位", "");
                res.Add(detail);
            }
            return res;
        }

    }
}