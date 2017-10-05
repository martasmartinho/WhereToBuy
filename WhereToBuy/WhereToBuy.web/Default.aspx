<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WhereToBuy.web.Default" %>

<%@ Register Src="~/UserControls/Authentication/Login/Login.ascx" TagPrefix="uc1" TagName="Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:Login runat="server" ID="Login" />
    </article>
     
</asp:Content>
