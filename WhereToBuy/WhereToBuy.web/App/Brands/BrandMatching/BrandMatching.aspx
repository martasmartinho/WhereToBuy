<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BrandMatching.aspx.cs" Inherits="WhereToBuy.web.App.Brands.BrandMatching.BrandMatching" %>

<%@ Register Src="~/UserControls/Brands/BrandMatching/BrandMatchingUC.ascx" TagPrefix="uc1" TagName="BrandMatchingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:BrandMatchingUC runat="server" ID="BrandMatchingUC" />
</asp:Content>
