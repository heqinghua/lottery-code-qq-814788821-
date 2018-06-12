<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MentionList.aspx.cs" Inherits="Ytg.ServerWeb.Views.Users.MentionList" MasterPageFile="/Views/ChongZhi/ChongZhi.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="usersHead" runat="server">
    <script src="/Content/Scripts/datepicker/WdatePicker.js"></script>
     <link href="/Content/Scripts/Pager/kkpager_orange.css" rel="stylesheet" />
    <script src="/Content/Scripts/Pager/kkpager.min.js"></script>
     <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            $("#tixian").addClass("title_active");

            $("#lbtnSearch").click(function () {
                loaddata();//加载数据
            });
          
            loaddata();//加载数据
        });

        var pageIndex = 1;
        function loaddata() {
            Ytg.common.loading();
            var st = $("#txtstart").val();
            var ed = $("#txtend").val();
            $.ajax({
                url: "/Page/Bank/Bank.aspx",
                type: 'post',
                data: "action=selectmention&sDate=" + st + "&eDate=" + ed + "&pageIndex=" + pageIndex + "&tp=" + $("#ddlStatus").find("option:selected").val(),
                success: function (data) {
                    Ytg.common.cloading();
                    var jsonData = JSON.parse(data);
                    //清除
                    $(".ltbody").children().remove();
                    if (jsonData.Code == 0 && jsonData.Data.length > 0) {

                        inintpager(pageIndex, jsonData.Total, function (n) {
                            pageIndex = n;
                            loaddata();
                        });

                        for (var c = 0; c < jsonData.Data.length; c++) {
                            var item = jsonData.Data[c];
                            var desc = item.IsEnableDesc == "提现失败" ? "<span style='color:red;'>提现失败</span>" : item.IsEnableDesc;
                            var htm = "<tr><td>" + item.MentionCode + "</td><td>" + item.SendTime + "</td><td>" + item.BankName + "</td><td>" + item.BankNo + "</td><td>" + item.MentionAmt + "</td><td>" + item.Poundage + "</td><td>" + item.RealAmt + "</td><td style='display:none;'>" + item.QueuNumber + "</td><td>" + desc + "</td></tr>"
                            $(".ltbody").append(htm);
                        }
                    } else {
                        $(".ltbody").Empty(9);
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="ct" ContentPlaceHolderID="ContentUsers" runat="server">
    <div class="control">
        <!--工具栏-->
        <div class="toolbar-wrap">
              <table  border="0" cellspacing="0" cellpadding="0" >
                <tbody>
                    <tr>
                        <td align="center"> 提现时间：<input id="txtstart" type="text" class="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" value="<%=Ytg.Comm.Utils.ToGetNowBeginDateStr() %>" />&nbsp;至&nbsp;
                            <input id="txtend" type="text" class="input date" value="<%=Ytg.Comm.Utils.GetNowEndDateStr() %>" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
                        </td>
                        <td align="center">提现状态：
                        <select id="ddlStatus">
                            <option selected="selected" value="-1">所有状态</option>
                            <option value="0">排队中</option>
                            <option value="1">提现成功</option>
                            <option value="2">提现失败</option>
                            <option value="3">用户撤销</option>
                        </select></td>
                        <td><input name="" id="lbtnSearch" type="button" value="查询" class="formCheck"></td>
                    </tr>
                    
                </tbody>
            </table>

           
            <!--/工具栏-->
            <!--列表-->
            <div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="grayTable">
                    <thead>
                        <tr>
                            <th >提现编号</th>
                            <th align="left" >申请时间</th>
                            <th align="left">提现银行</th>
                            <th align="left">银行账号</th>
                            <th align="left" >提现金额</th>
                            <th align="left">手续费</th>
                            <th align="left">到账金额</th>
                            <th align="left" style="display:none;">排队人数</th>
                            <th align="left">状态</th>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
                        <tr>
                            <td colspan="9">暂无记录!</td>
                        </tr>
                    </tbody>
                </table>
                  <!--/列表-->
                <div id="kkpager" style=""></div>
            </div>
            <!--/列表-->
        </div>
    </div>
</asp:Content>
