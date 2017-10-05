<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StockMatching.aspx.cs" Inherits="WhereToBuy.web.App.Stocks.StockMatching.StockMatching" %>

<%@ Register Src="~/UserControls/Stocks/StockMatching/StockMatchingUC.ascx" TagPrefix="uc1" TagName="StockMatchingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:StockMatchingUC runat="server" ID="StockMatchingUC" />
</asp:Content>
