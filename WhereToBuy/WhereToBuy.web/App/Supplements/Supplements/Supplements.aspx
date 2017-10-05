<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Supplements.aspx.cs" Inherits="WhereToBuy.web.App.Supplements.Supplements.Supplements" %>

<%@ Register Src="~/UserControls/Supplements/Supplements/SupplementsUC.ascx" TagPrefix="uc1" TagName="SupplementsUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:SupplementsUC runat="server" id="SupplementsUC" />
    </article>
</asp:Content>
