<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Classes.aspx.cs" Inherits="WhereToBuy.web.App.Classes.Classes.Classes" %>

<%@ Register Src="~/UserControls/Classes/Classes/ClassesUC.ascx" TagPrefix="uc1" TagName="ClassesUC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <article>
        <uc1:ClassesUC runat="server" id="ClassesUC" />
    </article>
</asp:Content>
