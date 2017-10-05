<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" 
    CodeBehind="StateMatching.aspx.cs" Inherits="WhereToBuy.web.App.States.StateMatching.StateMatching" %>

<%@ Register Src="~/UserControls/States/StateMatching/StateMatchingUC.ascx" TagPrefix="uc1" TagName="StateMatchingUC" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:StateMatchingUC runat="server" ID="StateMatchingUC" />
</asp:Content>
