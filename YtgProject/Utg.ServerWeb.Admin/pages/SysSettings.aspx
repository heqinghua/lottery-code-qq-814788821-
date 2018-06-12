<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysSettings.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.SysSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统参数设置</title>
    <link rel="stylesheet" href="/resource/bsvd/vendor/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="/resource/bsvd/dist/css/bootstrapValidator.css" />
    <script type="text/javascript" src="/resource/bsvd/vendor/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/vendor/bootstrap/js/bootstrap.min.js"></script>
    <link href="/dist/css/font.css" rel="stylesheet" />

</head>
<body>
    <div style="margin-left: 20px; width: 98%;">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header" style="margin: 0px; margin-bottom: 9px; margin-top: 20px;">系统参数设置</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <form id="defaultForm" runat="server" method="post" data-bv-message="This value is not valid"
            data-bv-feedbackicons-valid="glyphicon glyphicon-ok"
            data-bv-feedbackicons-invalid="glyphicon glyphicon-remove"
            data-bv-feedbackicons-validating="glyphicon glyphicon-refresh">

            <table class="fromtable" id="Table1" style="margin-left: -10px;">
                <tr class="odd gradeX">
                    <td class="titleTd">站点名称：</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="请填写站点名称" Style="width: 400px;"></asp:TextBox></td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">跳转地址：</td>
                    <td class="contentTd">
                        <table>
                            <tr>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control autoBox" Text="http://" required data-bv-notempty-message="请填写跳转URL" Style="width: 400px;"></asp:TextBox></td>
                                <td style="border: none;"><span style="color: red; padding-left: 5px;">用户名不对或锁定时跳转</span></td>
                            </tr>
                        </table>

                    </td>
                </tr>
                 <tr class="odd gradeX">
                    <td class="titleTd">客服连接地址：</td>
                    <td class="contentTd">
                        <table>
                            <tr>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtKfAddress" runat="server" CssClass="form-control autoBox" Text="http://" required data-bv-notempty-message="请填写跳转URL" Style="width: 400px;"></asp:TextBox></td>
                                <td style="border: none;"><span style="color: red; padding-left: 5px;">在线客服地址</span></td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">网站开关：</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="drpIsShowDialog" runat="server" CssClass="form-control autoBox">
                            <asp:ListItem Value="0" Text="开启"></asp:ListItem>
                            <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">网站关闭公告：</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtContent" runat="server" CssClass="form-control autoBox" Text="" Rows="10" TextMode="MultiLine" Style="width: 600px;"></asp:TextBox></td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">中奖排行开关：</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="drpShangBanOpen" runat="server" CssClass="form-control autoBox">
                            <asp:ListItem Value="0" Text="开启"></asp:ListItem>
                            <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">会员上榜最低奖金：</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtMinMonery" runat="server" CssClass="form-control autoBox" Text="100"></asp:TextBox></td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">虚拟会员上榜内容：</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtXuNiInContennt" runat="server" CssClass="form-control autoBox" Text="" Rows="20" TextMode="MultiLine" Style="width: 600px;"></asp:TextBox>
                        <p class="help-block">使用|分割虚拟账号和中奖金额，使用,分割账号 如:会员账号|中奖金额,会员账号|中奖金额</p>
                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">充值额度限制：</td>
                    <td class="contentTd">
                        <table>
                            <tr>
                                <td style="border: none;">&nbsp;&nbsp;&nbsp;&nbsp;最低：</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtInMinMonery" runat="server" CssClass="form-control autoBox" Text="10"></asp:TextBox></td>
                                <td style="border: none;">&nbsp;&nbsp;&nbsp;&nbsp;最高：</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtInMaxMonery" runat="server" CssClass="form-control autoBox" Text="50000"></asp:TextBox></td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">提现额度限制：</td>
                    <td class="contentTd">
                        <table>
                            <tr>
                                <td style="border: none;">&nbsp;&nbsp;&nbsp;&nbsp;最低：</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtOutMinMonery" runat="server" CssClass="form-control autoBox" Text="100"></asp:TextBox></td>
                                <td style="border: none;">&nbsp;&nbsp;&nbsp;&nbsp;最高：</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtOutMaxMonery" runat="server" CssClass="form-control autoBox" Text="50000"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">提现流水限制：</td>
                    <td class="contentTd">
                        <table>
                            <tr>
                                <td style="border: none;">&nbsp;&nbsp;&nbsp;&nbsp;充值提款最低消费比：</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtRechangeMinBili" runat="server" CssClass="form-control autoBox" Text="5%" Width="50"></asp:TextBox></td>
                                <td style="border: none;">%</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">提现审核：</td>
                    <td class="contentTd">
                        <table>
                            <tr>
                                <td style="border: none;">&nbsp;&nbsp;&nbsp;&nbsp;提现金额大于等于：</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtShMonery" runat="server" CssClass="form-control autoBox" Text="5000" Width="50"></asp:TextBox></td>
                                <td style="border: none;">需审核</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">新用户注册赠送活动：</td>
                    <td class="contentTd">
                        <table>
                            <tr>
                                <td style="border: none;">&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkopenZengSong" Text="是否启用活动" runat="server" />
                                </td>
                                <td style="border: none;">&nbsp;&nbsp;赠送金额：</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtNewUserZenSong" runat="server" CssClass="form-control autoBox" Text="18" Width="50"></asp:TextBox></td>
                                <td style="border: none;">&nbsp;&nbsp;提款投注金额倍数：</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtNewUserBeiShu" runat="server" CssClass="form-control autoBox" Text="18" Width="50"></asp:TextBox>
                                </td>
                                <td style="border: none;">倍</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">六合彩当月期数：</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtLhcQs" runat="server" CssClass="form-control autoBox" Text="" Rows="5" TextMode="MultiLine" Style="width: 600px;"></asp:TextBox>
                        <p class="help-block">使用,分割开奖日，格式为月-日;如:3-1,3-3,3-5</p>
                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">当前期：</td>
                    <td class="contentTd">
                        <table>
                            <tr>
                                <td style="border: none;">&nbsp;&nbsp;&nbsp;&nbsp;当前期 :</td>
                                <td style="border: none;">
                                    <asp:TextBox ID="txtStartIssue" runat="server" CssClass="form-control autoBox"></asp:TextBox></td>
                                <td style="border: none;">&nbsp;&nbsp;<asp:CheckBox ID="chkCleal" Text="清除现有期数" runat="server" /></td>
                                <td style="border: none;">&nbsp;&nbsp;<asp:Button ID="btnSaveLhc" runat="server" class="submitbtn" Text="生成六合彩期数" Style="width: auto;" OnClick="btnSaveLhc_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr class="odd gradeX">
                    <td class="titleTd">摩宝支付：</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="drpMb" runat="server" CssClass="form-control autoBox">
                            <asp:ListItem Value="0" Text="开启"></asp:ListItem>
                            <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">智付支付：</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="drpzhifu" runat="server" CssClass="form-control autoBox">
                            <asp:ListItem Value="0" Text="开启"></asp:ListItem>
                            <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">My18：</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="drpMy18" runat="server" CssClass="form-control autoBox">
                            <asp:ListItem Value="0" Text="开启"></asp:ListItem>
                            <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">提现是否开启：</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="drptx" runat="server" CssClass="form-control autoBox">
                            <asp:ListItem Value="0" Text="开启"></asp:ListItem>
                            <asp:ListItem Value="1" Text="关闭"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                  <tr class="odd gradeX">
                    <td class="titleTd">关闭提现功能原因：</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtTxErrorMsg" runat="server" CssClass="form-control autoBox" Text="" Rows="5" TextMode="MultiLine" Style="width: 600px;"></asp:TextBox>
                        <p class="help-block">关闭提现功能后，将提示该内容</p>
                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">支付宝充值二维码：</td>
                    <td class="contentTd">
                        <asp:FileUpload  ID="flzfb" runat="server"/>
                        <asp:Label ID="lbzfb" runat="server"  Text="支付宝充值二维码地址"></asp:Label>
                    </td>
                </tr>
                <tr class="odd gradeX">
                    <td class="titleTd">微信充值二维码：</td>
                    <td class="contentTd">
                        <asp:FileUpload  ID="flwx" runat="server"/>
                        <asp:Label ID="lbwx" runat="server"  Text="微信充值二维码地址"></asp:Label>
                    </td>
                </tr>
            </table>
            <div style="height: 10px;"></div>
            <div style="text-align: center;">
                <asp:Button ID="btnSave" runat="server" class="submitbtn" Text="保存" OnClick="btnSave_Click" /></div>
        </form>
    </div>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    //var isunque = false;
    //$(document).ready(function () {
    //    $('#defaultForm').bootstrapValidator();
    //});
    //$("<%=drptx.ClientID%>").change();
</script>

