<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmBindCardNumLast.aspx.cs" Inherits="Ytg.ServerWeb.Mobile.user.ConfirmBindCardNumLast" EnableEventValidation="false" EnableViewStateMac="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Content/Css/style.css" rel="stylesheet" />
     <link href="../css/dialogUI.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
 <link href="../css/style1.css" rel="stylesheet" />
    <link href="../css/subpage1.css" rel="stylesheet" />
    <link href="/Mobile/css/bootstrap1.css" rel="stylesheet" />
    <link href="/Mobile/css/list.css" rel="stylesheet" />
    <style type="text/css">
        #confirm span {
            font-size: 14px;
            color: red;
        }

        

        .lsrd {
            text-align:right;
            font-weight: bold;
        }

        .vdtable tr td {
            height: 28px;
            line-height: 28px;
        }
        .formTable th, .formTable td {
            border-left: 0px solid #d8d8d8;
            border-top: 0px solid #d8d8d8;
            line-height: 20px;
        }
   
        .bindDiv {
            padding:5px;
            text-align: left;
            margin-left:0px;
        }
            .bindDiv p {
                font-size: 14px;
                padding-left: 20px;
            }
    </style>
     <link rel="stylesheet" href="/wap/statics/css/global.css?ver=4.4" type="text/css" />
    <script src="/wap/statics/js/iscro-lot.js?ver=4.4"></script>
</head>
<body>
     <div class="header">
        <div class="headerTop">
            <div class="ui-toolbar-left">
                <button id="reveal-left" type="button">reveal</button>
            </div>
            <h1 class="ui-toolbar-title">绑定银行卡</h1>
        </div>
    </div>
    <div class="ctParent">
        <form id="form1" runat="server">
            <div class="control">
                <div class="bindDiv">
                   
                    <h2 class="subctTitle" style="font-size:14px;font-weight:bold;">使用提示：</h2>
                    <p>请仔细确认以下信息后, 点击 <span style="color: red;">“立即绑定”</span> 按钮。</p>
                    <div style="height: 10px;"></div>
                </div>
                <div class="tab-content" style="border-top: 1px solid #e1e1e1;">
                    <div id="confirm" style="margin: auto; width: 400px;">
                        <table style="width: 300px; margin: auto;" >
                            <tr>
                                <td class="lsrd">开户银行 :</td>
                                <td><asp:Label CssClass="Validform_checktip" ID="bankspan" runat="server"></asp:Label></td>
                            </tr>
                           <tr>
                               <td style="height:10px;"></td>
                           </tr>
                            <tr>
                                <td class="lsrd">开户银行省份 :</td>
                                <td><asp:Label CssClass="Validform_checktip" ID="opspan" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                               <td style="height:10px;"></td>
                           </tr>
                            <tr>
                                <td class="lsrd">开户银行市 :</td>
                                <td>
                                    <asp:Label CssClass="Validform_checktip" ID="opscity" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                               <td style="height:10px;"></td>
                           </tr>
                            <tr id="confirmzhih" runat="server">
                                <td class="lsrd">开户人支行 :</td>
                                <td>
                                    <asp:Label CssClass="Validform_checktip" ID="opszhihang" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                               <td style="height:10px;"></td>
                           </tr>
                            <tr>
                                <td class="lsrd">开户人姓名 :</td>
                                <td>
                                    <asp:Label CssClass="Validform_checktip" ID="opsname" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                               <td style="height:10px;"></td>
                           </tr>
                            <tr>
                                <td class="lsrd">银行账号 :</td>
                                <td>
                                    <asp:Label CssClass="Validform_checktip" ID="opsNo" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                               <td style="height:10px;"></td>
                           </tr>
                            <tr>
                                <td class="lsrd">资金密码 :</td>
                                <td>
                                    <asp:TextBox ID="txtZjPwd" TextMode="Password" runat="server" CssClass="password normal" Style="margin-left: 8px;    border: 1px solid #ddd;" onpaste="return false"></asp:TextBox>
                                    <span class="Validform_checktip">*</span></td>
                            </tr>
                        </table>
                        <div style="text-align:center;margin:auto;margin-top: 30px; ">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="formWord" Text="立即绑定" Style="" OnClick="btnSubmit_Click" />
                        <input type="button" value="返回" id="bankback" class="formReset" style="margin-left: 10px;" onclick="window.location = '/Mobile/user/ConfirmBindCardNum.aspx?<%=BackParam%>    '" />
                        </div>
                    </div>

                </div>
            </div>
        </form>
    </div>
</body>
</html>
<script type="text/javascript">
    $(function () {
       
        $("#btnSubmit").click(function () {
            
            var zjmi = $("#txtZjPwd").val();
            if ($.trim(zjmi) == "") {
                $("#txtZjPwd").focus();
                $.alert("资金密码不能为空!");
                return false;
            }
            return true;
        });
    });
</script>
