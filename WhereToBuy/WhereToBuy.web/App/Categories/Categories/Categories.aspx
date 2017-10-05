<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="WhereToBuy.web.App.Categories.Categories.Categories" %>

<%@ Register Src="~/UserControls/Categories/Categories/CategoriesUC.ascx" TagPrefix="uc1" TagName="CategoriesUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:CategoriesUC runat="server" id="CategoriesUC" />
    </article>
</asp:Content>
