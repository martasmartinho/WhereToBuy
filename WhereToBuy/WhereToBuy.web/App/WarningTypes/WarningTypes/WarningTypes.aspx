<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WarningTypes.aspx.cs" Inherits="WhereToBuy.web.App.WarningTypes.WarningTypes.WarningTypes" %>

<%@ Register Src="~/UserControls/WarningTypes/WarningTypes/WarningTypesUC.ascx" TagPrefix="uc1" TagName="WarningTypesUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:WarningTypesUC runat="server" ID="WarningTypesUC" />
    </article>
</asp:Content>
