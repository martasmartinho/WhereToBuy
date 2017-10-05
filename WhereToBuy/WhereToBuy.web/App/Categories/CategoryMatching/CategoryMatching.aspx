<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CategoryMatching.aspx.cs" Inherits="WhereToBuy.web.App.Categories.CategoryMatching.CategoryMatching" %>

<%@ Register Src="~/UserControls/Categories/CategoryMatching/CategoryMatchingUC.ascx" TagPrefix="uc1" TagName="CategoryMatchingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CategoryMatchingUC runat="server" ID="CategoryMatchingUC" />
</asp:Content>
