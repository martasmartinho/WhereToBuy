<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Quotations.aspx.cs" Inherits="WhereToBuy.web.App.Quotations.Quotations.Quotations" %>
<%@ Register Src="~/UserControls/Quotations/Quotations/QuotationsUC.ascx" TagPrefix="uc1" TagName="QuotationsUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
         <uc1:QuotationsUC runat="server" id="QuotationsUC" />
    </article>
    
</asp:Content>
