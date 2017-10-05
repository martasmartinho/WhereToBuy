<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuotationRulesUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.QuotationRules.QuotationRules.QuotationRulesUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>
<%@ Register Src="~/UserControls/Suppliers/SuppliersSelBox/SuppliersSelBox.ascx" TagPrefix="uc1" TagName="SuppliersSelBox" %>
<%@ Register Src="~/UserControls/Categories/CategoriesSelBox/CategoriesSelBox.ascx" TagPrefix="uc1" TagName="CategoriesSelBox" %>
<%@ Register Src="~/UserControls/Brands/BrandsSelBox/BrandsSelBox.ascx" TagPrefix="uc1" TagName="BrandsSelBox" %>
<%@ Register Src="~/UserControls/Stocks/StocksSelBox/StocksSelBox.ascx" TagPrefix="uc1" TagName="StocksSelBox" %>






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
                                <asp:Label ID="lblTitle" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, QuotationRulesString %>"></asp:Label>
                            </h2>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-6"></div>
                                <div class="col-lg-6">
                                    <div class="navbar-btn navbar-right">
                                        <h2>
                                            <asp:Button ID="btnNewElement" CssClass="btn btn-link btn-lg left " runat="server" Text="<%$ Resources:lang, NewString %>" onClick="btnNewElement_Click"/>
                                            <asp:Button ID="btnUpdateElement" CssClass="btn btn-link btn-lg right" runat="server" Text="<%$ Resources:lang, EditString %>" OnClick="btnUpdateElement_Click"/></h2>
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
                                            <asp:Label ID="lblCategory" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, CategoryString %>"></asp:Label>
                                        </h4>
                                        <uc1:CategoriesSelBox runat="server" ID="CategoriesSelBox" />
                                    </div>
                                </div>
                                <br />
                                <%-- supplier--%>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <h4>
                                            <asp:Label ID="Label1" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, BrandString %>"></asp:Label>
                                        </h4>
                                        <uc1:BrandsSelBox runat="server" ID="BrandsSelBox" />
                                    </div>
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-5">
                                        <h4>
                                            <asp:Label ID="Label2" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, StockString %>"></asp:Label>
                                        </h4>
                                        <uc1:StocksSelBox runat="server" ID="StocksSelBox" />
                                    </div>
                                </div>
                                <br />
                                <%-- button--%>
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="btn-group">
                                            <asp:RadioButton ID="btnCustomization" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, WithCustomizationString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="btnReset" runat="server" AutoPostBack="true" CssClass="btn radiobutton text-muted" Text="<%$ Resources:lang, CloseResetString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="btnAll" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, AllString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
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
                    <div class="navbar row">
                        <asp:GridView ID="gvQuotationRules" CssClass="table table-responsive" runat="server" AllowSorting="true"
                            AllowPaging="True" PageSize="10" OnPageIndexChanged="gvQuotationRules_PageIndexChanged" OnPageIndexChanging="gvQuotationRules_PageIndexChanging" 
                            OnRowDataBound="gvQuotationRules_RowDataBound" OnSelectedIndexChanging="gvQuotationRules_SelectedIndexChanging" OnSelectedIndexChanged="gvQuotationRules_SelectedIndexChanged"
                            OnRowCommand="gvQuotationRules_RowCommand" OnRowCreated="gvQuotationRules_RowCreated" OnSorting="gvQuotationRules_Sorting">

                            <Columns>

                                <asp:BoundField DataField="QuotationRule.Supplier.Name" HeaderText="<%$ Resources:lang, QuotationString %>" ReadOnly="True"
                                    SortExpression="FornecedorNome" Visible="false" />
                                <asp:TemplateField HeaderText="<%$ Resources:lang, RuleString %>" >
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkQuotation" CommandName="Sort" CommandArgument="FornecedorNome" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, QuotationString %>">Description</asp:LinkButton>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="header" />
                                    <ItemTemplate>
                                           <asp:Label ID="lblQuotationInformation" runat="server" Text='<%# string.Format("<p/>{0}: {1}<p/>{2}: {3}<p/>{4}: {5}<p/>{6}: {7}", this.GetGlobalResourceObject("lang", "SupplierString") as string,((WhereToBuy.entities.QuotationRule)Container.DataItem).Supplier.ToString(), 
                                                                                                                                                           this.GetGlobalResourceObject("lang", "CategoryString") as string, ((WhereToBuy.entities.QuotationRule)Container.DataItem).Category.ToString(),
                                                                                                                                                         this.GetGlobalResourceObject("lang", "BrandString") as string, ((WhereToBuy.entities.QuotationRule)Container.DataItem).Brand.ToString(),
                                                                                                                                                         this.GetGlobalResourceObject("lang", "StockString") as string, ((WhereToBuy.entities.QuotationRule)Container.DataItem).Stock.ToString())%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                              
                                <asp:TemplateField HeaderText="<%$ Resources:lang, RuleString %>" >
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkRule" CommandName="Sort" CommandArgument="DataReset" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, RuleString %>">Description</asp:LinkButton>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="header" />
                                    <ItemTemplate>
                                           <asp:Label ID="lblExtraInformation" runat="server" Text='<%# string.Format("<p/>{0} !: {1}<p/>{2} !: {3}<p/>{4}: {5}<p/>{6}: {7}", this.GetGlobalResourceObject("lang", "ExpireHoursString") as string,((WhereToBuy.entities.QuotationRule)Container.DataItem).ExpitationHours.ToString(), 
                                                                                                                                                           this.GetGlobalResourceObject("lang", "StockString") as string, 
                                                                                                                                                           ((WhereToBuy.entities.QuotationRule)Container.DataItem).SubstituteStock!=null?((WhereToBuy.entities.QuotationRule)Container.DataItem).SubstituteStock.ToString():"",
                                                                                                                                                         this.GetGlobalResourceObject("lang", "DataResetString") as string, ((WhereToBuy.entities.QuotationRule)Container.DataItem).DataReset.ToString(),
                                                                                                                                                         this.GetGlobalResourceObject("lang", "NotesString") as string, ((WhereToBuy.entities.QuotationRule)Container.DataItem).Notes)%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="...">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblSelect" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" style="display:none"><span class="glyphicon glyphicon-open"></asp:LinkButton>
                                            <asp:LinkButton ID="btnSelect" runat="server" CommandName="OpenSelect" CommandArgument ='<%# ((WhereToBuy.entities.QuotationRule)Container.DataItem).Supplier.Code +";"+((WhereToBuy.entities.QuotationRule)Container.DataItem).Brand.Code +";"+((WhereToBuy.entities.QuotationRule)Container.DataItem).Category.Code +";"+((WhereToBuy.entities.QuotationRule)Container.DataItem).Stock.Code  %>' ToolTip="<%$ Resources:lang, OpenString %>" ><span class="glyphicon glyphicon-open"></asp:LinkButton>
                                        </div>
                                        
                                        
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />

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