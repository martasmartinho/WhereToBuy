<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="TaxMatching.aspx.cs" Inherits="WhereToBuy.web.App.Taxes.TaxMatching.TaxMatching" %>

<%@ Register Src="~/UserControls/Taxes/TaxMatching/TaxMatchingUC.ascx" TagPrefix="uc1" TagName="TaxMatchingUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:TaxMatchingUC runat="server" ID="TaxMatchingUC" />
</asp:Content>
