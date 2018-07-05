<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Redirect._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="buttonRedirect" runat="server" Text="Redirect" 
            onclick="buttonRedirect_Click" />
        <br />
        <asp:RadioButtonList ID="ProcessingChoice" runat="server">
            <asp:ListItem Value="Process">Process on this page</asp:ListItem>
            <asp:ListItem Value="Transfer">Transfer Processing</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Make a choice"></asp:Label>
        <br />
        <br />
        <asp:Button ID="buttonSubmit" runat="server" Text="Submit Form" 
            onclick="buttonSubmit_Click" />
    
    </div>
    </form>
</body>
</html>
