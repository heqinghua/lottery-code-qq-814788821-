<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YongJin.aspx.cs" Inherits="Ytg.ServerWeb.Views.Activity.YongJin.YongJin" MasterPageFile="~/lotterySite.Master"%>
<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
    <script src="/Content/Scripts/layout.js" type="text/javascript"></script>
    <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" />
    <link href="/Content/Css/subpage.css" rel="stylesheet" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js"></script>
    <script src="/Content/Scripts/lhgdialog/lhgdialog.js?skin=chrome"></script>
    <style type="text/css">
        .ltable td {font-size:12px;}
        
    </style>
    <script type="text/javascript">
        var isautoRefbanner = <%=isautoRefbanner?"true":"false"%>;
        $(function () {
            $("#refff").html(Ytg.tools.moneyFormat('<%=UserAmt%>'));//old amt
            $("#actives").addClass("cur");
            $("#bottomState").remove();
        })
    </script>
    <style type="text/css">
        .yongbg {background:#fff;}
        .yongbg .content {margin-top:500px;}
        .yongbg .content ul {margin:0px;padding:0px;width:900px;margin:auto;}
        .yongbg .content ul li {list-style:none;float:left;width:50%;}
        .yongbg .content ul li p {color:#8e3f42;font-size:14px;text-align:left;line-height:28px;}
        .yongbg .content ul li p span {font-size:18px;font-weight:bold;}
        .h1style {font-weight:normal;line-height:none;font-size:20px;text-align:left;color:#781c1f;margin-top:10px;}
        .btn{height:81px;width:286px;font-size:16px; background:url('Content/Image/146282111847.gif') no-repeat;border:none;cursor:pointer;margin-right:10px;}
        .btn {color: #fff;border: none;margin:auto;margin-top:250px;}
        .closeButton {background: url(Content/Image/146281533676.png) left top;width: 84px;height: 34px;border: 0;cursor: pointer;display: inline-block;color: #fff;padding: 0 12px;  margin: 0 5px;font-family: "Microsoft Yahei";font-weight: bold;font-size:14px;}
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <style type="text/css">
        body {color:#fff;}
    </style>
    <form runat="server">
    <div id="content" class="yongbg">
        <div style="background:url('Content/Image/146908478436.jpg') no-repeat;background-position:center;background-position-x:center;height:567px;width:100%;">
        </div>
        <div style="background:url('Content/Image/146908478304.jpg') no-repeat;background-position:center;background-position-x:center;height:1152px;width:100%;">
        </div>
        <div style="background:url('Content/Image/146908478545.jpg') no-repeat;background-position:center;background-position-x:center;height:401px;width:100%;">
             <asp:Button ID="btnME" runat="server" OnClick="btnME_Click" CssClass="btn" />
        </div>
        
    </div>
   </form>
    <script type="text/javascript">
        $(function () {
            var ct = '<%=Content%>'
            if (ct != "") {
                ct = "<p style='color:#000;'><span style='color:#000;font-size:12px;ling-height:30px;'> " + ct + "</span></p><p style='text-align:center;'><input type='button' value='确定' class='closeButton' style='margin-top:15px;' onclick='dialog_close();'/></p>";
                $.dialog({
                    id: 'open_content',
                    fixed: true,
                    lock: false,
                    max: false,
                    min: false,
                    width: 400,
                    height: 130,
                    title: "提示",
                    content: ct
                });
            }
        });

        function dialog_close() {
            $.dialog({ id: 'open_content' }).close();
            Ytg.common.user.refreshBalance();
        }
    </script>
</asp:Content>
