<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BookEntryControl.ascx.cs"
    Inherits="BooksApp.BookEntryControl" %>
<asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="265px">
    <asp:Label ID="Label5" runat="server" Text="Book Entry" Font-Bold="True" />
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server" Text="ID:" />
            </asp:TableCell>
            <asp:TableCell>
                    <asp:TextBox ID="TextBoxID" runat="server" ReadOnly="True" Columns="5" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label6" runat="server" Text="Title:" />
            </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TextBoxTitle" runat="server" Width="200px" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" Text="Author:" />
            </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TextBoxAuthor" runat="server" Width="200px" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label3" runat="server" Text="Year:" />
            </asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="TextBoxPublishYear" runat="server" Width="75px" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Panel>
