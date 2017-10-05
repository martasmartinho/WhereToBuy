<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BrandsMatchingUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.BrandsMatching.BrandsMatchingUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>
<%@ Register Src="~/UserControls/Suppliers/SuppliersSelBox/SuppliersSelBox.ascx" TagPrefix="uc1" TagName="SuppliersSelBox" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-1"></div>
                <div class="col-lg-10">
                    <%--title row--%>
                    <div class="row">
                        <div class="col-lg-6">
                            <h2>
                                <asp:Label ID="TitleLabel" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, BrandsMatchingString %>"></asp:Label>
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
                                        <uc1:SuppliersSelBox runat="server" ID="SuppliersSelBox" />
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
                                    <div class="col-lg-8">
                                        <div class="btn-group">
                                            <asp:RadioButton ID="ActiveRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, ActiveString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="MatchingRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, WithoutMatchingString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="InactiveRadioButton" runat="server" AutoPostBack="true" CssClass="btn radiobutton text-muted" Text="<%$ Resources:lang, InactiveString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="AllRadioButton" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, AllString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
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
                        <asp:GridView ID="BrandMatchingGridView" CssClass="table table-responsive" runat="server" AllowSorting="true"
                            AllowPaging="True" PageSize="10" OnPageIndexChanged="BrandMatchingGridView_PageIndexChanged" OnPageIndexChanging="BrandMatchingGridView_PageIndexChanging"
                            OnRowDataBound="BrandMatchingGridView_RowDataBound"
                            OnRowCommand="BrandMatchingGridView_RowCommand" OnRowCreated="BrandMatchingGridView_RowCreated" OnSorting="BrandMatchingGridView_Sorting">
                            <Columns>
                                <asp:BoundField DataField="Supplier.Name" HeaderText="Supplier" ReadOnly="True"
                                    SortExpression="Supplier.Name" Visible="false" />
                                <asp:TemplateField HeaderText="Supplier" SortExpression="Supplier.Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="SupplierLinkSort" CommandName="Sort" CommandArgument="FornecedorCodigo" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, SupplierString %>">Supplier</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="SupplierLabel" runat="server" Text='<%# string.Format("{0}<p/>{1}",((WhereToBuy.entities.BrandMatching)Container.DataItem).Supplier.Code, 
                                                                                                                                                       ((WhereToBuy.entities.BrandMatching)Container.DataItem).Supplier.Name )%>'></asp:Label>
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
                                        <asp:Label ID="CodeLabel" runat="server" Text='<%# string.Format("{0}<p/>{1}",((WhereToBuy.entities.BrandMatching)Container.DataItem).Code, 
                                                                                                                                                        ((WhereToBuy.entities.BrandMatching)Container.DataItem).Description)%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="25%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Brand" SortExpression="BrandDescription">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="BrandLinkSort" CommandName="Sort" CommandArgument="MapTo" CssClass="btn-link" Font-Bold="true" Text="<%$ Resources:lang, BrandString %>">Brand</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="MapToCodeLabel" runat="server"
                                            Text='<%# ((WhereToBuy.entities.BrandMatching)Container.DataItem).MapTo !=null ? string.Format("[{0}] {1}", ((WhereToBuy.entities.BrandMatching)Container.DataItem).MapTo.Code,
                                                      ((WhereToBuy.entities.BrandMatching)Container.DataItem).MapTo.Description) : string.Empty %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="ActiveHeader" CssClass="text-primary" Text="<%$ Resources:lang, ActiveString %>"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<asp:CheckBox ID="ActiveCheckBox" CssClass=" checkbox-inline " runat="server" Enabled="false" Checked='<%# !((WhereToBuy.entities.BrandMatching)Container.DataItem).Inactive %>' OnCheckedChanged="ActiveCheckBox_CheckedChanged" />--%>
                                        <div style="display: block; text-align: center; vertical-align: middle">
                                            <%# ((bool)DataBinder.Eval(Container, "DataItem.Inactive") == false) ? "&#10004" : ""%>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OpenBrandMatching">
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


