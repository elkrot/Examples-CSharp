<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" 
Inherits="BrowserCapsAndTracing._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Browser Capabilities"></asp:Label>
        <br />
    </div>
    <asp:Table ID="Table1" runat="server" BackColor="#6666FF" BorderColor="#3333CC" 
        BorderWidth="1px">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Capability</asp:TableHeaderCell>
            <asp:TableHeaderCell>Enabled</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    </form>
</body>
</html>
