<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SimpleApp._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <script runat="server">
            void Page_Load(object sender, EventArgs e)
            {
                lbl.Text = "You posted " + Request.Form["val1"] 
                    + " and " + Request.Form["val2"];
            }
        </script>

    <asp:Label runat="server" ID="lbl"></asp:Label>
    </form>
</body>
</html>
