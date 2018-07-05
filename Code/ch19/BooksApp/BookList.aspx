<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="BookList.aspx.cs" Inherits="BooksApp.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>List of Books</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" runat="server">
    List of Books
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%-- disable column generation since we're specifying them below --%>
    <asp:GridView ID="BookListGrid" runat="server" AutoGenerateColumns="False">
        <Columns>
            <%-- Make the title into a link that goes to a detail page --%>
            <asp:HyperLinkField HeaderText="Title" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="BookDetail.aspx?id={0}"
                DataTextField="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
        </Columns>
    </asp:GridView>
</asp:Content>
