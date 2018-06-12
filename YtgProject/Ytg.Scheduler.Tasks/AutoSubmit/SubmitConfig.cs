using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Scheduler.Tasks.AutoSubmit
{
    public class SubmitConfig
    {
        public string title { get; set; }


        public string lotteryCode { get; set; }

        public List<BasicModel.DTO.HtmlParamDto> radios { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public int LotteryId { get; set; }


        /// <summary>
        /// 加载config文件
        /// </summary>
        /// <returns></returns>
        public static List<SubmitConfig> Load()
        {
            //@"Bets/RadioConfig.xml"
            List< SubmitConfig> configs = null;
            using (var stream = System.IO.File.OpenText(@"AutoSubmit\submitConfig.txt"))
            {
                string content= stream.ReadToEnd();
                configs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SubmitConfig>>(content);
            }

            return configs;

        }

    }
}
