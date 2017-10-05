<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Supplier.aspx.cs" Inherits="WhereToBuy.web.App.Suppliers.Supplier.Supplier" %>

<%@ Register Src="~/UserControls/Suppliers/Supplier/SupplierUC.ascx" TagPrefix="uc1" TagName="SupplierUC" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:SupplierUC runat="server" id="SupplierUC" />
</asp:Content>
