<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatesMatchingUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.States.StatesMatching.StatesMatchingUC" %>
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
                                <asp:Label ID="lblTitle" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, StatesMatchingString %>"></asp:Label>
                            </h2>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-6"></div>
                                <div class="col-lg-6">
                                    <div class="navbar-btn navbar-right">
                                        <h2>
                                            <asp:Button ID="btnNewElement" CssClass="btn btn-link btn-lg left " runat="server" Text="<%$ Resources:lang, NewString %>" OnClick="btnNewElement_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                            <asp:Button ID="btnUpdateElement" CssClass="btn btn-link btn-lg right" runat="server" Text="<%$ Resources:lang, EditString %>" OnClick="btnUpdateElement_Click" CausesValidation="false" UseSubmitBehavior="false"/></h2>
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
                                       <%--Supplier selbox--%>
                                        <uc1:SuppliersSelBox runat="server" ID="SuppliersSelBox" />

                                    </div>
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-5">
                                        <h4>
                                            <asp:Label ID="lblExternalCode" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, ExternalCodeString %>"></asp:Label>
                                        </h4>
                                        <asp:TextBox ID="txtExternalCode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <%-- button--%>
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="btn-group">
                                            <asp:RadioButton ID="btnActive" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, ActiveString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="btnMatching" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, WithoutMatchingString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="btnInactive" runat="server" AutoPostBack="true" CssClass="btn radiobutton text-muted" Text="<%$ Resources:lang, InactiveString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="btnAll" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, AllString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
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
                        <asp:GridView ID="gvStatesMatching" CssClass="table table-responsive" runat="server" AllowSorting="true"
                            AllowPaging="True" PageSize="10" OnPageIndexChanged="gvStatesMatching_PageIndexChanged" OnPageIndexChanging="gvStatesMatching_PageIndexChanging"
                            OnRowDataBound="gvStatesMatching_RowDataBound"
                            OnRowCommand="gvStatesMatching_RowCommand" OnRowCreated="gvStatesMatching_RowCreated" OnSorting="gvStatesMatching_Sorting">

                            <Columns>

                                <asp:BoundField DataField="Supplier.Name" HeaderText="<%$ Resources:lang, SupplierString %>" ReadOnly="True"
                                    SortExpression="Supplier.Name" Visible="false" />
                                <asp:TemplateField HeaderText="<%$ Resources:lang, SupplierString %>" SortExpression="Supplier.Name">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkSupplier" CommandName="Sort" CommandArgument="FornecedorCodigo" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, SupplierString %>">Supplier</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="SupplierLabel" runat="server" Text='<%# string.Format("{0}<p/>{1}",((WhereToBuy.entities.StateMatching)Container.DataItem).Supplier.Code, 
                                                                                                                                                        ((WhereToBuy.entities.StateMatching)Container.DataItem).Supplier.Name )%>'>
                                            </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="25%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code/Description" SortExpression="FirstName">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkCodeDescription" CommandName="Sort" CommandArgument="Codigo" CssClass="btn-link text-muted" Font-Bold="true"><%= Resources.lang.CodeString + "/" + Resources.lang.DescriptionString%></asp:LinkButton>

                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="header" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%# string.Format("{0}<p/>{1}",((WhereToBuy.entities.StateMatching)Container.DataItem).Code, 
                                                                                                                                                        ((WhereToBuy.entities.StateMatching)Container.DataItem).Description)%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="25%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State" SortExpression="StateDescription">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkState" CommandName="Sort" CommandArgument="MapTo" CssClass="btn-link" Font-Bold="true" Text="<%$ Resources:lang, StateString %>">Brand</asp:LinkButton>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblMapToCode" runat="server"
                                            Text='<%# ((WhereToBuy.entities.StateMatching)Container.DataItem).MapTo !=null ? string.Format("[{0}] {1}", ((WhereToBuy.entities.StateMatching)Container.DataItem).MapTo.Code,
                                                      ((WhereToBuy.entities.StateMatching)Container.DataItem).MapTo.Description) : string.Empty %>'></asp:Label>
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
                                        <asp:Label ID="lblSelect" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" style="display:none"><span class="glyphicon glyphicon-open"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSelect" runat="server" CommandName="OpenSelect" CommandArgument = "" ToolTip="<%$ Resources:lang, OpenString %>"><span class="glyphicon glyphicon-open"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />

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
                                    <%-- <asp:Repeater ID="repFooter" OnItemCommand="repFooter_ItemCommand" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:LinkButton ID="lnkPage" Text='<%# Container.DataItem %>' CommandName="Next" CommandArgument="<%# Container.DataItem %>" runat="server" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>--%>
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