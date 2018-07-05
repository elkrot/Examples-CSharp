<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SessionDemo._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Session state</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="LabelHello" runat="server" Text="Hello, "/><asp:Label ID="LabelName" runat="server" Text=""/>
        <asp:Label ID="LabelEnterYourName" runat="server" Text="Enter your name: "/><asp:TextBox ID="TextBoxName"
            runat="server"/>
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
            onclick="ButtonSubmit_Click" />
    </div>
    </form>
</body>
</html>
