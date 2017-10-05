<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuotationRules.aspx.cs" Inherits="WhereToBuy.web.App.QuotationRules.QuotationRules.QuotationRules" %>

<%@ Register Src="~/UserControls/QuotationRules/QuotationRules/QuotationRulesUC.ascx" TagPrefix="uc1" TagName="QuotationRulesUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
         <uc1:QuotationRulesUC runat="server" id="QuotationRulesUC" />
    </article>
    
</asp:Content>
