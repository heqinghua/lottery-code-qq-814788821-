<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//divD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/divD/xhtml1-transitional.divd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="js/GameValue.js" type="text/javascript"></script>

</head>
<body>
    <div>
        请选择支付方式：
        <input type="radio" name="radio" id="bank" checked="checked" onclick="ChoosePayment();" />银行支付
        &nbsp;
        <input type="radio" name="radio" id="gamecard" onclick="ChoosePayment();" />游戏点卡充值
    </div>
    <div>
        <form id="frm_payment" action="/ekapayDemo/Eka365pay/Send.aspx" method="post" class="democss" target="_blank">
        <div>
            <div style="display: none;">
                商品名称：
            </div>
            <div>
                <label>
                    <table width="100%" border="0" cellspacing="0" cellpadiving="0" style="text-align: left;">
                        <tr id="tr7">
                            <td>
                                <input type="radio" name="rtype" value="967" checked="checked" />工商银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="964" />农业银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="970" />招商银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="965" />建设银行
                            </td>
                        </tr>
                        <tr id="tr8">
                            <td>
                                <input type="radio" name="rtype" value="981" />交通银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="980" />民生银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="974" />深圳发展银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="963" />中国银行
                            </td>
                        </tr>
                        <tr id="tr9">
                            <td>
                                <input type="radio" name="rtype" value="962" />中信银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="972" />兴业银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="977" />浦发银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="986" />光大银行
                            </td>
                        </tr>
                        <tr id="tr10">
                            <td>
                                <input type="radio" name="rtype" value="989" />北京银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="988" />渤海银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="985" />广东发展银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="984" />广州市农信社
                            </td>
                        </tr>
                        <tr id="tr11">
                            <td>
                                <input type="radio" name="rtype" value="984" />广州市商业银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="976" />上海农商银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="973" />顺德农信社
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="982" />华夏银行
                            </td>
                        </tr>
                        <tr id="tr12">
                            <td>
                                <input type="radio" name="rtype" value="979" />南京银行
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="992" />支付宝
                            </td>
                            <td>
                                <input type="radio" name="rtype" value="993" />财付通
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </label>
            </div>
            <div>
                游戏帐号(用户名)：</div>
            <div>
                <label>
                    <input name="txtUserName" type="text" id="txtUserName" size="20" value="" class="input" />
                </label>
                <font class="red">*</font>
            </div>
            <div>
                确认帐号：</div>
            <div>
                <label>
                    <input name="txtUserName2" type="text" id="txtUserName2" size="20" value="" class="input" />
                </label>
                <font class="red">*</font>
            </div>
            <div>
                金额：</div>
            <div>
                <input name="PayMoney" type="text" id="PayMoney" maxlength="9" value="" class="input"
                    style="widivh: 70px;" />元<font class="red">*</font> &nbsp;&nbsp;
            </div>
        </div>
        <div id="submit">
            <label>
                <input class="btn_ok" type="button" onclick="onFormSubmit();" name="button5" id="button5" value="填好提交" />
                <input class="btn_ok" type="reset" name="button5" id="Submit1" value="清空重写" onclick="ClearInfo()" />
            </label>
        </div>

        <script type="text/javascript">
            function ClearInfo(){
                document.getElementById('txtUserName').value = '';
                document.getElementById('txtUserName2').value = '';
            }
            
            function onFormSubmit()
            {
                var txtUserName = document.getElementById('txtUserName');
                var txtUserName2 = document.getElementById('txtUserName2');
                var PayMoney = document.getElementById("PayMoney");
                if(txtUserName.value==""){
                    txtUserName.focus();
                    alert('请输入游戏帐号！（用户名）');
                    return;
                }
                
                if(txtUserName.value != txtUserName2.value){
                    alert('两次输入游戏帐号不一致！');
                    txtUserName2.value="";
                    txtUserName2.focus();
                    return;
                }
                if(isNaN(PayMoney.value)||PayMoney.value=="")
                {
                    PayMoney.value="";
                    PayMoney.focus();
                    alert('输入正确的金额。');
                    return;
                }
                document.getElementById("frm_payment").submit();
            }
         //-->
        </script>

        </form>
        <form id="frm_card" action="Send.aspx" method="post" class="democss" style="display: none;" target="_blank">
        <div style="color: #c9b10f;  font-family: Arial, Helvetica, sans-serif,'宋体';">
            <input type="hidden" value="card" name="card" />
            <table>
                <tr>
                    <td>
                        <b>卡号：</b>
                    </td>
                    <td>
                        <input name="cardNo" type="text" id="txtcardNo" size="20" value="" class="input" /><font
                            class="red">&nbsp;*&nbsp;</font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>卡密：</b>
                    </td>
                    <td>
                        <input name="cardPwd" type="text" id="txtcardpwd" size="20" value="" class="input" /><font
                            class="red">&nbsp;*&nbsp;</font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>游戏帐号(用户名)：</b>
                    </td>
                    <td>
                        <input name="txtUserNameCard" type="text" id="txtUserNameCard" size="20" value=""
                            class="input" /><font class="red">&nbsp;*&nbsp;</font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>确认帐号(用户名)：</b>
                    </td>
                    <td>
                        <input name="txtUserNameCard2" type="text" id="txtUserNameCard2" size="20" value=""
                            class="input" /><font class="red">&nbsp;*&nbsp;</font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>请选择充值卡类型：</b>
                    </td>
                    <td style="text-align: left;">
                        <select id="sel_card" name="sel_card" onchange="GetValue(this.id);">
                            <option value="4">易优付充值卡</option>
                            <option value="2">盛大一卡通</option>
                            <option value="3">骏网一卡通</option>
                            <option value="1">腾讯QB卡</option>
                            <option value="5">完美一卡通</option>
                            <option value="7">征途游戏卡</option>
                            <option value="6">搜狐一卡通</option>
                            <option value="8">久游一卡通</option>
                            <option value="9">网易一卡通</option>
                            <option value="14">联通充值卡</option>
                            <option value="12">中国电信付费充值卡</option>
                            <option value="13，0">神州行全国充值卡</option>
                            <option value="13，24">神州行浙江卡</option>
                            <option value="13，23">神州行江苏卡</option>
                        </select><font class="red">&nbsp;*<br />
                        </font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>金额：</b>
                    </td>
                    <td style="text-align: left;">
                        <select id="sel_price" name="sel_price" style="widivh: 80px;">
                        </select>
                        元<font class="red">&nbsp;*</font>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <center>
                            <b>
                                <label style="color: red;">
                                    玩家充值自付20%手续费 ( 如：100充值卡得到实际面值为80 )</label></b></center>
                        <input class="btn_ok" type="button" onclick="SumFrms();" value="卡类提交" />
                        <input class="btn_ok" type="reset" name="button5" id="Button1" value="清空重写" onclick="ClearInfo();" />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
</body>
</html>
