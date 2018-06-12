<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Customer.UserList" %>

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
    <style type="text/css">
        .aspNetDisabled {color:#000;text-decoration:none;}
            .aspNetDisabled:hover {color:#000;text-decoration:none;}
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">会员列表</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <table class="filterTable" style="text-align: right;">
            <tr>
                <td><span class="autoSpan">昵称：</span></td>
                <td>
                    <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">登陆账号：</span></td>
                <td>
                    <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control autoBox"></asp:TextBox>
                </td>
                <td><span class="autoSpan">用户状态：</span></td>
                <td>
                    <asp:DropDownList ID="drpuserState" runat="server" CssClass="form-control autoBox" style="width:auto;">
                        <asp:ListItem Value="-1">--全部--</asp:ListItem>
                        <asp:ListItem Value="0">正常</asp:ListItem>
                        <asp:ListItem Value="1">禁用</asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td><span class="autoSpan">用户类型：</span></td>
                <td>
                    <asp:DropDownList ID="drpuserType" runat="server" CssClass="form-control autoBox" style="width:auto;">
                        <asp:ListItem Value="-1">--全部--</asp:ListItem>
                        <asp:ListItem Value="0">普通会员</asp:ListItem>
                        <asp:ListItem Value="1">代理</asp:ListItem>
                        <asp:ListItem Value="3">总代理</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="autoSpan">返点区间：</span></td>
                <td colspan="5">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtBeginRebate" onblur="checkNum(this)" runat="server" CssClass="form-control autoBox" style="width:80px;"></asp:TextBox></td>
                            <td>&nbsp;至&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtEndRebate" onblur="checkNum(this)" runat="server" CssClass="form-control autoBox" style="width:80px;"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
                <td colspan="6" style="text-align: center;">
                    <asp:Button ID="btnFilter" runat="server" CssClass="btnf" Text="查询" OnClick="btnFilter_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidNowParentId" runat="server" />
        <!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <a href="javascript:refBack('');">用户列表</a>&nbsp;&nbsp;>&nbsp;&nbsp;
                        <asp:Repeater ID="rptLink" runat="server">
                            <ItemTemplate>
                                <a href="javascript:refBack('<%#Eval("Uid") %>');" class="xxxxxdf"><%#Eval("Code") %></a>&nbsp;&nbsp;>&nbsp;&nbsp;
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- /.panel-heading -->
                    <div class="panel-body">
                        <div class="dataTable_wrapper">
                            <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                <thead>
                                    <tr>
                                        <th>登录名</th>
                                        <th></th>
                                        <th>昵称</th>
                                        <th style="display:none;">上级</th>
                                        <th>余额</th>
                                        <%--  <th>充值总额</th>
                                        <th>消费总额</th>--%>
                                        <th>用户类型</th>
                                        <th>返点</th>
                                        <th>登录禁用</th>
                                        <th>资金禁用</th>
                                        <th>登录次数</th>
                                        <th>最后登录时间</th>
                                        <th>注册时间</th>
                                        <th>QQ</th>
                                        <th>电话</th>
                                        <th>是否录入</th>
                                        <th>测试</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repList" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td>
                                                    <asp:LinkButton ID="lkbChildren" runat="server" Text='<%#Eval("Code") %>' Enabled='<%# IsEnabe(Eval("Id")) %>' CommandArgument='<%#Eval("Id") %>' OnClick="lkbChildren_Click"></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <img src='<%# Eval("IsLogin").ToString()=="True"?"/images/lvdian.png":"/images/huidian.png" %>' />
                                                </td>
                                                <td><%#Eval("NikeName") %></td>
                                                <td  style="display:none;"><%#Eval("ParentCode") %></td>
                                                <td><%#Eval("UserAmt") %></td>
                                                <%--  <td><%#Eval("TotalChongzhi") %></td>
                                                <td><%#Eval("TotalXiaofei") %></td>--%>
                                                <td>
                                                    <%#ToUserStateString(Eval("UserType").ToString()) %>
                                                </td>
                                                <td>
                                                    <script>
                                                        if(<%# Eval("PlayType")%>==0)
                                                            document.write((9-<%# Eval("Rebate") %>).toFixed(1));
                                                        else
                                                            document.write((14-<%# Eval("Rebate") %>).toFixed(1));
                                                    </script>

                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("IsDelete")%>' ToolTip='<%# Eval("Id")%>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("Status").ToString()=="0"?false:true%>' ToolTip='<%# Eval("Id")%>' />
                                                </td>
                                                <td><%#Eval("LoginCount") %></td>
                                                <td><%#Convert.ToDateTime(Eval("LastLoginTime")).ToString("yyyy/MM/dd").Replace("0001-01-01","")%></td>
                                                <td><%#Convert.ToDateTime(Eval("OccDate")).ToString("yyyy/MM/dd")%></td>
                                                <td>
                                                    <%if (actionModel.Recharge == true){%>
                                                    <%#Eval("Qq") %><%}%>
                                                </td>
                                                <td> <%if(actionModel.Recharge == true){%>
                                                     <%#Eval("MoTel") %><%}%>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkIn" runat="server" OnCheckedChanged="CheckBox2_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("Head").ToString()=="1"?true:false%>' ToolTip='<%# Eval("Id")%>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chktest" runat="server" OnCheckedChanged="chktest_CheckedChanged" AutoPostBack="true" Checked='<%# GetSex(Eval("Sex"))%>' ToolTip='<%# Eval("Id")%>' />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbkDel" runat="server" Text="删除" CommandArgument='<%#Eval("Id") %>' Visible="<%# actionModel.Delete %>" OnCommand="lbkDel_Command" OnClientClick="return confirm('确定要删除吗？')"></asp:LinkButton>&nbsp;&nbsp;
                                                    <%if (actionModel.Recharge == true){%> <a href="javascript:recharge(<%#Eval("id") %>,'<%#Eval("NikeName") %>','<%#Eval("Code") %>',0);">充值</a>&nbsp;&nbsp;<%}%>
                                                    <%if (actionModel.AccountChange == true)
                                                      {%> <a href="/pages/Business/AccountDetailedManager.aspx?code=<%#Eval("Code") %>">账变</a>&nbsp;&nbsp;<%}%>
                                                    <a href="<%=Utg.ServerWeb.Admin.BootStrapper.ConfigHelper.GetWebSiteDns() %>/·Views/UserGroup/UserBonus.aspx?id=<%#Eval("Id") %>" target="_blank">详情</a>&nbsp;&nbsp;                                                    <a href="/pages/Customer/UserQuotasFilter.aspx?code=<%#Eval("Code") %>">开户额</a>&nbsp;&nbsp;
                                                     <%if (actionModel.TeamMoney == true){%> <a href="javascript:groupMonery(<%#Eval("id") %>);">团队余额</a>&nbsp;&nbsp;<%}%>
                                                   <a href="/pages/SysUserBanks.aspx?code=<%#Eval("Code") %>">银行卡</a>&nbsp;&nbsp;
                                                    <a href="/pages/Customer/UpdateUserPwd.aspx?code=<%#Eval("Code") %>">修改密码</a>&nbsp;&nbsp;
                                                    <%if (actionModel.Recharge == true){%> <a href="javascript:recharge(<%#Eval("id") %>,'<%#Eval("NikeName") %>','<%#Eval("Code") %>',1);">分红</a>&nbsp;&nbsp;<%}%>
                                                  <%if (actionModel.Recharge == true){%> <a href="javascript:recharge(<%#Eval("id") %>,'<%#Eval("NikeName") %>','<%#Eval("Code") %>',2);">扣款</a>&nbsp;&nbsp;<%}%>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Visible="<%# actionModel.Delete || GetCaiwu1()%>" Text="清除取款限制" CommandArgument='<%#Eval("Id") %>' OnCommand="LinkButton1_Command" OnClientClick="return confirm('确定要清除取款限制吗？')"></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Visible="<%# actionModel.Delete  %>" Text="清除QQ信息" CommandArgument='<%#Eval("Id") %>' OnCommand="LinkButton2_Command" OnClientClick="return confirm('确定要清除QQ和电话信息吗？')"></asp:LinkButton>&nbsp;&nbsp;
                                                    <%if (actionModel.Recharge == true){%>
                                                    <a href='MoveUserParent.aspx?uid=<%#Eval("Id") %>&cd=<%#Eval("Code") %>&nk=<%#Eval("NikeName") %>'>迁移</a><%}%>
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
                <asp:Button ID="btnRef" runat="server" Style="display: none;" OnClick="btnRef_Click" />
                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Style="display: none;" />
            </div>
            <!--/列表-->

            <!-- Modal -->
            <div id="model_dialog">
                <div class="modal fade" id="NoPermissionModal">
                    <div class="modal-dialog" style="background: #fff;" id="dialog_parent">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" id="NoPermissionModalLabel">团队余额</h4>
                            </div>
                            <div class="modal-body" id="modal_body_id">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal -->

        </div>
    </form>
</html>

<!-- jQuery -->
<script src="/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<link href="/dist/css/font.css" rel="stylesheet" />
<script type="text/javascript">
    function groupMonery(uid){
       
        //获取团队余额
        $.ajax({
            url: "UserList.aspx",
            type: 'post',
            data: "ajx=group&uid="+uid+"&dt="+new Date() ,
            success: function (data) {
                if(data=="")
                    data="0";
              
                //清除
                $('#NoPermissionModal').modal();
                $("#modal_body_id").html( "<span style='font-size:14px;'> 团队余额：</span><span style='font-size:16px;font-weight:bold;color:red;'>"+data+"&nbsp;&nbsp;&nbsp;</span>");
            }
        });
    }
 
    /**充值*/
    function recharge(id,nikename,code,tp){
        var tpName="普通充值";
        if(tp==1)
            tpName="分红充值";
        else if(tp==2)
            tpName="扣款";
        parent.openModal((nikename!=""?nikename:code)+" "+tpName, "/pages/Customer/UserRecharge.aspx?id="+id+"&tp="+tp, function () {
            $("#btnRef")[0].click();
        },650,400);
    }

    function checkNum(obj) 
    {
        if($.trim(obj.value.toString())=="")
            return;

        var strP=/^\d+(\.\d{1})?$/; 
        if(!strP.test(obj.value.toString())) 
        {
            obj.value="";
            alert("请输入正整数货小数,且小数长度只能是一位!");
        }
    };

    function refBack(code){
        $("#hidNowParentId").val(code);
        $("#btnBack")[0].click();
    }

    
</script>

<script type="text/javascript">
    $(function(){
        $(".xxxxxdf").last().removeAttr("href");
        $(".xxxxxdf").last().addClass("aspNetDisabled");
    });
    </script>