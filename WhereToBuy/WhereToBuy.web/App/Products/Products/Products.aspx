<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WhereToBuy.web.App.Products.Products.Products" %>
<%@ Register Src="~/UserControls/Products/Products/ProductsUC.ascx" TagPrefix="uc1" TagName="ProductsUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:ProductsUC runat="server" ID="ProductsUC" />
    </article>
</asp:Content>
