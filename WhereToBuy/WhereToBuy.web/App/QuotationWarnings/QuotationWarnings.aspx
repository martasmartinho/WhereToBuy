<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuotationWarnings.aspx.cs" Inherits="WhereToBuy.web.App.QuotationWarnings.QuotationWarnings" %>
<%@ Register Src="~/UserControls/QuotationWarnings/QuotationWarningsUC.ascx" TagPrefix="uc1" TagName="QuotationWarningsUC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
         <uc1:QuotationWarningsUC runat="server" id="QuotationWarningsUC" />
    </article>
</asp:Content>
