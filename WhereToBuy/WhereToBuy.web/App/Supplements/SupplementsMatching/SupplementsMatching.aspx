<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="SupplementsMatching.aspx.cs" Inherits="WhereToBuy.web.App.Supplements.SupplementsMatching.SupplementsMatching" %>

<%@ Register Src="~/UserControls/Supplements/SupplementsMatching/SupplementsMatchingUC.ascx" TagPrefix="uc1" TagName="SupplementsMatchingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:SupplementsMatchingUC runat="server" id="SupplementsMatchingUC" />
    </article>
</asp:Content>
