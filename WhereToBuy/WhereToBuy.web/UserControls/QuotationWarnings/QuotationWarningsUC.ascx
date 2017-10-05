<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuotationWarningsUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.QuotationWarnings.QuotationWarningsUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>
<%@ Register Src="~/UserControls/Suppliers/SuppliersSelBox/SuppliersSelBox.ascx" TagPrefix="uc1" TagName="SuppliersSelBox" %>
<%@ Register Src="~/UserControls/WarningTypes/WarningTypesSelBox/WarningTypesSelBox.ascx" TagPrefix="uc2" TagName="WarningTypesSelBox" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <script type="text/javascript">
            Sys.Application.add_load(function () {

             });
        </script>
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-1"></div>
                <div class="col-lg-10">
                    <%--title row--%>
                    <div class="row">
                        <div class="col-lg-6">
                            <h2>
                                <asp:Label ID="lblTitle" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, WarningsString %>"></asp:Label>
                            </h2>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-6"></div>
                                <div class="col-lg-6">
                                    <div class="navbar-btn navbar-right">
                                        <h2>
                                        </h2>
                                    </div>

                                </div>

                            </div>
                        </div>

                    </div>

                    <%--search row--%>
                    <div class="navbar navbar-inverse">
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-xs-2 col-sm-2  col-lg-1"></div>
                            <div class="col-xs-8 col-sm-8 col-lg-10">
                                <%-- supplier--%>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h4>
                                            <asp:Label ID="lblSupplier" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, SupplierString %>"></asp:Label>
                                        </h4>
                                        <uc1:SuppliersSelBox runat="server" ID="SuppliersSelBox" />
                                    </div>
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-5">
                                        <h4>
                                            <asp:Label ID="lblWarningType" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, WarningTypeString %>"></asp:Label>
                                        </h4>
                                        <uc2:WarningTypesSelBox runat="server" ID="WarningTypesSelBox"/>
                                    </div>
                                </div>
                                <br />
                               
                                <%-- button--%>
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="btn-group">
                                           
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                         <div class="row">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn pull-right" Style="margin-right: 3%" Text="<%$ Resources:lang, SearchString %>" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnClean" runat="server" CssClass="btn pull-right" Style="margin-right: 10px;" Text="<%$ Resources:lang, CleanString %>" OnClick="btnClean_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-2 col-sm-2 col-lg-1"></div>
                        </div>
                        <br />
                        <br />
                    </div>


                    <br />
                    <br />

                    <%--grid--%>
                    <div class="navbar row"   style="z-index: 0;">
                        <asp:GridView ID="gvQuotationWarnings" CssClass="table table-responsive" runat="server" AllowSorting="true"
                            AllowPaging="True" PageSize="10" OnPageIndexChanged="gvQuotationWarnings_PageIndexChanged" OnPageIndexChanging="gvQuotationWarnings_PageIndexChanging" 
                            OnRowDataBound="gvQuotationWarnings_RowDataBound" 
                            OnRowCommand="gvQuotationWarnings_RowCommand" OnRowCreated="gvQuotationWarnings_RowCreated" OnSorting="gvQuotationWarnings_Sorting">

                            <Columns>

                                <asp:BoundField DataField="QuotationWarning.Date" HeaderText="<%$ Resources:lang, QuotationString %>" ReadOnly="True"
                                    SortExpression="Data" Visible="false" />
                                <asp:TemplateField HeaderText="<%$ Resources:lang, QuotationString %>" >
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkQuotation" CommandName="Sort" CommandArgument="Data" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, QuotationString %>">Description</asp:LinkButton>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="header" />
                                    <ItemTemplate>
                                         <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" style="display:none"><span class="glyphicon glyphicon-open"></asp:LinkButton>
                                           <asp:Label ID="lblQuotationInformation" runat="server" Text='<%# string.Format("<p/><b><ins>{0}</ins></b><p/>{1}<p/><b><ins>{2}</ins></b><p/>{3}<p/><b><ins>{4}</ins></b><p/>{5}<p/><b><ins>{6}</ins></b><p/>{7}", this.GetGlobalResourceObject("lang", "DateString") as string,((WhereToBuy.entities.QuotationWarning)Container.DataItem).Date.ToString(),
                                                                                                                                                         this.GetGlobalResourceObject("lang", "SupplierString") as string,((WhereToBuy.entities.QuotationWarning)Container.DataItem).Supplier.ToString(),  
                                                                                                                                                         this.GetGlobalResourceObject("lang", "ProductString") as string, ((WhereToBuy.entities.QuotationWarning)Container.DataItem).ProductCode.ToString(),
                                                                                                                                                         this.GetGlobalResourceObject("lang", "SupplementString") as string, ((WhereToBuy.entities.QuotationWarning)Container.DataItem).SupplementCode.ToString())%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                              
                                <asp:TemplateField HeaderText="<%$ Resources:lang, WarningString %>" >
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkWarning" CommandName="Sort" CommandArgument="Gravidade" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, WarningString %>">Description</asp:LinkButton>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="header" />
                                    <ItemTemplate>
                                           <asp:Label ID="lblExtraInformation" runat="server" Text='<%# string.Format("<p/>{0}: {1}<p/>{2}: {3}<p/>{4}: {5}<p/>{6}: {7}<p/>{8}: {9}", "Id",((WhereToBuy.entities.QuotationWarning)Container.DataItem).Id.ToString(), 
                                                                                                                                                         this.GetGlobalResourceObject("lang", "TypeString") as string, 
                                                                                                                                                           ((WhereToBuy.entities.QuotationWarning)Container.DataItem).WarningType.ToString(),
                                                                                                                                                         this.GetGlobalResourceObject("lang", "DescriptionString") as string, ((WhereToBuy.entities.QuotationWarning)Container.DataItem).Description,
                                                                                                                                                         this.GetGlobalResourceObject("lang", "SeverityString") as string, ((WhereToBuy.entities.QuotationWarning)Container.DataItem).WarningType.Severity.ToString(),
                                                                                                                                                         this.GetGlobalResourceObject("lang", "CreationString") as string, ((WhereToBuy.entities.QuotationWarning)Container.DataItem).Creation.ToString())%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="60%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                            </Columns>

                            <PagerTemplate>
                                <asp:Panel runat="server" CssClass="container-fluid" OnItemCommand="PageFooter_ItemCommand">
                                    <%--<ul class="pagination">--%>
                                    <ul class="pager">
                                        <li class="previous">
                                            <asp:LinkButton ID="btnBackward" runat="server" CommandName="Backward" OnClick="btnPage_Click"><%= Resources.lang.BackwardString%></asp:LinkButton></asp></li>
                                        <li class="previous">
                                            <asp:Label ID="lblActualPage" runat="server" CssClass="btn-primary"  BackColor="#428bca"></asp:Label></li>
                                        <li class="previous">
                                            <asp:LinkButton ID="btnForward" runat="server" CommandName="Forward" OnClick="btnPage_Click"><%= Resources.lang.ForwardString%></asp:LinkButton></li>
                                    </ul>
                                    
                                </asp:Panel>
                            </PagerTemplate>

                            <EmptyDataTemplate>
                                não existem registos nestas condições
                            </EmptyDataTemplate>
                            <FooterStyle Height="100%" />
                            <SelectedRowStyle CssClass="info" />

                        </asp:GridView>



                    </div>

                </div>
                 <div class="col-lg-1"></div>
            </div>

        </div>

    </ContentTemplate>
</asp:UpdatePanel>

<uc1:MessageUC runat="server" ID="MessageUC" />