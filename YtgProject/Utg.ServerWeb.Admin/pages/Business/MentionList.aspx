<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MentionList.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Business.MentionList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>菜单管理</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">提现请求</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>
                <td><span class="autoSpan">充值时间：</span></td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtBeginDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({startDate:'%y-%M-%d 03:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                            <td>&nbsp;至&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control autoBox" onfocus="WdatePicker({startDate:'%y-%M-%d 03:00:00',dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>

                <td><span class="autoSpan">用户名：</span></td>
                <td>
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td style="display:none;"><span class="autoSpan">提现状态：</span></td>
                <td style="display:none;">
                    <asp:DropDownList ID="drpStates" runat="server" CssClass="form-control autoBox" Style="width: auto;">
                        <asp:ListItem Text="所有状态" Value="-1" ></asp:ListItem>
                        <asp:ListItem Text="待处理" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="提现成功" Value="1"></asp:ListItem>
                        <asp:ListItem Text="提现失败" Value="2"></asp:ListItem>
                        <asp:ListItem Text="用户撤销" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">提现金额：</span></td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtBeginMonery" runat="server" CssClass="form-control autoBox" Style="width: 80px;" Text=""></asp:TextBox></td>
                            <td><span>&nbsp;至&nbsp;</span></td>
                            <td>
                                <asp:TextBox ID="txtEndMonery" runat="server" CssClass="form-control autoBox" Style="width: 80px;" Text=""></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>

        </table>
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        提现记录数据列表
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>用户名</th>
                                        <th>提现单号</th>
                                        <th>发起时间</th>
                                        <th>提现银行</th>
                                        <th>开户省市</th>
                                        <th>支行名称</th>
                                        <th>开户姓名</th>
                                        <th>银行帐号</th>
                                        <th>提现金额</th>
                                        <th>手续费</th>
                                        <th>到账金额</th>
                                        <th>状态</th>
                                        <th style="display:none;">排队人数</th>
                                        <th>操作</th>
                                    </tr>
                                    
                                </thead>
                               
                                <tbody id="body-content">
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr id="<%#Eval("Id")%>" class="odd gradeX"  style='<%# getStet(Eval("Sex"))?"background:red;color:#fff;":""%>'>
                                                <td><%#Eval("Code") %></td>
                                                <td><%#Eval("MentionCode") %></td>
                                                <td><%#Convert.ToDateTime(Eval("SendTime")).ToString("yyyy/MM/dd HH:mm:ss") %></td>
                                                <td><%#Eval("BankName") %></td>
                                                <td><%#Eval("ProvinceName") %> <%#Eval("CityName") %></td>
                                                <td><%#Eval("Branch") %></td>
                                                <td><%#Eval("BankOwner") %></td>
                                                <td><%#(Eval("BankNo")) %></td>
                                                <td><%# string.Format("{0:N0}",Eval("MentionAmt")) %></td>
                                                <td><%# string.Format("{0:N0}",Eval("Poundage")) %></td>
                                                <td>0</td>
                                                <td><%#SubSDes(Eval("IsEnableDesc"))%></td>
                                                <td style="display:none;"><%#Eval("QueuNumber")%></td>
                                                <td>
                                                    <%# GetTransferAccountAction((int)Eval("id"),(decimal)Eval("MentionAmt"),(int)Eval("Audit"),Eval("IsEnableDesc").ToString()) %>
                                                    &nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnAudit" runat="server" Text="审核" Visible='<%#HasAudit(Eval("Audit"),Eval("MentionAmt")) %>' CommandArgument='<%#Eval("Id") %>' OnCommand="btnAudit_Command"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.table-responsive -->
                        <div class="row" style="text-align: right; padding-right: 50px;">
                            <webdiyer:AspNetPager ID="pagerControl" runat="server" PageSize="20" OnPageChanged="pagerControl_PageChanged"></webdiyer:AspNetPager>
                        </div>
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
                <!-- /.col-lg-12 -->

            </div>
            <!--/列表-->

            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content" style="background: #FFF;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">转账处理&nbsp;&nbsp;<label id="msg" style="color: red;"></label></h4>
                        </div>
                        <div class="modal-body">
                            <table class="fromtable" id="Table1">
                                <tr class="odd gradeX" >
                                    <td class="titleTd">提现编号：</td>
                                    <td id="mentionCode" class="contentTd"></td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">提现银行</td>
                                    <td id="bankName" class="contentTd"></td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">银行账号</td>
                                    <td id="bankNo" class="contentTd"></td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">开户人</td>
                                    <td id="bankOwner" class="contentTd"></td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">提现金额</td>
                                    <td class="contentTd">
                                        <input id="mentionAmt" type="text" value="0" /></td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">手续费</td>
                                    <td class="contentTd">
                                        <input id="poundage" type="text" value="0" /></td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">实际到账金额</td>
                                    <td class="contentTd">
                                        <input id="realAmt" type="text" value="0" /></td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">提现状态</td>
                                    <td class="contentTd">
                                        <select id="status" style="width: 175px; height: 27px;">
                                            <option value="1" selected="selected">成功</option>
                                            <option value="2">失败</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr class="odd gradeX">
                                    <td class="titleTd">提现失败原因</td>
                                    <td class="contentTd">
                                        <textarea id="des" rows="3" cols="45"></textarea>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="btnSubmit" type="button" class="btn btn-default" onclick="TransferAccounts(this)">确定</button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

        </div>
    </form>
</body>
</html>

<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<script src="/resource/datepicker/WdatePicker.js"></script>
<link href="/resource/Pager/kkpager_orange.css" rel="stylesheet" />
<script src="/resource/Pager/kkpager.min.js"></script>
<link href="/Content/Css/subpage.css" rel="stylesheet" />
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">

    function ShowDialog(id) {

        //加载数据
        $("#msg").text("");
        $("#status option[value='1'] ").attr("selected", true)
        $("#" + id).children("td").each(function (index) {
            var val = $(this).text();
            $("#btnSubmit").val(id);
            switch (index) {
                case 1:
                    $("#mentionCode").text(val);
                    break;
                case 2:
                    //$("#mentionCode").text(val);
                    break;
                case 3:
                    $("#bankName").text(val);
                    break;
                case 4:
                    $("#bankOwner").text(val);
                    break;
                case 5:
                    $("#bankNo").text(val);
                    break;
                case 6:
                   
                    break;
                case 7:
                    $("#bankNo").html(val);
                    break;
                case 8:
                    $("#mentionAmt").val(val);
                    $("#realAmt").val(val);
                    break;
               
            }
        });
        $('#myModal').modal('show');
    }

    var isSubmit = false;
    function TransferAccounts(obj) {
        //防止多次处理
        if (isSubmit == true)
            true;

        isSubmit = true;
        var id = $(obj).val();
        var status = $("#status option:selected").val();
        var errorMsg = "";
        if (status == 2) {
            errorMsg = $.trim($("#des").val());
          
            if (errorMsg == "") {
                isSubmit = false;
                $("#msg").text("请填写提现失败原因!");
                return;
            }
        }
        if ($("#realAmt").val() < 0)
        {
            $("#msg").text("请填写实际到账金额!");
            return;
        }

        if (confirm("确认处理?")) {
            $.post("MentionList.aspx", { "action": "ajax", "id": id, "status": status, "errorMsg": errorMsg, "realAmt": $("#realAmt").val(), "poundage": $("#poundage").val()}, function (data) {
                isSubmit = false;
                if (data == "1") {
                    alert("提现编号【" + $("#mentionCode").text() + "】处理成功");
                    window.location = "?menuId=<%=Request.Params["menuId"]%>&xd=" + new Date();
                }
                else if (data == "2") {
                    $("#msg").text("很抱歉，提现【" + $("#mentionCode").text() + "】已失败");
                }
            });
        }
    }

</script>
