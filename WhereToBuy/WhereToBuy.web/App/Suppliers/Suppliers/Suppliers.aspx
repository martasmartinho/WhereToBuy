<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="WhereToBuy.web.App.Suppliers.Suppliers.Suppliers" %>

<%@ Register Src="~/UserControls/Suppliers/Suppliers/SuppliersUC.ascx" TagPrefix="uc1" TagName="SuppliersUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:SuppliersUC runat="server" id="SuppliersUC" />
</asp:Content>
