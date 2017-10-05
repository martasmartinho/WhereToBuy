<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WorryingTerms.aspx.cs" Inherits="WhereToBuy.web.App.WorryingTerms.WorryingTerms.WorryingTerms" %>

<%@ Register Src="~/UserControls/WorryingTerms/WorryingTerms/WorryingTermsUC.ascx" TagPrefix="uc1" TagName="WorryingTermsUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:WorryingTermsUC runat="server" ID="WorryingTermsUC" />
    </article>
</asp:Content>
