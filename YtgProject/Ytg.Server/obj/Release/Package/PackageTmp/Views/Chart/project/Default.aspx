<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Style.css" rel="Stylesheet" />
    
    <script src="../js/jquery-1.4.2.min.js"></script>
    <script src="js/jquery.form.js" type="text/javascript"></script>
    <script src="js/fileload.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            createHtml($("#str"));
        })
    </script>
</head>
<body>
    <div id="str">
        
    </div>
</body>
</html>
