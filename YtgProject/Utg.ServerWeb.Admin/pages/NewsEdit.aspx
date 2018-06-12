<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsEdit.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.NewsEdit" ValidateRequest="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑新闻</title>
    <link rel="stylesheet" href="/resource/bsvd/vendor/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="/resource/bsvd/dist/css/bootstrapValidator.css" />
    <script type="text/javascript" src="/resource/bsvd/vendor/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/resource/bsvd/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="/js/ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/js/ueditor/ueditor.all.min.js"> </script>
    <!--建议手动加在语言，避免在ie下有时因为加载语言失败导致编辑器加载失败-->
    <!--这里加载的语言文件会覆盖你在配置项目里添加的语言类型，比如你在配置项目里配置的是英文，这里加载的中文，那最后就是中文-->
    <script type="text/javascript" charset="utf-8" src="/js/ueditor/lang/zh-cn/zh-cn.js"></script>

</head>
<body>
    <div style="margin-left: 20px;width:98%;">
        <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header" style="margin:0px;margin-bottom:9px;margin-top:20px;">编辑新闻</h2>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <form id="defaultForm" runat="server" method="post" data-bv-message="This value is not valid"
            data-bv-feedbackicons-valid="glyphicon glyphicon-ok"
            data-bv-feedbackicons-invalid="glyphicon glyphicon-remove"
            data-bv-feedbackicons-validating="glyphicon glyphicon-refresh">
            <table class="fromtable" style="margin-left:-5px;">
                 <tr>
                    <td class="titleTd">是否弹窗：</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="drpIsShowDialog" runat="server" CssClass="form-control autoBox">
                            <asp:ListItem Value="0" Text="否"></asp:ListItem>
                            <asp:ListItem Value="1" Text="是"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="titleTd">新闻标题：</td>
                    <td class="contentTd">
                        <p class="help-block">
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control autoBox" required data-bv-notempty-message="请填写新闻标题" style="width:400px;"></asp:TextBox>
                        </p>
                    </td>
                </tr>
               
                <tr>
                    <td class="titleTd">新闻内容：</td>
                    <td class="contentTd">
                        <script id="editor" type="text/plain" style="width: 100%; height: 300px;"><%=txtContent.Text %>
                        </script>
                        <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" Text="" Rows="10" TextMode="MultiLine" Style="display: none;"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div style="height:10px;"></div>
            <div style="text-align:center;">
                <asp:Button ID="btnSave" runat="server" class="submitbtn" Text="保存" OnClick="btnSave_Click" OnClientClick="return onSubmit();" />
                <input  id="btnPrew" class="submitbtn" value="预览" type="button" onclick="prew();"/>
            </div>

            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content" style="background: #FFF;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">公告预览&nbsp;&nbsp;<label id="msg" style="color: red;"></label></h4>
                        </div>
                        <div class="modal-body" id="newContent">
                           
                        <div class="modal-footer">
                            <button id="btnSubmit" type="button" class="btn btn-default"  data-dismiss="modal">确定</button>
                            <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </form>
    </div>
</body>
</html>
<link href="/dist/css/font.css" rel="stylesheet" />
<style type="text/css">
    .modal-dialog {height:310px;}
    .modal-content {height:310px;}
</style>
<script type="text/javascript">
    var isunque = false;
    $(document).ready(function () {
        //实例化编辑器
        //建议使用工厂方法getEditor创建和引用编辑器实例，如果在某个闭包下引用该编辑器，直接调用UE.getEditor('editor')就能拿到相关的实例
        var ue = UE.getEditor('editor');
        //$('#defaultForm').bootstrapValidator();
        //设置内容
    });
    function onSubmit() {
        var content = UE.getEditor('editor').getContent();
        UE.getEditor('editor').execCommand('insertHtml', "");
        $("#ueditor_textarea_editorValue").val("");
        $("#txtContent").val(encodeURI(content));
        return true;
    }

    function prew() {
        var content = UE.getEditor('editor').getContent();
        $("#newContent").html(content);
        $('#myModal').modal('show');
    }
</script>

