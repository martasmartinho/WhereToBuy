<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Brand.aspx.cs" Inherits="WhereToBuy.web.App.Brands.Brand.Brand" %>

<%@ Register Src="~/UserControls/Brands/Brand/BrandUC.ascx" TagPrefix="uc1" TagName="BrandUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:BrandUC runat="server" ID="BrandUC" />
</asp:Content>

