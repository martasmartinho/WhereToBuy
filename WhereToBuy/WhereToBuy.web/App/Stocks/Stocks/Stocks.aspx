<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Stocks.aspx.cs" Inherits="WhereToBuy.web.App.Stocks.Stocks.Stocks" %>

<%@ Register Src="~/UserControls/Stocks/Stocks/StocksUC.ascx" TagPrefix="uc1" TagName="StocksUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:StocksUC runat="server" id="StocksUC" />
    </article>
</asp:Content>
