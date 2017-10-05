<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WarningType.aspx.cs" Inherits="WhereToBuy.web.App.WarningTypes.WarningType.WarningType" %>

<%@ Register Src="~/UserControls/WarningTypes/WarningType/WarningTypeUC.ascx" TagPrefix="uc1" TagName="WarningTypeUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WarningTypeUC runat="server" ID="WarningTypeUC" />
</asp:Content>
