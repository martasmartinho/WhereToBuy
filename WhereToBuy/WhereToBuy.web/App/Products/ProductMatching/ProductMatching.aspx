<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductMatching.aspx.cs" Inherits="WhereToBuy.web.App.Products.ProductMatching.ProductMatching" %>
<%@ Register Src="~/UserControls/Products/ProductMatching/ProductMatchingUC.ascx" TagPrefix="uc1" TagName="ProductMatchingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ProductMatchingUC runat="server" ID="ProductMatchingUC" />
</asp:Content>
