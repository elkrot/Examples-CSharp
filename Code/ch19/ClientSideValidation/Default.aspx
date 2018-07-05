<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClientSideValidation._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Validation Demo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Name:
        <br />
        <asp:TextBox ID="TextBoxName" runat="server" />
        <asp:RequiredFieldValidator
            ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="You must enter your name" 
            ControlToValidate="TextBoxName" />
            
        <br />
        Date:
        <br />
        <asp:TextBox ID="TextBoxDate" runat="server" />
        <asp:RegularExpressionValidator
            ID="RegularExpressionValidator1" runat="server" 
            ErrorMessage="Enter a date in the form MM/DD/YYYY" 
            ControlToValidate="TextBoxDate" 
             ValidationExpression="(0[1-9]|1[012])/([1-9]|0[1-9]|[12][0-9]|3[01])/\d{4}"
            />
        <br />
        A Number from 1-10
        <br />
        <asp:TextBox ID="TextBoxNumber" runat="server" />
        <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ErrorMessage="Enter a value from 1 to 10" 
        ControlToValidate="TextBoxNumber" 
        MinimumValue="1" MaximumValue="10" />
        <br />
        A Prime Number < 1000:
        <br />
        <%-- Notice that you can have multiple validators per field --%>
        <asp:TextBox ID="TextBoxPrimeNumber" runat="server" />
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ErrorMessage="Number is not < 1000" 
            ControlToValidate="TextBoxPrimeNumber" 
            Operator="LessThan"
            ValueToCompare="1000" Type="Integer"
             />
        <%-- While the standard validators generate javascript,
        a custom validator has to go back to the server to work--%>
        <asp:CustomValidator ID="CustomValidator1" runat="server" 
            ErrorMessage="Number is not prime" 
            ControlToValidate="TextBoxPrimeNumber" 
            onservervalidate="OnValidatePrime" />
        <br />
        <asp:Button ID="buttonSubmit" runat="server" Text="Submit" 
            onclick="buttonSubmit_Click"  />
    </div>
    </form>
</body>
</html>
