<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="WhereToBuy.web.App.Stocks.Stock.Stock" %>

<%@ Register Src="~/UserControls/Stocks/Stock/StockUC.ascx" TagPrefix="uc1" TagName="StockUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:StockUC runat="server" id="StockUC" />
</asp:Content>
