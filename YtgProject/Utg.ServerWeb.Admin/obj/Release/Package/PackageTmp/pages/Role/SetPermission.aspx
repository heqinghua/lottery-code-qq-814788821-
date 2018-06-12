<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPermission.aspx.cs" Inherits="Utg.ServerWeb.Admin.pages.Role.SetPermission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/bower_components/bootstrap/dist/css/bootstrap.css" rel="stylesheet" /> 
    <link href="/dist/css/font.css" rel="stylesheet" />
</head>
<body>
    <div class="row">
            <div class="col-lg-12">
                <h2 class="page-header">权限列表</h2>
            </div>
            <!-- /.col-lg-12 -->
     </div>
   <form id="Form1" runat="server">
        <div class="dataTable_wrapper">
            <asp:TreeView ExpandDepth="0" ShowCheckBoxes="All" ID="treeT" runat="server" ></asp:TreeView>
            <br />
            <div class="modal-footer" style="text-align:center;">
                <asp:HiddenField ID="txtRoleId" runat="server" />
                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="保存"  OnClick="btnSave_Click"/>
            </div>
        </div>
    </form>
</body>
</html>

<script>
    
    function public_GetParentByTagName(element, tagName) {
        var parent = element.parentNode;
        var upperTagName = tagName.toUpperCase();
        //如果这个元素还不是想要的tag就继续上溯 
        while (parent && (parent.tagName.toUpperCase() != upperTagName)) {
            parent = parent.parentNode ? parent.parentNode : parent.parentElement;
        }
        return parent;
    }

    //设置节点的父节点Cheched——该节点可访问，则他的父节点也必能访问 
    function setParentChecked(objNode) {
        var objParentDiv = public_GetParentByTagName(objNode, "div");
        if (objParentDiv == null || objParentDiv == "undefined") {
            return;
        }
        var objID = objParentDiv.getAttribute("ID");
        objID = objID.substring(0, objID.indexOf("Nodes"));
        objID = objID + "CheckBox";
        var objParentCheckBox = document.getElementById(objID);
        if (objParentCheckBox == null || objParentCheckBox == "undefined") {
            return;
        }
        if (objParentCheckBox.tagName != "INPUT" && objParentCheckBox.type == "checkbox")
            return;
        objParentCheckBox.checked = true;
        setParentChecked(objParentCheckBox);
    }

    //设置节点的子节点uncheched——该节点不可访问，则他的子节点也不能访问 
    function setChildUnChecked(divID) {
        var objchild = divID.children;
        var count = objchild.length;
        for (var i = 0; i < objchild.length; i++) {
            var tempObj = objchild[i];
            if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
                tempObj.checked = false;
            }
            setChildUnChecked(tempObj);
        }
    }

    //设置节点的子节点cheched——该节点可以访问，则他的子节点也都能访问 
    function setChildChecked(divID) {
        var objchild = divID.children;
        var count = objchild.length;
        for (var i = 0; i < objchild.length; i++) {
            var tempObj = objchild[i];
            if (tempObj.tagName == "INPUT" && tempObj.type == "checkbox") {
                tempObj.checked = true;
            }
            setChildChecked(tempObj);
        }
    }

    //触发事件 
    function CheckEvent() {
        var objNode = event.srcElement;
        if (objNode.tagName != "INPUT" || objNode.type != "checkbox")
            return;
        if (objNode.checked == true) {
            setParentChecked(objNode);
            var objID = objNode.getAttribute("ID");
            var objID = objID.substring(0, objID.indexOf("CheckBox"));
            var objParentDiv = document.getElementById(objID + "Nodes");
            if (objParentDiv == null || objParentDiv == "undefined") {
                return;
            }
            setChildChecked(objParentDiv);
        }
        else {
            var objID = objNode.getAttribute("ID");
            var objID = objID.substring(0, objID.indexOf("CheckBox"));
            var objParentDiv = document.getElementById(objID + "Nodes");
            if (objParentDiv == null || objParentDiv == "undefined") {
                return;
            }
            setChildUnChecked(objParentDiv);
        }
    }
</script>
