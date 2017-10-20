<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="WhereToBuy.web.App.Quotations.Quotation.Quotation" %>
<%@ Register Src="~/UserControls/Quotations/Quotation/QuotationUC.ascx" TagPrefix="uc1" TagName="QuotationUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
         <uc1:QuotationUC runat="server" id="QuotatiosUC" />
    </article>
    
</asp:Content>

