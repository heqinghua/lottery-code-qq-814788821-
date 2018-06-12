<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadClient.aspx.cs" Inherits="Ytg.ServerWeb.DownloadClient" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户端下载</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <script src="/Content/Scripts/jquery-1.7.min.js"></script>
   <link href="/Content/Scripts/Dialog/dialogUI.css" rel="stylesheet" type="text/css" />
    <script src="/Content/Scripts/Dialog/jquery.dialogUI.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Dialog/jquery.dragdrop.js" type="text/javascript"></script>
    <style type="text/css">
        body {font-size:13px;}
       .pc-ul-l li a, .pc-ul-r li a {color: #fff;background-color:#cc0228;margin: 5px;padding: 4px;text-decoration: none;}
       .pc-ul-l li {padding: 6px;}
       li {list-style: none;}
       a:hover {text-decoration: none;}
       a:visited {text-decoration: none;}
    </style>
</head>
<body>
    <div i="content" class="ui-dialog-content"  style="width: 580px;margin-top:30px;">
       <div style="font-weight: bold; background: #e9dfc9; height: 30px; line-height: 30px; padding-left: 10px;">电脑客户端下载</div>
        <div style="height: 205px;">
            <ul class="pc-ul-l" >
                <li class="pc-li-one">第1步：若您使用Windows 7或以下版本，请先下载Microsoft .NET Framework 4</li>
                <li class="pc-li-two">Windows 7运行客户端 请先安装Microsoft .NET Framework 4环境</li>
                <li class="pc-li-three"><a href="https://download.microsoft.com/download/9/5/A/95A9616B-7A37-4AF6-BC36-D6EA96C8DAAE/dotNetFx40_Full_x86_x64.exe" target="_blank">下载Microsoft .NET Framework 4</a></li>
            </ul>
            <ul class="pc-ul-l" >
                <li class="pc-li-one">第2步：下载客户端(点击以下链接下载客户端)</li>
                <li class="pc-li-three"><a href="/apk/boyue.rar" target="_blank" title="客户端1">下载地址1</a><a href="/apk/boyue.rar" target="_blank" title="客户端2">下载地址2</a></li>
            </ul>
        </div>
        <div style="font-weight: bold; background: #e9dfc9; height: 30px; line-height: 30px; padding-left: 10px;">Android版客户端下载</div>
        <ul class="pc-ul-l" >
                <li class="pc-li-two">点击图片或扫描二维码下载客户端</li>
                <li class="pc-li-three"><a href="http://play.wushuanglinhh.cn/apk/by.apk" target="_blank" title="客户端1"><img src="/Css/apk.png" style="width:100px;"/></a></li>
        </ul>
        <div style="font-weight: bold; background: #e9dfc9; height: 30px; line-height: 30px; padding-left: 10px;">IOS版客户端下载</div>
        <ul class="pc-ul-l" >
                <li class="pc-li-two">IOS客户端应用商店尚未审核通过，请扫码访问wap版本(微信扫码后，点击右上角-在浏览器中打开为最佳体验)</li>
                <li class="pc-li-three"><a href="#" target="_blank" title="wap"><img src="/Css/wap.png" style="width:100px;"/></a></li>
        </ul>
    </div>
</body>
</html>
