<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Brands.aspx.cs" Inherits="WhereToBuy.web.App.Brands.Brands.Brands" %>

<%@ Register Src="~/UserControls/Brands/Brands/BrandsUC.ascx" TagPrefix="uc1" TagName="BrandsUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <!-- Button trigger modal -->
        <uc1:BrandsUC runat="server" ID="BrandsUC" />
    </article>

</asp:Content>
