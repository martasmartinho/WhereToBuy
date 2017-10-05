<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Catalogs.aspx.cs" Inherits="WhereToBuy.web.App.Catalogs.Catalogs.Catalogs" %>

<%@ Register Src="~/UserControls/Catalogs/Catalogs/CatalogsUC.ascx" TagPrefix="uc1" TagName="CatalogsUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:CatalogsUC runat="server" ID="CatalogsUC" />
    </article>
</asp:Content>
