<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductsUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.Products.Products.ProductsUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>
<%@ Register Src="~/UserControls/Suppliers/SuppliersSelBox/SuppliersSelBox.ascx" TagPrefix="uc2" TagName="SuppliersSelBox" %>
<%@ Register Src="~/UserControls/Brands/BrandsSelBox/BrandsSelBox.ascx" TagPrefix="uc3" TagName="BrandsSelBox" %>
<%@ Register Src="~/UserControls/Categories/CategoriesSelBox/CategoriesSelBox.ascx" TagPrefix="uc4" TagName="CategoriesSelBox" %>



<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="container-fluid">
            <div class="row">
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 col-xl-1"></div>
                <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10 col-xl-10">
                    <%--title row--%>
                    <div class="row">
                        <div class="col-lg-6">
                            <h2>
                                <asp:Label ID="TitleLabel" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, ProductsString %>"></asp:Label>
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
                            <div class="col-xs-2 col-sm-2 col-md-1 col-lg-1"></div>
                            <div class="col-xs-8 col-sm-8 col-md-10 col-lg-10">
                                <%-- supplier--%>
                                <div class="row">
                                    <div class="col-sm-5 col-md-5 col-lg-5">
                                        <h4>
                                            <asp:Label ID="BrandLabel" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, BrandString %>"></asp:Label>
                                        </h4>
                                        <%--Brand selbox--%>
                                        <uc3:BrandsSelBox runat="server" ID="BrandsSelBox" />
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1"></div>
                                    <div class="col-sm-6 col-md-6 col-lg-6">
                                        <h4>
                                            <asp:Label ID="CategoryLabel" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, CategoryString %>"></asp:Label>
                                        </h4>
                                        <%--Category selbox--%>
                                        <uc4:CategoriesSelBox runat="server" ID="CategoriesSelBox" />
                                    </div>
                                   
                                   
                                </div>
                                <br />
                            </div>
                            <div class="col-xs-2 col-sm-2 col-md-1 col-lg-1"></div>
                        </div>
                        <div class="row">
                            <div class="col-xs-2 col-sm-2 col-md-1  col-lg-1"></div>
                            <div class="col-xs-8 col-sm-8 col-md-10 col-lg-10">
                                <%-- supplier--%>
                                <div class="row">
                                     <div class="col-sm-5 col-md-5 col-lg-5" >
                                        <h4>
                                            <asp:Label ID="Label2" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, SupplierString %>"></asp:Label>
                                        </h4>
                                        <%--Category selbox--%>
                                        <uc2:SuppliersSelBox runat="server" ID="SuppliersSelBox" />
                                    </div>
                                        
                                        
                                    <div class="col-sm-1 col-md-1 col-lg-1"></div>
                                    <div class="col-sm-6 col-md-6 col-lg-6">
                                        <h4>
                                            <asp:Label ID="Label1" runat="server" CssClass="text-muted" Text="Partnumber"></asp:Label>
                                        </h4>
                                        <asp:TextBox ID="PartnumberTxt" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    
                                </div>
                                <br />
                                <%-- button--%>
                                <div class="row">
                                    <div class="col-lg-9">
                                        <div class="btn-group">
                                            <asp:RadioButton ID="CurrentRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, CurrentsString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="ManualUpdateRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, ManualUpdateString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="IPCRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted"  Text="<%$ Resources:lang,IPCString %>" OnCheckedChanged="RadioButton_CheckedChanged"/>
                                            <asp:RadioButton ID="DiscontinuedRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, DiscontinuedString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
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
                            <div class="col-xs-2 col-sm-2 col-md-1 col-lg-1"></div>
                        </div>
                        <br />
                        <br />
                    </div>
                    <br />
                    <br />
                    <%--grid--%>
                    <div class="navbar row">
                        <asp:GridView ID="ProductGridView" CssClass="table table-responsive" runat="server" AllowSorting="true"
                            AllowPaging="True" PageSize="10" OnPageIndexChanged="ProductGridView_PageIndexChanged" OnPageIndexChanging="ProductGridView_PageIndexChanging"
                            OnRowDataBound="ProductGridView_RowDataBound"
                            OnRowCommand="ProductGridView_RowCommand" OnRowCreated="ProductGridView_RowCreated" OnSorting="ProductGridView_Sorting">
                            <Columns>
                                <asp:BoundField DataField="Product.Code" HeaderText="Product" ReadOnly="True"
                                    SortExpression="Product.Code" Visible="false" />
                                <asp:TemplateField HeaderText="Product" SortExpression="Supplier.Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="ProductLinkSort" CommandName="Sort" CommandArgument="Codigo" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, ProductString %>">Product</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="ProductLabel" runat="server" Text='<%# string.Format("<strong>{0}<p/><p/>{1}<p/>{2}<p/>{3}<p/>{4}</strong>",((WhereToBuy.entities.Product)Container.DataItem).Code,
                                                                                                                                                    ((WhereToBuy.entities.Product)Container.DataItem).Supplier.Code, 
                                                                                                                                                    ((WhereToBuy.entities.Product)Container.DataItem).Brand.Code, 
                                                                                                                                                    ((WhereToBuy.entities.Product)Container.DataItem).Category.Code, 
                                                                                                                                                       ((WhereToBuy.entities.Product)Container.DataItem).ContentConcernIndex )%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="CodeDescriptionLinkSort" CommandName="Sort" CommandArgument="Descricao" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, DescriptionString %>"></asp:LinkButton>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="header" />
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="DescriptionLabel" runat="server" Text='<%# string.Format("<strong>{0}</strong><p/><p/>", ((WhereToBuy.entities.Product)Container.DataItem).Description)%>'></asp:Label>

                                            <asp:Label ID="PartnumberLabel" runat="server" Text='<%# string.Format("<strong>{0}:</strong>   {1}<p/>", this.GetGlobalResourceObject("lang", "PartnumberString"), 
                                                                                                                    ((WhereToBuy.entities.Product)Container.DataItem).Partnumber) %>'></asp:Label>

                                            <asp:Label ID="StockU1Label" runat="server" Text='<%# string.Format("<strong>{0}:</strong>  <del>[{1}] {2}</del><p/>",this.GetGlobalResourceObject("lang", "StockString") as string, ((WhereToBuy.entities.Product)Container.DataItem).Stock_U1!=null?((WhereToBuy.entities.Product)Container.DataItem).Stock_U1.Code:this.GetGlobalResourceObject("lang", "NoDataString") as string,
                                                                                                                        ((WhereToBuy.entities.Product)Container.DataItem).Stock_U1!=null?((WhereToBuy.entities.Product)Container.DataItem).Stock_U1.Description:string.Empty)%>'></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text='<%# string.Format("     [{0}] {1}<p/>", ((WhereToBuy.entities.Product)Container.DataItem).Stock!=null?((WhereToBuy.entities.Product)Container.DataItem).Stock.Code:this.GetGlobalResourceObject("lang", "NoDataString") as string,
                                            ((WhereToBuy.entities.Product)Container.DataItem).Stock!=null?((WhereToBuy.entities.Product)Container.DataItem).Stock_U1.Description:string.Empty)%>'></asp:Label>
                                            <asp:Label ID="ICDLabem" runat="server" Text='<%# string.Format("<strong>{0}:</strong>  {1}", this.GetGlobalResourceObject("lang", "ICPString") as string, ((WhereToBuy.entities.Product)Container.DataItem).Supplier.ProductAvailableTrust)%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price" SortExpression="ProductPrice">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="PriceLinkSort" CommandName="Sort" CommandArgument="PrecoCusto" CssClass="btn-link" Font-Bold="true" Text="<%$ Resources:lang, PriceString %>">Price</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="DateLabel" runat="server" CssClass="pull-center" Text='<%# (string.Format("<strong>{0}</strong><p/><p/>", ((WhereToBuy.entities.Product)Container.DataItem).CostPrice_Date.ToString())) %>'></asp:Label>
                                            <asp:Label ID="ICPLabel" runat="server" Text='<%# string.Format("<strong>{0}:</strong> {1}<p/><p/><p/>",this.GetGlobalResourceObject("lang", "ICPString") as string, ((WhereToBuy.entities.Product)Container.DataItem).Supplier.ProductPriceTrust)%>'></asp:Label>
                                            <asp:Label ID="PriceLabel" runat="server" style="text-align: center;width:100%" Text='<%# string.Format("<h3>{0}€</h3>",((WhereToBuy.entities.Product)Container.DataItem).CostPrice.ToString("0.00"))%>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discontinued">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="DiscontinuedHeader" CssClass="text-primary" Text="<%$ Resources:lang, DiscontinuedString %>"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<asp:CheckBox ID="ActiveCheckBox" CssClass=" checkbox-inline " runat="server" Enabled="false" Checked='<%# !((WhereToBuy.entities.Product)Container.DataItem).Inactive %>' OnCheckedChanged="ActiveCheckBox_CheckedChanged" />--%>
                                        <div style="display: block; text-align: center; vertical-align: middle">
                                            <%# ((bool)DataBinder.Eval(Container, "DataItem.Discontinued") == true) ? "&#10004" : ""%>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OpenProduct">
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
                <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1 col-xl-1"></div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<uc1:MessageUC runat="server" ID="MessageUC" />
