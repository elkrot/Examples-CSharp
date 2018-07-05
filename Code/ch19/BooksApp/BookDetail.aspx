<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="BooksApp.BookDetailForm" %>
<%@ Register TagPrefix="how" TagName="BookEntry" Src="~/BookEntryControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Book Detail</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Header" runat="server">
    Book Detail
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <how:BookEntry runat="server" ID="bookEntry" IsEditable="false" />
</asp:Content>
