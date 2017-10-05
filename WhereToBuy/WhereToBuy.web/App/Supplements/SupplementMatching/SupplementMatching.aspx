<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SupplementMatching.aspx.cs" Inherits="WhereToBuy.web.App.Supplements.SupplementMatching.SupplementMatching" %>

<%@ Register Src="~/UserControls/Supplements/Supplements/SupplementsUC.ascx" TagPrefix="uc1" TagName="SupplementsUC" %>
<%@ Register Src="~/UserControls/Supplements/SupplementMatching/SupplementMatchingUC.ascx" TagPrefix="uc1" TagName="SupplementMatchingUC" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:SupplementMatchingUC runat="server" ID="SupplementMatchingUC" />
</asp:Content>
