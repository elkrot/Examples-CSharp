<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="AuthDemo.LoginForm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login Form</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Username:
        <asp:TextBox ID="TextBoxUsername" runat="server" />
        <br />
        Password:<asp:TextBox ID="TextBoxPassword" runat="server" />
        <br />
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" 
            onclick="ButtonSubmit_Click" />
        <br />
        <asp:Label ID="LabelStatus" runat="server" Text="Enter your login info" />
    </div>
    </form>
</body>
</html>
