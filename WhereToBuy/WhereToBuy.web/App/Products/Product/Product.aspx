<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="WhereToBuy.web.App.Products.Product.Product" %>
<%@ Register Src="~/UserControls/Products/Product/ProductUC.ascx" TagPrefix="uc1" TagName="ProductUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ProductUC runat="server" ID="ProductUC" />
</asp:Content>

