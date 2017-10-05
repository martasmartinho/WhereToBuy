<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StatesUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.States.States.StatesUC" %>
   <%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>


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
                                <asp:Label ID="lblTitle" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, StatesString %>"></asp:Label>
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
                                            <asp:Label ID="lblCode" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, CodeString %>"></asp:Label>
                                        </h4>
                                        <asp:TextBox ID="txtCode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-5">
                                        <h4>
                                            <asp:Label ID="lblDescription" runat="server" CssClass="text-muted" Text="<%$ Resources:lang, DescriptionString %>"></asp:Label>
                                        </h4>
                                        <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <%-- button--%>
                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="btn-group">
                                            <asp:RadioButton ID="btnActive" runat="server" AutoPostBack="true" CssClass="btn text-muted" Text="<%$ Resources:lang, ActiveString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
                                            <asp:RadioButton ID="btnInactive" runat="server" AutoPostBack="true" CssClass="btn radiobutton text-muted" Text="<%$ Resources:lang, InactiveString %>" OnCheckedChanged="RadioButton_CheckedChanged" />
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
                        <asp:GridView ID="gvStates" CssClass="table table-responsive" runat="server" AllowSorting="true"
                            AllowPaging="True" PageSize="10" OnPageIndexChanged="gvStates_PageIndexChanged" OnPageIndexChanging="gvStates_PageIndexChanging"
                            OnRowDataBound="gvStates_RowDataBound"
                            OnRowCommand="gvStates_RowCommand" OnRowCreated="gvStates_RowCreated" OnSorting="gvStates_Sorting">

                            <Columns>

                                <asp:BoundField DataField="State.Code" HeaderText="<%$ Resources:lang, CodeString %>" ReadOnly="True"
                                    SortExpression="Supplier.Code" Visible="false" />
                                <asp:TemplateField HeaderText="<%$ Resources:lang, CodeString %>" SortExpression="State.Code">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkCode" CommandName="Sort" CommandArgument="Codigo" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, CodeString %>">Code</asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# (DataBinder.Eval(Container, "DataItem.Code")) %>'>
                                            </asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="25%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:lang, DescriptionString %>" SortExpression="FirstName">
                                    <HeaderTemplate>
                                        <asp:LinkButton runat="server" ID="lnkDescription" CommandName="Sort" CommandArgument="Descricao" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, DescriptionString %>">Description</asp:LinkButton>
                                    </HeaderTemplate>
                                    <HeaderStyle CssClass="header" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# (DataBinder.Eval(Container, "DataItem.Description")) %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="50%" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$ Resources:lang, ActiveString %>">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" ID="lblActive" CssClass="text-primary" Text="<%$ Resources:lang, ActiveString %>"></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <div style="display: block; text-align: center; vertical-align: middle">
                                            <%# ((bool)DataBinder.Eval(Container, "DataItem.Inactive") == false) ? "&#10004" : ""%>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="...">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblSelect" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" Text="<%$ Resources:lang, SelectString %>"></asp:LinkButton>--%>
                                       <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" style="display:none"><span class="glyphicon glyphicon-open"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSelect" runat="server" CommandName="OpenSelect" ToolTip="<%$ Resources:lang, OpenString %>"><span class="glyphicon glyphicon-open"></asp:LinkButton>
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