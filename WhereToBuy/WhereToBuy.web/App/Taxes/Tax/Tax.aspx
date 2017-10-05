<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tax.aspx.cs" Inherits="WhereToBuy.web.App.Taxes.Tax.Tax" %>

<%@ Register Src="~/UserControls/Taxes/Tax/TaxUC.ascx" TagPrefix="uc1" TagName="TaxUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:TaxUC runat="server" id="TaxUC" />
</asp:Content>
