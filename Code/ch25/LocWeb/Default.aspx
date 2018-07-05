<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" uiCulture="Auto" Culture="Auto" meta:resourcekey="PageResource1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="LabelName" runat="server" Text="The Flag:" 
            meta:resourcekey="LabelNameResource1"></asp:Label>
        <asp:Image ID="ImageFlag" runat="server" ImageUrl="~/images/us.png" 
            meta:resourcekey="ImageFlagResource1"></asp:Image>
            <br />
        <asp:TextBox ID="TextBox1" runat="server" 
            Text="<%$ Resources:GlobalResources, Message %>" 
            meta:resourceKey="TextBoxResource1" />
    </div>
    </form>
</body>
</html>
