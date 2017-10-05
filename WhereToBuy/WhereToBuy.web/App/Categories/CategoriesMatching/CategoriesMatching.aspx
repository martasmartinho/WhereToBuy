<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoriesMatching.aspx.cs" Inherits="WhereToBuy.web.App.Categories.CategoriesMatching.CategoriesMatching" %>

<%@ Register Src="~/UserControls/Categories/CategoriesMatching/CategoriesMatchingUC.ascx" TagPrefix="uc1" TagName="CategoriesMatchingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:CategoriesMatchingUC runat="server" id="CategoriesMatchingUC" />
    </article>
</asp:Content>
