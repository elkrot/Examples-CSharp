<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebPartsDemo._Default" %>
<%@ Register TagPrefix="part" TagName="QuoteGenerator" Src="~/RandomQuoteControl.ascx" %>
<%@ Register TagPrefix="part" TagName="TimeDisplay" Src="~/TimeDisplayControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:WebPartManager ID="WebPartManager1" runat="server" Personalization-Enabled="true"/>
    <div>
        <div style="float: left; width: 250px;" >
            <asp:WebPartZone ID="LeftZone" runat="server">
                <ZoneTemplate>
                    <part:QuoteGenerator ID="QuoteGenerator1" runat="server" title="Quotes"/>
                </ZoneTemplate>
            </asp:WebPartZone>
        </div>
        <div style="margin-left: 20px; float:left; width:250px;">
            <asp:WebPartZone ID="CenterZone" runat="server">
                <ZoneTemplate>
                    <part:TimeDisplay ID="TimeDisplay1" runat="server" title="Time"/>
                </ZoneTemplate>
            </asp:WebPartZone>
        </div>
        <div style="margin-left: 20px; float: left; width: 250px; ">
            <asp:DropDownList ID="DropDownListSupportedModes" runat="server" AutoPostBack="true"
             OnSelectedIndexChanged="OnDisplayModeChanged">
            </asp:DropDownList>
            <asp:WebPartZone ID="Rightzone" runat="server">
                <ZoneTemplate>
                    <part:QuoteGenerator ID="QuoteGenerator2" runat="server" title="Quote"/>
                </ZoneTemplate>
            </asp:WebPartZone>
            <%-- These zones help manipulate the look and content of the parts --%>
            <asp:EditorZone ID="EditorZone1" runat="server">
                <ZoneTemplate>
                    <asp:AppearanceEditorPart ID="AppearanceEditor1" runat="server" />
                </ZoneTemplate>
            </asp:EditorZone>
            <asp:CatalogZone ID="CatalogZone1" runat="server">
                <ZoneTemplate>
                    <asp:PageCatalogPart ID="PageCatalogPart1" runat="server" />
                </ZoneTemplate>
            </asp:CatalogZone>
        </div>
    </div>
    </form>
</body>
</html>
