<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductsMatchingUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.Products.ProductsMatching.ProductsMatchingUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>
<%@ Register Src="~/UserControls/Suppliers/SuppliersSelBox/SuppliersSelBox.ascx" TagPrefix="uc2" TagName="SuppliersSelBox" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
         <script type="text/javascript">
            // O METODO AJAX Sys.Application.add_load É EXECUTADO SEMPRE QUE O CONTEUDO DO UPDATEPANEL É
            // CARREGADO PELO BROWSER. ASSIM, SEMPRE QUE EXISTE ATUALIZAÇÃO DO UPDATEPANEL
            // ISTO É EXECUTADO (PRETENDIDO).
            // Sys.Application.add_load => ADICIONA CONTEUDO AO LOAD DA PAGINA
            Sys.Application.add_load(function () {
                $(document).ready(function () {
                    $('[data-toggle="tooltip"]').tooltip(); 

                });
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
                                <asp:Label ID="TitleLabel" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, ProductsMatchingString %>"></asp:Label>
                            </h2>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-6"></div>
                                <div class="col-lg-6">
                                    <div class="navbar-btn navbar-right">
                                        <h2>
                                            <asp:Button ID="NewElementButton" CssClass="btn btn-link btn-lg left " runat="server" Text="<%$ Resources:lang, NewString %>" OnClick="NewElementButton_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                            <asp:Button ID="UpdateElementButton" CssClass="btn btn-link btn-lg right" runat="server" Text="<%$ Resources:lang, EditString %>" OnClick="UpdateElementButton_Click" CausesValidation="false" UseSubmitBehavior="false" /></h2>
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
                                            <asp:Label ID="SupplierLabel" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, SupplierString %>"></asp:Label>
                                        </h4>
                                        <%--Supplier selbox--%>
                                        <uc2:SuppliersSelBox runat="server" ID="SuppliersSelBox" />
                                    </div>
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-5">
                                        <h4>
                                            <asp:Label ID="ExternalCodeLabel" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, ExternalCodeString %>"></asp:Label>
                                        </h4>
                                        <asp:TextBox ID="ExternalCodeTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <%-- button--%>
                                <div class="row">
                                    <div class="col-lg-9">
                                        <div class="btn-group">
                                            <asp:RadioButton ID="ActiveRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, ActiveString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="MatchingRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, WithoutMatchingString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="CustomRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, WithCustomString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="ResetRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, CloseResetString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="InactiveRadioButton" runat="server" AutoPostBack="true" CssClass="btn radiobutton text-muted" Text="<%$ Resources:lang, InactiveString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="AllRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, AllString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                         <div class="row">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn pull-right" Style="margin-right: 3%" Text="<%$ Resources:lang, SearchString %>" OnClick="btnSearch_Click" CausesValidation="false" UseSubmitBehavior="false"/>
                                            <asp:Button ID="btnClean" runat="server" CssClass="btn pull-right" Style="margin-right: 10px;" Text="<%$ Resources:lang, CleanString %>" OnClick="btnClean_Click" CausesValidation="false" UseSubmitBehavior="false"/>
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
                        <asp:GridView ID="ProductMatchingGridView" CssClass="table table-responsive" runat="server" AllowSorting="true"
                            AllowPaging="True" PageSize="10" OnPageIndexChanged="ProductMatchingGridView_PageIndexChanged" OnPageIndexChanging="ProductMatchingGridView_PageIndexChanging"
                            OnRowDataBound="ProductMatchingGridView_RowDataBound"
                            OnRowCommand="ProductMatchingGridView_RowCommand" OnRowCreated="ProductMatchingGridView_RowCreated" OnSorting="ProductMatchingGridView_Sorting">
                            <Columns>
                                <asp:BoundField DataField="Supplier.Name" HeaderText="Supplier" ReadOnly="True"
                                    SortExpression="Supplier.Name" Visible="false" />
                                <asp:TemplateField HeaderText="Supplier" SortExpression="Supplier.Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="SupplierLinkSort" CommandName="Sort" CommandArgument="FornecedorCodigo" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, SupplierString %>">Supplier</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="SupplierLabel" runat="server" Text='<%# string.Format("{0}<p/>{1}",((WhereToBuy.entities.ProductMatching)Container.DataItem).Supplier.Code, 
                                                                                                                                                       ((WhereToBuy.entities.ProductMatching)Container.DataItem).Supplier.Name )%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="25%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code/Description" SortExpression="FirstName">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="CodeDescriptionLinkSort" CommandName="Sort" CommandArgument="Codigo" CssClass="btn-link text-muted" Font-Bold="true"><%= Resources.lang.CodeString + "/" + Resources.lang.DescriptionString%></asp:LinkButton>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="header" />
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="CodeLabel" runat="server" Text='<%# string.Format("{0}<p/>",((WhereToBuy.entities.ProductMatching)Container.DataItem).Code)%>'></asp:Label>
                                            <asp:Label ID="SupplementLabel" runat="server" Text='<%# string.Format("{0}",((WhereToBuy.entities.ProductMatching)Container.DataItem).Supplement)%>'></asp:Label>
                                        </div>
                                        
                                    </ItemTemplate>
                                    <ItemStyle Width="25%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product" SortExpression="ProductDescription">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="ProductLinkSort" CommandName="Sort" CommandArgument="MapTo" CssClass="btn-link" Font-Bold="true" Text="<%$ Resources:lang, ProductString %>">Product</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                             <asp:Label ID="MapToCodeLabel" runat="server"
                                            Text='<%# ((WhereToBuy.entities.ProductMatching)Container.DataItem).MapTo != null ? string.Format("[{0}]<p/>    {1}<p/>", ((WhereToBuy.entities.ProductMatching)Container.DataItem).MapTo.Code,
                                                      ((WhereToBuy.entities.ProductMatching)Container.DataItem).MapTo.Description) : string.Empty %>'></asp:Label>
                                            <a href="#" data-toggle="tooltip" title='<%# this.GetGlobalResourceObject("lang", "HoursString") %>'>
                                                <span class="glyphicon glyphicon-info-sign"></span>
                                            </a>
                                            <asp:Label ID="PartnumberLabel" runat="server" Text='<%# string.Format("<strong>{0}:</strong>   {1}<p/>", this.GetGlobalResourceObject("lang", "HoursString"), 
                                                                                                                    ((WhereToBuy.entities.ProductMatching)Container.DataItem).QuotationExpireHours) %>'></asp:Label>
                                            <a href="#" data-toggle="tooltip" title='<%# this.GetGlobalResourceObject("lang", "HoursString") %>'>
                                                <span class="glyphicon glyphicon-info-sign"></span>
                                            </a>
                                            <asp:Label ID="Label1" runat="server" Text='<%# string.Format("<strong>{0}:</strong>   {1}<p/>", this.GetGlobalResourceObject("lang", "StockString"), 
                                            ((WhereToBuy.entities.ProductMatching)Container.DataItem).ReplacementStock != null ? ((WhereToBuy.entities.ProductMatching)Container.DataItem).ReplacementStock.ToString(): string.Empty)%>'></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text='<%# string.Format("<strong>{0}:</strong>   {1}<p/>", this.GetGlobalResourceObject("lang", "NeedPreventionPricesOutString"), 
                                            ((WhereToBuy.entities.ProductMatching)Container.DataItem).NeedPreventionPricesOut ? this.GetGlobalResourceObject("lang", "YesString"): this.GetGlobalResourceObject("lang", "NoString"))%>'></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text='<%# string.Format("<strong>{0}:</strong>   {1}<p/>", this.GetGlobalResourceObject("lang", "NeedPreventionFakeStockString"), 
                                            ((WhereToBuy.entities.ProductMatching)Container.DataItem).NeedPreventionFakeStock ? this.GetGlobalResourceObject("lang", "YesString"): this.GetGlobalResourceObject("lang", "NoString"))%>'></asp:Label>
                                            <a href="#" data-toggle="tooltip" title='<%# this.GetGlobalResourceObject("lang", "HoursString") %>'>
                                                <span class="glyphicon glyphicon-info-sign"></span>
                                            </a>
                                            <asp:Label ID="Label4" runat="server" Text='<%# string.Format("<strong>{0}:</strong>   {1}<p/>", this.GetGlobalResourceObject("lang", "DataResetString"), 
                                            ((WhereToBuy.entities.ProductMatching)Container.DataItem).DataReset.ToString())%>'></asp:Label>
                                        </div>
                                       
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="ActiveHeader" CssClass="text-primary" Text="<%$ Resources:lang, ActiveString %>"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<asp:CheckBox ID="ActiveCheckBox" CssClass=" checkbox-inline " runat="server" Enabled="false" Checked='<%# !((WhereToBuy.entities.ProductMatching)Container.DataItem).Inactive %>' OnCheckedChanged="ActiveCheckBox_CheckedChanged" />--%>
                                        <div style="display: block; text-align: center; vertical-align: middle">
                                            <%# ((bool)DataBinder.Eval(Container, "DataItem.Inactive") == false) ? "&#10004" : ""%>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OpenProductMatching">
                                    <HeaderTemplate>
                                        <asp:Label ID="SelectRowLabel" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" style="display:none"><span class="glyphicon glyphicon-open"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSelect" runat="server" CommandName="OpenSelect" CommandArgument = "" ToolTip="<%$ Resources:lang, OpenString %>"><span class="glyphicon glyphicon-open"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerTemplate>
                                <asp:Panel runat="server" CssClass="container-fluid">
                                    <%--<ul class="pagination">--%>
                                    <ul class="pager">
                                        <li class="previous">
                                            <asp:LinkButton ID="BackwardButton" runat="server" CommandName="Backward" OnClick="PageButton_Click"><%= Resources.lang.BackwardString%></asp:LinkButton></asp></li>
                                        <li class="previous">
                                            <asp:Label ID="ActualPageLabel" runat="server" CssClass="btn-primary" BackColor="#428bca"></asp:Label></li>
                                        <li class="previous">
                                            <asp:LinkButton ID="ForwardButton" runat="server" CommandName="Forward" OnClick="PageButton_Click"><%= Resources.lang.ForwardString%></asp:LinkButton></li>
                                    </ul>
                                </asp:Panel>
                            </PagerTemplate>
                            <EmptyDataTemplate>
                                não existem registos nestas condições
                            </EmptyDataTemplate>
                            <%--<PagerStyle CssClass="pagination-ys" />--%>
                            <FooterStyle Height="100%" />
                            <SelectedRowStyle CssClass="info" />
                            <%--<RowStyle CssClass="active" />--%>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-lg-1"></div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<uc1:MessageUC runat="server" ID="MessageUC" />

