<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Taxes.aspx.cs" Inherits="WhereToBuy.web.App.Taxes.Taxes.Taxes" %>

<%@ Register Src="~/UserControls/Taxes/Taxes/TaxesUC.ascx" TagPrefix="uc1" TagName="TaxesUC" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:TaxesUC runat="server" id="TaxesUC" />
    </article>
   
</asp:Content>
