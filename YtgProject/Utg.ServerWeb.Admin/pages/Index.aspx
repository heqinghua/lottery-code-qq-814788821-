<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <title>后台管理中心</title>
    <!-- Bootstrap Core CSS -->
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../bower_components/metisMenu/dist/metisMenu.min.css" rel="stylesheet">
    <!-- Timeline CSS -->
    <link href="../dist/css/timeline.css" rel="stylesheet">
    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet">
    
    <!-- Custom Fonts -->
    <link href="../bower_components/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style type="text/css">
        body {font-size:13px;line-height:normal;}
    </style>

</head>
<body>

    <div id="wrapper">

        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0">
            <div class="navbar-header" >
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only"</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="index.aspx" >乐诚网</a>
            </div>
            <!-- /.navbar-header -->
            <!---->
             <ul class="nav navbar-top-links navbar-right" style="position:absolute;" id="centerPostion">
                 <li class="dropdown" style="cursor:pointer;display:none;" id="openingLi" >
                    <a class="dropdown-toggle" data-toggle="dropdown" ><i class="glyphicon glyphicon-tag  " ></i>&nbsp;即将开奖（<span id="opening" style="color:red;">0</span>）</a>
                 </li>
                  <li class="dropdown" style="cursor:pointer;display:none;" id="newMessage">
                    <a class="dropdown-toggle" data-toggle="dropdown" ><i class="glyphicon glyphicon-comment  " ></i>&nbsp;充值成功（<span id="messageCount" style="color:red;">0</span>）</a>
                 </li>
                 <li class="dropdown" style="cursor:pointer;" id="tixianQingqiu">
                    <a class="dropdown-toggle" data-toggle="dropdown" ><i class="glyphicon glyphicon-usd " ></i>&nbsp;提现请求（<span id="userCount" style="color:red;">0</span>）</a>
                 </li>
              </ul>
            <!---->
            <ul class="nav navbar-top-links navbar-right" id="homeResult">
                 <li class="dropdown" style="cursor:pointer;">
                    <a class="dropdown-toggle" data-toggle="dropdown" id="downHome">管理首页&nbsp;<i class="glyphicon glyphicon-home"></i></a>
                 </li>
                 <li class="dropdown">
                   <a href="/pages/EditPassword.aspx" target="main"><i class="fa fa-edit fa-fw"></i>修改密码</a>
                </li>
                <li class="dropdown" style="cursor:pointer;">
                    <a class="dropdown-toggle" data-toggle="dropdown" id="downRef">刷新页面&nbsp;<i class="glyphicon glyphicon-refresh "></i></a>
                 </li>
                <li class="dropdown">
                   <a href="/pages/Index.aspx?type=logout"><i class="fa fa-sign-out fa-fw"></i>注销登录</a>
                </li>
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->

            <div class="navbar-default sidebar" role="navigation" id="navigation" style="overflow-y:auto;">
                <div class="sidebar-nav navbar-collapse">
                    <ul class="nav" id="side-menu">

                        <asp:Repeater runat="server" ID="rptList" OnItemDataBound="rptList_ItemDataBound">
                              <ItemTemplate>
                                   <li>
                                        <a href="#"><i class="fa fa-dashboard fa-fw"></i><%#Eval("Name") %><span class="fa arrow"></span></a>
                                        <ul class="nav nav-second-level">
                                             <asp:Repeater ID="rptList1" runat="server">
                                                <ItemTemplate>
                                                       <li>
                                                            <a href="<%# BuilderUrl(Eval("Url"),Eval("Id")) %>" target="main"><%#Eval("Name") %></a>
                                                        </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                        <!-- /.nav-second-level -->
                                    </li>
                              </ItemTemplate>
                        </asp:Repeater>
                        <li>
                                        <a href="#"><i class="fa fa-dashboard fa-fw"></i>优化试用<span class="fa arrow"></span></a>
                                        <ul class="nav nav-second-level">
                                             
                                                       <li>
                                                            <a href="/pages/Stat/ProfitLossListnew.aspx?menuId=31" target="main">盈亏报表</a>
                                                        </li>
                                                
                                        </ul>
                                        <!-- /.nav-second-level -->
                                    </li>
                    </ul>
                </div>
                <!-- /.sidebar-collapse -->
            </div>
            <!-- /.navbar-static-side -->
        </nav>

        <div id="page-wrapper" style="padding: 0px;">
            <iframe id="main" name="main" allowtransparency="true" width="100%" scrolling="yes" frameborder="0" border="0" noresize="noresize" src="/pages/Stat/CountData.aspx?menuId=35" framespacing="0"></iframe>
        </div>
        <!-- /#page-wrapper -->
        <!-- Modal -->
        <div id="model_dialog">
            <div class="modal fade" id="NoPermissionModal">
                <div class="modal-dialog" style="background: #fff;" id="dialog_parent">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="NoPermissionModalLabel">系统消息</h4>
                        </div>
                        <div class="modal-body" id="modal_body_id">
                            <iframe id="NoPermissioniframe" width="100%" height="100%" frameborder="0"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    </div>
    <!-- /#wrapper -->

    <!-- jQuery -->
    <script src="/bower_components/jquery/dist/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="/bower_components/metisMenu/dist/metisMenu.min.js"></script>
    <!-- Custom Theme JavaScript -->
    <script src="/dist/js/sb-admin-2.js"></script>
    <!-- Morris Charts JavaScript -->

    <%--<script src="../js/jquery-1.2.6.pack.js"></script>--%>
    <script src="../js/jquery.messager.js"></script>

    <audio id="myAudio" style="display:none;" controls="controls">
        <source src="../resource/Music/msg.mp3">
        你的浏览器不支持video标签。
    </audio>

</body>
</html>
<script type="text/javascript">
    $("#main,#navigation").css("height", ($(window).height() - 57));

    var tempmap = " <div class='modal fade' id='NoPermissionModal'>" +
            "<div class='modal-dialog' style='background: #fff;' id='dialog_parent'>" +
                "<div class='modal-content'>" +
                    "<div class='modal-header'>" +
                        "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>" +
                        "<h4 class='modal-title' id='NoPermissionModalLabel'>系统消息</h4>" +
                    "</div>" +
                    "<div class='modal-body' id='modal_body_id'>" +
                        "<iframe id='NoPermissioniframe' width='100%' height='100%' frameborder='0'></iframe>" +
                    "</div></div></div></div>";

    function openModal(title, url, dialogHide, width, height) {

        $("#NoPermissionModal").remove();
        $("#model_dialog").append(tempmap);

        $("#NoPermissionModalLabel").html(title);
        $("#NoPermissioniframe").attr("src", url);
        if (width != undefined) {
            $("#modal_body_id").css({ "width": width });
            $("#dialog_parent").css({ "width": (width) });

        }
        if (height != undefined)
            $("#modal_body_id").css({ "height": height });

        $('#NoPermissionModal').modal();
        $('#NoPermissionModal').on('hidden.bs.modal', function () {
            // 执行一些动作...
            if (dialogHide != undefined) {
                dialogHide();
            }
        })

    }

    function hideModal() {
        $('#NoPermissionModal').modal('hide')
    }
    GetWithdrawRechargePersonNumber();
    //间隔三十秒查询一次
    setInterval(GetWithdrawRechargePersonNumber, 1000);
    function GetWithdrawRechargePersonNumber()
    {
        $.post("Index.aspx", { "action": "" }, function (data)
        {
            if (data != "")
            {
               
                var txNonber = data.split(',')[0];
                if (txNonber != "0") {
                    //播放声音
                    document.getElementById("myAudio").play();

                }

                //提现申请提示
                $("#userCount").html(data.split(',')[0]);

                $("#messageCount").html(data.split(',')[1]);//充值成功的
                //即将开奖
               
                $("#opening").html(data.split(',')[2]);
                //$.messager.lays(280, 180);
                //$.messager.show('<font style="color:red; font-size:14px; font-weight:bold;">提现,充值人数</font>', '<font color=green style="font-size:12px;font-weight:bold;">' + data + '</font>');
            }
        });
    }


  
</script>
    <script type="text/javascript">

        $(function () {
            $("#downHome").click(function () {
                location.reload();
            });
            $("#downRef").click(function () {
                //刷新页面
                document.getElementById("main").contentWindow.location.reload(true);
            });
            $("#tixianQingqiu").click(function () {
                //提现请求
                $("#main").attr("src", "/pages/Business/MentionList.aspx?menuId=5");
            });
            //充值成功的
            $("#newMessage").click(function(){
                $("#main").attr("src", "/pages/Business/RechargeManager.aspx?menuId=10");
            });
            //即将开奖投注内容
            $("#openingLi").click(function () {
                $("#main").attr("src", "/pages/Business/BettList.aspx?menuId=38");
            });
            setCenterPostion();
        })
        window.onresize = setCenterPostion();
        function setCenterPostion() {
            $("#centerPostion").css("left", ($(window).width() / 2) - ($("#homeResult").width()/2));
        }
    </script>