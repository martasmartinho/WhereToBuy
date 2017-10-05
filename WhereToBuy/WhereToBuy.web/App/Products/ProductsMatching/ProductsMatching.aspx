<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductsMatching.aspx.cs" Inherits="WhereToBuy.web.App.Products.ProductsMatching.ProductsMatching" %>
<%@ Register Src="~/UserControls/Products/ProductsMatching/ProductsMatchingUC.ascx" TagPrefix="uc1" TagName="ProductsMatchingUC" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:ProductsMatchingUC runat="server" ID="ProductsMatchingUC" />
    </article>
</asp:Content>
