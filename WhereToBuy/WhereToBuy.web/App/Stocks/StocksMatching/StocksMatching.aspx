<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StocksMatching.aspx.cs" Inherits="WhereToBuy.web.App.Stocks.StocksMatching.StocksMatching" %>

<%@ Register Src="~/UserControls/Stocks/StocksMatching/StocksMatchingUC.ascx" TagPrefix="uc1" TagName="StocksMatchingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:StocksMatchingUC runat="server" id="StocksMatchingUC" />
    </article>
</asp:Content>
