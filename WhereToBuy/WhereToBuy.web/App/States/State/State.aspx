<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="State.aspx.cs" Inherits="WhereToBuy.web.App.States.State.State" %>

<%@ Register Src="~/UserControls/States/State/StateUC.ascx" TagPrefix="uc1" TagName="StateUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:StateUC runat="server" ID="StateUC" />

</asp:Content>
