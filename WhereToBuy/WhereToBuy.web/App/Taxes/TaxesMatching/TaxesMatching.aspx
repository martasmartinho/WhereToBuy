<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TaxesMatching.aspx.cs" Inherits="WhereToBuy.web.App.Taxes.TaxesMatching.TaxesMatching" %>

<%@ Register Src="~/UserControls/Taxes/TaxesMatching/TaxesMatchingUC.ascx" TagPrefix="uc1" TagName="TaxesMatchingUC" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:TaxesMatchingUC runat="server" id="TaxesMatchingUC" />
    </article>
</asp:Content>
