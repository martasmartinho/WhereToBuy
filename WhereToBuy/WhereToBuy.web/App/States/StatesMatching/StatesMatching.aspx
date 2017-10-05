<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StatesMatching.aspx.cs" Inherits="WhereToBuy.web.App.States.StatesMatching.StatesMatching" %>

<%@ Register Src="~/UserControls/States/StatesMatching/StatesMatchingUC.ascx" TagPrefix="uc1" TagName="StatesMatchingUC" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:StatesMatchingUC runat="server" id="StatesMatchingUC" />
    </article>
</asp:Content>
