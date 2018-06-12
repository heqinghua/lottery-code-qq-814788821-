using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Ytg.ServerWeb.Views.Help
{
    /// <summary>
    /// help 的摘要说明
    /// </summary>
    public class help : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string result = "";
            if (!string.IsNullOrEmpty(context.Request["action"]))
            {
                string title = context.Request["title"].ToString();
                string action = context.Request["action"].ToString();
                if (action == "zc")
                {
                    result=GetHelpText(new List<HelpModel>() { 
                         new HelpModel(){ Title="如何注册？",Text="用户寻找乐诚网代理进行开户注册，提供给代理账号•密码•昵称等， 乐诚网代理将会给您开设账户。"},
                         new HelpModel(){ Title="注册时用户需要注意什么？",Text="用户名一旦提交，不可更改，请选择容易记忆且安全级别高的用户名，并妥善保管； 初始密码是不安全的，进入平台后，请先修改您的密码。"}
                    }, title);
                }
                else if (action == "dl")
                {
                    result=GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="登录时为什么看不到验证码或总是提示验证码不正确？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="看不到验证码或者总是提示验证码输入错误，可能的原因有： "},
                                new HelpModel(){Text="A：网速过慢，请多刷新几次页面； "},
                                new HelpModel(){Text="B：输入错误，数字有全角和半角之分，请换用半角方式输入；"},
                                new HelpModel(){Text="C：安全级别设置过高，请将IE的安全级别设为“默认级别”； "},
                                new HelpModel(){Text="D：网页没有自动更新，请先设置浏览器的“Internet临时文件”为“自动”更新或使用Ctrl+F5刷新网页。 "},
                                new HelpModel(){Text="E：建议您使用IE8及以上的浏览器，最好选择火狐浏览器和谷歌浏览器登录平台 "}
                             }
                         },
                         new HelpModel()
                         { 
                             Title=" 登录时忘记了密码怎么办？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：如果忘记平台登录密码，请点击用户登录区的“忘记密码”，输入资金密码，也可登陆乐诚网平台，为保障账户安全，建议用户登录后立即修改密码； "},
                                new HelpModel(){Text="B：如果忘记平台登陆密码和资金密码，请联系您的上级或者平台客服，提供绑定的银行卡信息进行核实，如果属实，可重新设定密码； "},
                                new HelpModel(){Text="C：如果…如果忘记与平台所相关的所有账号信息，请参考帮助中心的“如何注册”，重新申请账号。 "}
                             }
                         },
                         new HelpModel()
                         { 
                             Title="如何修改登录密码？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：登录后进入“个人中心”，点击“修改密码”。 "},
                                new HelpModel(){Text="B：在“修改密码”条目下选择“修改登录密码”选项。"},
                                new HelpModel(){Text="C：用户输入旧密码，并设置新密码。"},
                                new HelpModel(){Text="D：提交信息，修改密码成功，系统提示成功信息。 "}
                             }
                         },
                         new HelpModel()
                         { 
                             Title="注册登录需要注意什么？",
                             Text="请认准乐诚网平台域名验证地址：http://www.boyuesite.com"
                         },
                         new HelpModel()
                         { 
                             Title="登录过程中为什么会跳转到谷歌界面？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：进行登陆时的用户名输入错误； "},
                                new HelpModel(){Text="B：您的账号不存在； "},
                                new HelpModel(){Text="C：当前域名为非系统指定域名，请联系上级或平台客服获取最新域名地址。 "}
                             }
                         }
                    }, title);
                }
                else if (action == "yzm")
                {
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel(){ Title="验证码有何作用？",Text="设置验证码是为了提高系统的安全性，它可以有效防止针对某个特定账号的暴力破解（即用特定程序进行不断的登录尝试），使用验证码也是目前各大平台网站通行的安全措施。"}
                    }, title);
                }
                else if (action == "zhqc")
                {
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="什么情况下账户会被删除？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：14天内在平台没有账变记录"},
                                new HelpModel(){Text="B：在平台余额小于或等于10元"},
                                new HelpModel(){Text="C：同时满足以上2个条件的账户将被系统自动删除"},
                                new HelpModel(){Text="D：如果同时满足以上两个条件的账号有下级，并且下级账号还未被删除，那此账号也会一直被保留 "}                                
                             }
                         }
                    }, title);
                }
                else if (action == "ltgj")
                {
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel(){ Title="如何使用上下级聊天工具？",Text="乐诚网开通了上下级聊天工具，进入网站右下角有图标，点击即可展开。你可以看到自己的上级和下级每个用户，和在线与否。可以通过该工具与上下级便捷沟通。"}
                    }, title);
                }
                else if (action == "zjaq")
                { 
                    //资金安全
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="用户为保证账户资金安全应注意哪些事项？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：用户必须注意自己电脑中是否有键盘记录或远程控制等木马程序，使用病毒实时监控程序和网络防火墙并注意升级更新操作系统厂商所提供的安全补丁"},
                                new HelpModel(){Text="B：尽量不要在公共场所（如网吧）等登录本平台"},
                                new HelpModel(){Text="C：尽量在不同场合使用有所区别的密码"},
                                new HelpModel(){Text="D：牢记密码，如作记录则应妥善保管 "},            
                                new HelpModel(){Text="E：密码不得告诉他人，包括自己的亲朋好友 "},
                                new HelpModel(){Text="F：在登录或网上支付密码输入时，应防止左右可疑的人窥视 "},
                                new HelpModel(){Text="G：设定密码时不要选用您的身份证、生日、电话、门牌、吉祥、重复或连续等易被他人破译的数字。建议选用既不易被他人猜到，又方便记忆的密码 "},
                                new HelpModel(){Text="I：发现泄密的危险时，及时更换密码 "},
                                new HelpModel(){Text="J：不定期更换密码 "},
                             }
                         }
                    }, title);
                }
                else if (action == "zjmm")
                { 
                    //资金密码
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel(){ Title="什么是资金密码？",Text="资金密码是保障您资金账户安全的一道防线，能有效的降低因账户被盗而引发的经济损失。为了用户资金安全，平台强制使用，请确保登录密码与资金密码不同。"},
                         new HelpModel(){ Title="忘记资金密码怎么办？",Text="如果您忘记了自己的资金密码，请联系乐诚网“在线客服”，通过银行卡的绑定信息，重新设定新的资金密码。"}
                    }, title);
                }
                else if (action == "zhaq")
                {
                    //账号安全
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel(){ Title="为什么登录后长时间不操作，需要重新登录平台？",Text="用户登录后，长时间不操作电脑，系统就认为用户离开自己的电脑了。为了避免别人趁机使用您的账户投注和保护乐诚网用户账户的安全，系统有要求用户重新登录的功能设定。"},
                         new HelpModel(){ Title="乐诚网平台中奖者的个人信息是否安全？",Text="您的个人信息是保密的，本平台采用国际先进加密技术，交易安全放心，请您放心购买。"}
                    }, title);
                }
                else if (action == "wsyhaq")
                {
                    //网上银行安全
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="使用网上银行安全吗？",Text="用户在乐诚网平台充值时选择任一银行卡支付通道后立即进入银行网关，银行卡资料全部在银行网关加密页面上填写，资料提交过程全部采用国际通用的SSL或SET及数字证书进行加密传输，安全性由银行全面提供支持和保护，各银行网上支付系统对网上支付的安全提供保障。"}
                    }, title);
                }
                else if (action == "czzysx")
                {
                    //充值注意事项
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="通过网上充值，出现页无法显示时怎么办？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：没有升级IE浏览器，导致加密级别过低，无法进入银行系统。 请升级到8.0及以上版本。"},
                                new HelpModel(){Text="B：上网环境或上网方式受限，可能是网络服务商限制，如有条件更换一种上网方式或环境。 "},
                                new HelpModel(){Text="C：瞬间网络不通，尝试刷新页面；如果刷新不能解决问题，可能由于浏览器设置缓存，请在IE菜单—工具—Internet选项—点击“删除cookies”和“删除文件”，用以清除临时文件。"}
                             }
                         },
                         new HelpModel()
                         { 
                             Title="为何充值不到帐？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：平台填写的存款金额和实际汇款金额不一致，会导致游戏平台不到账。"},
                                new HelpModel(){Text="B：使用网上银行在线充值，充值完成后一定要点击返回商户按钮 。 "},
                                new HelpModel(){Text="C：存款时没有从平台发起存款请求或者没有使用平台最新收款银行信息。"},
                                new HelpModel(){Text="D：如果因用户没有使用平台的最新收款信息而汇款到旧的收款银行帐号而损失，须由用户自行承担。"},
                                new HelpModel(){Text="E：部分银行垮地区转账无法实时到账，少则几分钟，多则数小时，请耐心等待。"},
                             }
                         },
                         new HelpModel()
                         { 
                             Title=" 银行充值提款的手续费怎么收取？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：我们会返还您的充值手续费至平台游戏币中。"},
                                new HelpModel(){Text="B：我们不收取提款手续费，无论是跨行或跨地区。 "},
                                new HelpModel(){Text="C：若您提款需要汇入的银行是跨行或跨地区可能会有部分时间延误问题，少则几分钟，多则数小时，请耐心等待。"}
                             }
                         }
                    }, title);
                }
                else if (action == "czxe")
                {
                    //充值限额
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                      new HelpModel(){ Title="平台服务时间",Text="单次充值限额：10元 - 50000元"}   
                    }, title);
                }
                else if (action == "czfwsj")
                {
                    //充值服务时间
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="平台充值限额",Text="在线充值时间：7*24小时自动到账"}
                    }, title);
                }
                else if (action == "yhk")
                { 
                    //银行卡
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="如何修改已绑定银行卡？",Text="若您需要修改银行卡信息，而该信息已被绑定，无法修改，请点击“在线客服”与平台客服人员联系，客服与您确认个人信息会给您解除绑定，您就可以修改银行卡信息了。"}
                    }, title);
                }
                else if (action == "tk")
                { 
                   //提款
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title=" 如何提款？",Text="<img src=\"resource/images/chongzhi.png\" />"},
                         new HelpModel(){ Title=" 提款什么时候可以到账？",Text="自您发起提款后的5分钟内处理完毕，如因遇到网银系统问题或者其他不可抗力因素影响，到帐时间将会延迟。"},
                         new HelpModel(){ 
                              
                             Title="为什么我的提款没有成功或者没有及时到账？", 
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="提款不成功分两种情况："},
                                new HelpModel(){Text="① 提款申请未成功："},
                                new HelpModel(){Text="A、提款金额大于账户实有金额"},
                                new HelpModel(){Text="B、提款时填写的银行卡姓名与用户注册时提交的用户真实姓名不一致"},
                                new HelpModel(){Text="C、银行卡信息不符合平台规定格式"},
                                new HelpModel(){Text="② 提款申请成功，但提款失败"},
                                new HelpModel(){Text="A、银行卡卡号填写错误，与用户姓名不符。银行退单"},
                                new HelpModel(){Text="B、提供的银行卡银行不接受公司对个人用户进行转账"},
                                new HelpModel(){Text="一旦发生上述现象，可与乐诚网平台的在线客服联系"}
                             }},
                         new HelpModel(){ Title="如何查询提款记录？",Text="充值提现->提现记录 查询提款记录。"},
                    }, title);
                }
                else if (action == "tkgd")
                {
                    //提现规定
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="平台对于提款有何规定？",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：对于新开的用户需要自开户 6 小时后方可发起提款。"},
                                new HelpModel(){Text="B：新增银行卡（含新增，修改）6 个小时后方可发起提款。 "},
                                new HelpModel(){Text="C：存款后总投注额小于总存款额 5% 的提款将不予受理，如有特殊情况，请咨询在线客服申请处理。"},
                                new HelpModel(){Text="D：每天限提5次， 单次提款金额最低100元， 最高50,000元。"},
                                new HelpModel(){Text="E：提现时间为10:00am-次日02:00。"},
                             }
                         }
                    }, title);
                }
                else if (action == "czlc")
                { 
                    //充值流程
                    result = GetHelpText(new List<HelpModel>() { 
                        new HelpModel(){ Title="1 点击充值提现→在线充值",Text="<img src=\"resource/images/deposit/1.png\" />"},
                        new HelpModel(){ Title="2 完善充值信息中，写入充值金额，点击下一步(若未自动打开充值页面，请点击“如果没有弹出支付窗口，请点此处”按钮 )",Text="<img src=\"resource/images/deposit/1.png\" />"},
                        new HelpModel(){ Title="2 完善充值信息中，写入充值金额，点击下一步(若未自动打开充值页面，请点击“如果没有弹出支付窗口，请点此处”按钮 )",Text="<img src=\"resource/images/deposit/1.png\" />"},
                     }, title);


                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="在线充值步骤",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="1 点击充值提现→在线充值<br/><img src=\"resource/images/deposit/1.png\" />"},
                                new HelpModel(){Text="2 完善充值信息中，写入充值金额，点击下一步(若未自动打开充值页面，请点击“如果没有弹出支付窗口，请点此处”按钮 )<br/><img src=\"resource/images/deposit/1.png\" />"},
                                new HelpModel(){Text="3 系统自动跳转至选择的支付银行的网上银行进行支付。"},
                                new HelpModel(){Text="4 完成充值，同时请检查您的账户余额。<br/><br/>"}
                             }
                         }
                    }, title);
                }
                else if (action == "cpdg")
                {
                    //彩票代购
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="什么叫彩票代购？",Text="彩票代购指的是由个人出资委托本平台购买所需的彩票，乐诚网平台统筹所有会员各式投注金额合并下注，由本站代理出票，兑奖和派奖。彩票代购无需您外出排队买彩票，不受自然因素困扰，既快捷、方便又高效、安全。"}
                    }, title);
                }
                else if (action == "tzcd")
                {
                    //投注撤单
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="如何修改已绑定银行卡？",Text="当然可以，只要您投注的彩票在该期还未截止销售，您的下注就还可以撤销并重新下注。 撤单方法为，点击“投注记录”，在投注记录里头点击“查看详情”，在订单详情页面可以进行“撤单”操作。撤单的前提是该类彩票还未到截止销售时间点。 追号单的撤单需要到 \"投注记录 ->追号投注查询\" 里面进行终止任务。"}
                    }, title);
                }
                else if (action == "tzxe")
                {
                    //投注限额
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel(){ Title="什么是赔付限额？",Text="赔付限额是指该平台账户在某个玩法的某一注号码投注所能获得的最大赔付总额，平台目前设定的最高赔付限额为40万（3D和P3奖金限额为10万）。"},
                         new HelpModel()
                         { 
                             Title="参与游戏时提示“奖金超过上限”问题",
                             CNode=new List<HelpModel>(){
                                new HelpModel(){Text="A：每个账号在同一游戏、同一玩法、同一奖期中购买相同号码的最大可中奖金额。 "},
                                new HelpModel(){Text="B：高频彩（时时彩、时时乐、11选5）的奖金限额40万，低频彩（福彩3D、体彩P3）的奖金限额10万 "}
                             }
                         }
                    }, title);

                    //• 什么是赔付限额？
                }
                else if (action == "xsrj")
                {
                    //销售时间
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="彩种销售时间",
                             CNode=new List<HelpModel>(){
                               new HelpModel(){Text="A：福彩3D销售周期为：每日上午07：00至晚上20：20。 "},
                             }
                         }
                    }, title);
                }
                else if (action == "gmjl")
                {
                    //购买记录
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="为什么我一个月前的购买记录查不到了?",Text="目前平台只保留5天的历史记录，之前的投注记录会被系统自动清理。"}
                    }, title);
                }
                else if (action == "eytz")
                {
                    //恶意投注
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="什么是恶意投注？",Text="恶意投注：用户使用不当程序在平台下注或者已经获知官方开奖结果等类似的情况下，在平台下注的行为视为“恶意投注”，基于公平原则，平台将予以冻结处理。（“恶意投注”的最终解释权归平台所有）"}
                    }, title);
                }
                else if (action == "zhtz")
                {
                    //追号投注
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="什么是追号投注？",Text="追号指将一注或一组号码进行两期或两期以上的投注。追号可分为连续追号和间隔追号，连续追号指追号的期数是连续的，间隔追号指追号的期数不连续。追号可增加您所选号码的中奖概率：）"}
                    }, title);
                }
                else if (action == "dstz")
                {
                    //单式投注
                    result = GetHelpText(new List<HelpModel>() { 
                         new HelpModel()
                         { 
                             Title="为什么单式投注时号码导入不成功？",
                             CNode=new List<HelpModel>(){
                               new HelpModel(){Text="A：号码导入，有两点要求：号码导入时，必须是文本格式（.txt）。 "},
                               new HelpModel(){Text="B：文本内容有格式要求(不同玩法不同要求，请按照每个单式玩法的号码要求进行号码编辑)。 "},
                               new HelpModel(){Text="D：确认要导入的号码文件位置,如果没有请新建(英文状态下书写)。 "}
                             }
                         }
                    }, title);
                }
                else if (action == "wkjcl")
                {
                    //未开奖处理
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="第3方开奖机构不开奖，投注单如何处理？",Text="时时彩系列，11选5系列，上海时时乐等彩种，若官方超过30分钟未开奖，平台将对超过30分钟的期数进行撤单返款，依次顺延，撤单后无论第三方以任何形式补开，平台均维持撤单处理。"}
                    }, title);
                }
                else if (action == "qzgl")
                { 
                    //圈子管理
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="什么叫圈子管理？",Text="圈子管理就是代理团队管理"},
                         new HelpModel(){ Title="圈子管理的好处有哪些？",Text="单独将代理团队涉及的管理及内容展示在圈子管理中，更加团队管理•查询及处理问题，逻辑更加清晰流畅"},
                         new HelpModel(){ Title="什么叫圈子管理？",Text="圈子管理就是代理团队管理"},
                         new HelpModel()
                         { 
                             Title="圈子管理中我的团队有哪些内容？",
                             CNode=new List<HelpModel>(){
                               new HelpModel(){Text="A：团队余额：我的代理团队的余额。 "},
                               new HelpModel(){Text="B：我的返点：我在乐诚网所获得的返点 。 "},
                               new HelpModel(){Text="C：奖金限额：我在同一游戏、同一玩法、同一奖期中购买相同号码的最大可中奖金额度。 "},
                               new HelpModel(){Text="D：代理团队：我的下级及下级相关游戏数据。 "},
                               new HelpModel(){Text="E：用户名查询：直接查找我的指定下级。 "},
                             }
                         }
                    }, title);
                }
                else if (action == "zjyh")
                {
                     //增加用户
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel()
                         { 
                             Title="如何增加用户？",
                             CNode=new List<HelpModel>(){
                               new HelpModel(){Text="A.增加用户： 点击进入 用户管理=>增加用户页面，选择需要新增用户的“用户级别”，输入“登陆账号”、“登陆密码”、“用户昵称”点击“确认注册按钮”"},
                               new HelpModel(){Text="B：2.推广设定： 点击进入 用户管理=>推广设定，设定好自身保留的返点，然后复制“您的推广注册地址”发送给用户，用户填写账号、昵称后即注册成功"}
                             }
                         }
                    }, title);
                }
                else if (action == "zxkf")
                {
                    //在线客服
                    result = GetHelpText(new List<HelpModel>() 
                    { 
                         new HelpModel(){ Title="乐诚网在线客服服务时间是什么时候？",Text="在线客服服务时间为：早上09：00-次日03:00。"}
                    }, title);
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
            context.Response.End();
        }

        private string GetHelpText(List<HelpModel> list,string title)
        {
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append("<div class=\"help-list-title\">" + title + "</div>");
            sbStr.Append("<ul class=\"help-list-normal clearfix\"></ul>");
            for (int i = 0; i < list.Count; i++)
            {
                sbStr.Append("<li><br/>");
                sbStr.Append("<p class=\"help-list-name\"><span class=\"help-list-num\">" + (i + 1).ToString() + "</span><a href=\"#\">" + list[i].Title + "</a></p>");
                if (string.IsNullOrEmpty(list[i].Text) && list[i].CNode != null)
                {
                    sbStr.Append("<div class=\"help-list-text\">");
                    for (int j = 0; j < list[i].CNode.Count; j++)
                    {
                        sbStr.Append("<p>" + list[i].CNode[j].Text + "</p>");
                    }
                    sbStr.Append("</div>");
                }
                else
                {
                    sbStr.Append("<p class=\"help-list-text\">");
                    sbStr.Append(list[i].Text);
                    sbStr.Append("</p>");
                }
                sbStr.Append("</li>");
            }
            sbStr.Append("</ul>");
            return sbStr.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}