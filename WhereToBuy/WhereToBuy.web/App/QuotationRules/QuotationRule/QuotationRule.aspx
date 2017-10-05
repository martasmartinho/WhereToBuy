<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="QuotationRule.aspx.cs" Inherits="WhereToBuy.web.App.QuotationRules.QuotationRule.QuotationRule" %>

<%@ Register Src="~/UserControls/QuotationRules/QuotationRule/QuotationRuleUC.ascx" TagPrefix="uc1" TagName="QuotationRuleUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:QuotationRuleUC runat="server" id="QuotationRuleUC" />
</asp:Content>
