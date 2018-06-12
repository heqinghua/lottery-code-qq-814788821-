using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.BasicModel.DTO;
using Ytg.BasicModel.LotteryBasic.DTO;

namespace Ytg.Core.Service.Lott
{
    [ServiceContract]
    public interface IPlayTypeRadioService : ICrudService<PlayTypeRadio>
    {
        /// <summary>
        /// 获取玩法单选
        /// </summary>
        /// <param name="lottTypeId"></param>
        /// <param name="palyTypeId"></param>
        /// <returns></returns>
        [OperationContract]
        List<PlayRado> GetPattRado(string lotteryCode, string radioCode);

        [OperationContract]
        bool UpdateStatus(int playTypeRadioId, bool isEnable);

        /// <summary>
        /// 修改玩法单选
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isenale"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdatePlayRadio(PlayRado item);

        [OperationContract]
        IEnumerable<PlayTypeRadio> GetPlayTypeRadios();

        /// <summary>
        /// 根据彩种获取玩法单选
        /// </summary>
        /// <param name="playTypeId"></param>
        /// <returns></returns>
        List<PlayRadioHtmlDTO> GetRadios(int playTypeId);


        /// <summary>
        /// 查询lottery_vw数据
        /// </summary>
        /// <returns></returns>
        List<Lottery_VwDTO> GetLottery_VwDTO();
    }
}
