<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="WhereToBuy.web.App.Categories.Category.Category" %>

<%@ Register Src="~/UserControls/Categories/Category/CategoryUC.ascx" TagPrefix="uc1" TagName="CategoryUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CategoryUC runat="server" id="CategoryUC" />
</asp:Content>
