<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BrandsMatching.aspx.cs" Inherits="WhereToBuy.web.App.Brands.BrandsMatching.BrandsMatching" %>

<%@ Register Src="~/UserControls/Brands/BrandsMatching/BrandsMatchingUC.ascx" TagPrefix="uc1" TagName="BrandsMatchingUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:BrandsMatchingUC runat="server" ID="BrandsMatchingUC1" />
    </article>
</asp:Content>
