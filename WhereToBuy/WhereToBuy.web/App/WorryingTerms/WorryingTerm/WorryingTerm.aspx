<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WorryingTerm.aspx.cs" Inherits="WhereToBuy.web.App.WorryingTerms.WorryingTerm.WorryingTerm" %>

<%@ Register Src="~/UserControls/WorryingTerms/WorryingTerm/WorryingTermUC.ascx" TagPrefix="uc1" TagName="WorryingTermUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:WorryingTermUC runat="server" id="WorryingTermUC" />
</asp:Content>
