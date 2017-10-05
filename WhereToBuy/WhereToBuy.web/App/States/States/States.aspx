<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="States.aspx.cs" Inherits="WhereToBuy.web.App.States.States.States" %>

<%@ Register Src="~/UserControls/States/States/StatesUC.ascx" TagPrefix="uc1" TagName="StatesUC" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:StatesUC runat="server" id="StatesUC" />
    </article>
</asp:Content>
