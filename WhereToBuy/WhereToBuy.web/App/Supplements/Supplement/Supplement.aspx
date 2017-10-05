<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Supplement.aspx.cs" Inherits="WhereToBuy.web.App.Supplements.Supplement.Supplement" %>

<%@ Register Src="~/UserControls/Supplements/Supplement/SupplementUC.ascx" TagPrefix="uc1" TagName="SupplementUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:SupplementUC runat="server" id="SupplementUC" />
</asp:Content>
