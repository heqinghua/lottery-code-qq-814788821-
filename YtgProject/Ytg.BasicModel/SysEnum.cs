using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Ytg.BasicModel
{
    /// <summary>
    /// 系统错误编码
    /// </summary>
    public enum ApiCode
    {

        /// <summary>
        /// 操作成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 未知错误 失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// 编号001代表 参数错误
        /// </summary>
        ParamEmpty = 1001,

        /// <summary>
        /// 验证失败
        /// </summary>
        ValidationFails = 1002,

        /// <summary>
        /// 账号禁用
        /// </summary>
        DisabledCode = 1003,
        /// <summary>
        /// 身份验证失败
        /// </summary>
        Security = 1004,

        /// <summary>
        /// 请求失败
        /// </summary>
        RequestFail = 1005,

        /// <summary>
        /// 请求成功，结果为空
        /// </summary>
        Empty = 1006,
        /// <summary>
        /// 余额不够
        /// </summary>
        NotEnough = 1007,
        /// <summary>
        /// 异常
        /// </summary>
        Exception = 1008,

        /// <summary>
        /// 账号在其他地方登录，被迫下线
        /// </summary>
        ExitLogin = 1009,

        /// <summary>
        /// 以参与过活动
        /// </summary>
        MarketExist = 1010,

        /// <summary>
        /// 不在范围之内
        /// </summary>
        NotScope = 1011,
        /// <summary>
        /// 超出限制注数
        /// </summary>
        GoBeyond = 1002,
        /// <summary>
        /// 暂停销售
        /// </summary>
        NotSell = 1003,
        /// <summary>
        /// 禁用ip
        /// </summary>
        DisabledIp=2000,

        /// <summary>
        /// 禁用资金密码
        /// </summary>
        DisabledMonery=2001
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 普通会员
        /// </summary>
        General = 0,

        /// <summary>
        /// 代理用户
        /// </summary>
        Proxy = 1,

        /// <summary>
        /// 管理用户
        /// </summary>
        Manager = 2,
        /// <summary>
        /// 总代用户
        /// </summary>
        BasicProy = 3,
        /// <summary>
        /// 总管用户，
        /// </summary>
        Main = 4,
        /// <summary>
        /// 客服用户 只限于使用聊天程序，其他则无用
        /// </summary>
        Customer = 20,
        
    }

    /// <summary>
    /// 用户奖金类型
    /// </summary>
    public enum UserPlayType
    {
        /// <summary>
        /// 1800 奖金
        /// </summary>
        P1800 = 0,
        /// <summary>
        /// 1700 奖金
        /// </summary>
        P1700 = 1
    }


    /// <summary>
    /// 交易类型
    /// </summary>
    public enum TradeType
    {
        用户充值 = 1,
        用户提现 = 2, 
        投注扣款 = 3,
        /// <summary>
        /// 追号的时候 一次性扣款
        /// </summary>
        追号扣款 = 4,
        /// <summary>
        /// 有追号的时候，是一次性扣款，等到了某一期，先将之前一次性扣的款 对应的这一期的钱退给客户，这个叫做追号返款。
        /// </summary>
        追号返款 = 5,
        /// <summary>
        /// 本人投注，立即产生游戏返点，开奖之后才计算游戏返点 给上级
        /// </summary>
        游戏返点 = 6,
        /// <summary>
        /// 中奖之后派送奖金
        /// </summary>
        奖金派送 = 7,
        /// <summary>
        /// 撤单返款包括 撤销投注、撤销某一期的追号,
        /// </summary>
        撤单返款 = 8,
        /// <summary>
        /// 官网没有开奖，系统撤单，系统撤单需要考虑的事情：有没有开奖
        /// </summary>
        系统撤单 = 9,
        /// <summary>
        /// 投注之后会产生返点给自身，所以撤单的时候会产生撤销返点数据，撤销派奖的时候也会产生撤销返点数据
        /// </summary>
        撤销返点 = 10,
        /// <summary>
        /// 撤消派奖,应该是官网开奖时间变更,与平台的开奖时间不一致,所以增加的这项功能
        /// 相当于系统撤单
        /// </summary>
        撤销派奖 = 11,
        /// <summary>
        /// 给下级充值，自己就要扣费
        /// </summary>
        充值扣费 = 12,
        上级充值 = 13,
        活动礼金 = 14,
        分红 = 15,
        分红扣款 = 26,
        提现失败 = 16,
        撤销提现 = 17,

        /// <summary>
        /// 活动
        /// </summary>
        满赠活动=18,
        签到有你 = 19,
        注册活动=20,
        充值活动=21,
        佣金大返利 = 22,
        幸运大转盘 = 23,
        系统充值 = 24,
        投注送礼包=25,

        其他 = 99,
        所有 = -1
    }

    /// <summary>
    /// 配额操作类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 开户扣减配额
        /// </summary>
        开户扣减 = 1,

        /// <summary>
        /// 编辑返点扣减
        /// </summary>
        编辑扣减 = 2,
        /// <summary>
        /// 编辑返点返还
        /// </summary>
        编辑返还 = 3,

        /// <summary>
        /// 向下级发配额
        /// </summary>
        发放配额 = 4,
        /// <summary>
        /// 接收上级配额
        /// </summary>
        接收配额 = 5,

        /// <summary>
        /// 上级回收配额
        /// </summary>
        返还配额 = 6,
        /// <summary>
        /// 回收下级配额
        /// </summary>
        回收配额 = 7,


        /// <summary>
        /// 公司回收配额
        /// </summary>
        系统扣减 = 8,
        /// <summary>
        /// 公司发放配额
        /// </summary>
        系统增加 = 9,

        /// <summary>
        /// 升点扣减配额
        /// </summary>
        升点扣减 = 10,
        /// <summary>
        /// 升点增加配额
        /// </summary>
        升点增加 = 11,

        /// <summary>
        /// 降点扣减配额
        /// </summary>
        降点扣减 = 12,
        /// <summary>
        /// 降点增加配额
        /// </summary>
        降点增加 = 13,
    }




    /// <summary>
    /// 投注状态状态：1 已中奖、2 未中奖、3 未开奖、4 已撤单
    /// </summary>
    public enum BetResultType
    {
        /// <summary>
        /// 已中奖
        /// </summary>
        Winning = 1,
        /// <summary>
        /// 未中奖
        /// </summary>
        NotWinning = 2,
        /// <summary>
        /// 未开奖
        /// </summary>
        NotOpen = 3,
        /// <summary>
        /// 本人撤单
        /// </summary>
        Cancel = 4,
        /// <summary>
        /// 系统撤单
        /// </summary>
        SysCancel=5
    }

    /// <summary>
    /// 追号状态
    /// </summary>
    public enum CatchNumType
    {
        /// <summary>
        /// 正在进行
        /// </summary>
        Runing = 0,
        /// <summary>
        /// 已完成
        /// </summary>
        Compled = 1,
        /// <summary>
        /// 已撤单
        /// </summary>
        Cancel = 2
    }

    public class OpenUser
    {
        /// <summary>
        /// 系统管理
        /// </summary>
        public const string System = "系统管理";

        /// <summary>
        /// 上级代理
        /// </summary>
        public const string ParentUser = "上级代理";
    }

    /// <summary>
    /// 充提现 设置资金密码，绑定银行卡，两天投注金额大于充值金额的5%
    /// </summary>
    [Flags]
    public enum RechargeMentionStatus
    {
        /// <summary>
        /// 什么都没有
        /// </summary>
        None = 0,
        SetBalancePwd = 1,
        BindBank = 2,
        AllMention = 4
    }
}
