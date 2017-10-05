<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.Products.Product.ProductUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>

<%--<%@ Register Src="~/UserControls/Suppliers/SuppliersSelBox/SuppliersSelBox.ascx" TagPrefix="uc5" TagName="SuppliersSelBox" %>--%>
<%@ Register Src="~/UserControls/Categories/CategoriesSelBox/CategoriesSelBox.ascx" TagPrefix="uc2" TagName="CategoriesSelBox" %>
<%@ Register Src="~/UserControls/Brands/BrandsSelBox/BrandsSelBox.ascx" TagPrefix="uc3" TagName="BrandsSelBox" %>
<%@ Register Src="~/UserControls/Taxes/TaxSelBox/TaxSelBox.ascx" TagPrefix="uc4" TagName="TaxesSelBox" %>






<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <script type="text/javascript">
            // O METODO AJAX Sys.Application.add_load É EXECUTADO SEMPRE QUE O CONTEUDO DO UPDATEPANEL É
            // CARREGADO PELO BROWSER. ASSIM, SEMPRE QUE EXISTE ATUALIZAÇÃO DO UPDATEPANEL
            // ISTO É EXECUTADO (PRETENDIDO).
            // Sys.Application.add_load => ADICIONA CONTEUDO AO LOAD DA PAGINA
            Sys.Application.add_load(function () {
                $(document).ready(function () {

                    var productPanel = '#' + '<%= pnlProduct.ClientID %>';

                    var pnl1 = '#' + '<%= pnlProduct1.ClientID %>';
                    var pnl2 = '#' + '<%= pnlProduct2.ClientID %>';
                    var pnl3 = '#' + '<%= pnlProduct3.ClientID %>';
                    var pnl4 = '#' + '<%= pnlProduct4.ClientID %>';
                   
                    $(pnl1 + ' .spinner .btn:first-of-type').on('click', function () {
                        $(pnl1 + ' .spinner input').val((parseInt($(pnl1 + ' .spinner input').val()) + 1));

                    });
                    $(pnl1 + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseInt($(pnl1 + ' .spinner input').val()) > 0) {
                            $(pnl1 + ' .spinner input').val((($(pnl1 + ' .spinner input').val()) - 1));
                        }


                    });

                    //####

                    $(pnl2 + ' .spinner .btn:first-of-type').on('click', function () {
                        $(pnl2 + ' .spinner input').val((parseInt($(pnl2 + ' .spinner input').val()) + 1));

                    });
                    $(pnl2 + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseInt($(pnl2 + ' .spinner input').val()) > 0) {
                            $(pnl2 + ' .spinner input').val((($(pnl2 + ' .spinner input').val()) - 1));
                        }


                    });

                    //####

                    $(pnl3 + ' .spinner .btn:first-of-type').on('click', function () {
                        $(pnl3 + ' .spinner input').val((parseInt($(pnl3 + ' .spinner input').val()) + 1));

                    });
                    $(pnl3 + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseInt($(pnl3 + ' .spinner input').val()) > 0) {
                            $(pnl3 + ' .spinner input').val((($(pnl3 + ' .spinner input').val()) - 1));
                        }


                    });


                    //####


                    $(pnl4 + ' .spinner .btn:first-of-type').on('click', function () {
                        $(pnl4 + ' .spinner input').val((parseInt($(pnl4 + ' .spinner input').val()) + 1));

                    });
                    $(pnl4 + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseInt($(pnl4 + ' .spinner input').val()) > 0) {
                            $(pnl4 + ' .spinner input').val((($(pnl4 + ' .spinner input').val()) - 1));
                        }


                    });


                    $("#btnShowDetails").click(function () {
                        $("#btnShowDetails").hide();
                        $("#btnHideDetails").show();
                        $("#DetailsGrid").show();
                        
                    });
                    $("#btnHideDetails").click(function () {
                        $("#btnShowDetails").show();
                        $("#btnHideDetails").hide();
                        $("#DetailsGrid").hide();
                    });

                    $("#btnMoreInfo1").click(function () {
                        $("#btnMoreInfo1").hide();
                        $("#btnMoreInfo2").show();
                        $("#MoreInfoDiv").show();

                    });
                    $("#btnMoreInfo2").click(function () {
                        $("#btnMoreInfo1").show();
                        $("#btnMoreInfo2").hide();
                        $("#MoreInfoDiv").hide();
                    });

                   

                });
            });

        </script>

        <asp:Panel ID="pnlProduct" runat="server" CssClass="container-fluid">

            <div class="col-lg-1"></div>

            <div class="col-lg-10">

                <%--title row--%>
                <div class="row">
                    <div class="col-sm-4 col-md-4 col-lg-4">
                        <h2>
                            <asp:Label ID="lblTitle" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, ProductString %>"></asp:Label>
                        </h2>
                    </div>
                    <div class="col-sm-8 col-md-8 col-lg-8">
                        <div class="row">
                            <div class="col-xs-6"></div>
                            <div class="col-xs-6">
                                <div class="navbar-btn navbar-right">
                                    <h2>
                                        <asp:Button ID="btnNew" CssClass="btn btn-link btn-lg left " runat="server" Text="<%$ Resources:lang, NewString %>" OnClick="btnNew_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnDelete" CssClass="btn btn-link btn-lg right" runat="server" Text="<%$ Resources:lang, DeleteString %>" OnClick="btnDelete_Click" /></h2>
                                </div>

                            </div>

                        </div>
                    </div>

                </div>

                <%--body--%>
                <div class="row">
                    <div class="col-sm-2 col-md-1 col-lg-1"></div>


                    <div class="col-sm-8 col-md-10 col-lg-10">

                        <%--Information Data--%>
                        <div class="row">
                            <h4><u><strong>
                                <asp:Label ID="lblIdentification" runat="server" Text="<%$ Resources:lang, GeneralDataString %>"></asp:Label></strong></u></h4>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">
                                <%--Code--%>
                                <div class="form-group">
                                    <asp:Label ID="lblCode" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, CodeString %>"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control left" />
                                    </div>
                                    <div class="col-xs-4 col-sm-2">
                                        <asp:LinkButton ID="lnkCodeSearch" runat="server" CssClass="btn btn-primary dropdown-toggle left" OnClick="lnkCodeSearch_Click" ToolTip="<%$ Resources:lang, LoadString %>"><span class="glyphicon glyphicon-download-alt"></span></asp:LinkButton>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <%--Partnumber--%>
                                <div class="form-group">
                                    <asp:Label ID="lblPartnumber" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, PartnumberString %>"></asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <asp:TextBox ID="txtPartnumber" runat="server" CssClass="form-control left" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <%--Supplier--%>
                                <%--<div class="form-group">
                                    <asp:Label ID="Label7" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, SupplierString %>"></asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <uc5:SuppliersSelBox runat="server" ID="SuppliersSelBox" />
                                    </div>
                                </div>
                                <br />
                                <br />--%>
                                <%--brand--%>
                                <div class="form-group">
                                    <asp:Label ID="Label13" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, BrandString %>"></asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <uc3:BrandsSelBox runat="server" ID="BrandsSelBox" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <%--category--%>
                                <div class="form-group">
                                    <asp:Label ID="Label5" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, CategoryString %>"></asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <uc2:CategoriesSelBox runat="server" ID="CategoriesSelBox" />
                                    </div>
                                </div>
                                <br />
                                <br />

                                <%--Tax--%>
                                <div class="form-group">
                                    <asp:Label ID="Label9" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, TaxString %>"></asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <uc4:TaxesSelBox runat="server" ID="TaxesSelBox" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />

                               <%-- buttons--%>
                               <%-- <div class="row">
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-10">
                                        <div class="control-form navbar-right">
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary left" Text="<%$ Resources:lang, OkString %>" OnClick="btnOk_Click" />
                                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary right" Text="<%$ Resources:lang, CancelString %>" OnClick="btnCancel_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                        </div>

                                    </div>

                                    <div class="col-lg-1"></div>

                                </div>--%>
                            </div>



                            <div class="col-lg-1"></div>

                        </div>

                        <br />
                        <hr>

                        <%--Product Details--%>
                        <div class="row">
                            <h4><u><strong>
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:lang, ActiveExternalInformationString %>"></asp:Label></strong></u></h4>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">
                                <%-- current detail--%>
                                <div class="row">
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th style="width: 30%;"><%= Resources.lang.CodeString%></th>
                                                <th style="width: 50%;"><%= Resources.lang.ContentString%></th>
                                                <th style="width: 10%;"><%= Resources.lang.ScoreString%></th>
                                                <th style="width: 10%;"><%= Resources.lang.ActiveString%></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <%--Description--%>
                                            <tr>
                                                <td><%= Resources.lang.DescriptionString%></td>
                                                <td>
                                                    <asp:TextBox ID="txtDetailDescription" runat="server" CssClass="form-control left" />
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlProduct1" runat="server" CssClass="form-group">
                                                        <div class="input-group spinner" style="width: 100%">
                                                            <asp:TextBox ID="txtDescriprionScore" runat="server" Enabled="false" CssClass="form-control" Text="0"/>
                                                            <div class="input-group-btn-vertical">
                                                                <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                                <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </td>
                                                <td style="align-content:center">
                                                        <asp:CheckBox ID="cbxDescriptionActive" runat="server" />
                                                </td>
                                            </tr>

                                            <%--Link--%>
                                            <tr>
                                                <td><%= Resources.lang.LinkString%></td>
                                                <td>
                                                    <asp:TextBox ID="txtDetailLink" runat="server" CssClass="form-control left" />
                                                </td>
                                                <td>
                                                    <asp:Panel ID="pnlProduct2" runat="server" CssClass="form-group">
                                                        <div class="input-group spinner" style="width: 100%">
                                                        <asp:TextBox ID="txtLinkScore" runat="server" Enabled="false" CssClass="form-control" Text="0" />
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                            <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                        </div>

                                                    </div>
                                                    </asp:Panel>
                                                    
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbxLinkActive" runat="server" />
                                                </td>
                                            </tr>

                                            <%--Image--%>
                                            <tr>
                                                <td><%= Resources.lang.ImageString%></td>
                                                <td>
                                                    <asp:TextBox ID="txtDetailImage" runat="server" CssClass="form-control left" />
                                                </td>
                                                <td>
                                                     <asp:Panel ID="pnlProduct3" runat="server" CssClass="form-group">
                                                         <div class="input-group spinner" style="width: 100%">
                                                        <asp:TextBox ID="txtImageScore" runat="server" Enabled="false" CssClass="form-control" Text="0" />
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                            <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                        </div>

                                                    </div>
                                                     </asp:Panel>
                                                    
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbxImageActive" runat="server" />
                                                </td>
                                            </tr>

                                            <%--Features--%>
                                            <tr>
                                                <td><%= Resources.lang.FeaturesString%></td>
                                                <td>
                                                    <asp:TextBox ID="txtDeatailFeatures" runat="server" CssClass="form-control" TextMode="multiline" Rows="5" />
                                                </td>
                                                <td>
                                                     <asp:Panel ID="pnlProduct4" runat="server" CssClass="form-group">
                                                         <div class="input-group spinner" style="width: 100%">
                                                        <asp:TextBox ID="txtFeaturesScore" runat="server" Enabled="false" CssClass="form-control" Text="0"/>
                                                        <div class="input-group-btn-vertical">
                                                            <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                            <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                        </div>

                                                    </div>
                                                     </asp:Panel>
                                                    
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cbxFeaturesActive" runat="server"/>
                                                </td>
                                            </tr>

                                        </tbody>
                                    </table>
                                    <br />
                                    <br />

                                    <%-- buttons--%>
                                    <div class="row">
                                        <div class="col-lg-1"></div>
                                        <div class="col-lg-10">
                                            <div class="control-form navbar-right">
                                                <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary left" Text="<%$ Resources:lang, OkString %>" OnClick="btnOk_Click" />
                                                <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary right" Text="<%$ Resources:lang, CancelString %>" OnClick="btnCancel_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                            </div>

                                        </div>

                                        <div class="col-lg-1"></div>

                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                 <br />
                                 <br />
                                  
                                <%--details grid--%>
                                <div class="row">
                                    <button id="btnShowDetails" class="glyphicon glyphicon-eye-open pull-right" type="button" style="background: transparent; border-style: none"></button>
                                    <button id="btnHideDetails" class="glyphicon glyphicon-eye-close pull-right" type="button" style="background: transparent; border-style: none; display: none"></button>
                                    <div id="DetailsGrid" style="display: none; min-height: 100px">
                                        <br />
                                        <br />
                                        <asp:GridView ID="gvDetails" CssClass="table table-responsive" runat="server" AllowSorting="true"
                                            AllowPaging="True" PageSize="10" OnPageIndexChanged="gvDetails_PageIndexChanged" OnPageIndexChanging="gvDetails_PageIndexChanging"
                                            OnRowDataBound="gvDetails_RowDataBound" OnRowCommand="gvDetails_RowCommand" OnRowCreated="gvDetails_RowCreated" OnSorting="gvDetails_Sorting">
                                            <Columns>
                                                <asp:BoundField DataField="ProductDetail.ContentConcernIndex" HeaderText="<%$ Resources:lang, CodeString %>" ReadOnly="True"
                                                    SortExpression="ProductDetail.ContentConcernIndex" Visible="false" />
                                                <asp:TemplateField HeaderText="<%$ Resources:lang, CodeString %>" SortExpression="Brand.Code">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkCode" CommandName="Sort" CommandArgument="Codigo" CssClass="btn-link text-muted" Font-Bold="true" Text="<%$ Resources:lang, CodeString %>">Code</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div>
                                                            <div class="row">
                                                                <asp:Label ID="lblCode" runat="server" Text='Teste'>
                                                                </asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <table class="table table-bordered">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 30%;"><%= Resources.lang.CodeString%></th>
                                                                            <th style="width: 50%;"><%= Resources.lang.ContentString%></th>
                                                                            <th style="width: 10%;"><%= Resources.lang.ScoreString%></th>
                                                                            <th style="width: 10%;"><%= Resources.lang.ActiveString%></th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <%--Description--%>
                                                                        <tr>
                                                                            <td>%><%= Resources.lang.DescriptionString%></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.Description") %></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.DescriptionScore") %></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.IsDescriptionDisable") %></td>
                                                                        </tr>
                                                                        <%--Link--%>
                                                                        <tr>
                                                                            <td><%= Resources.lang.LinkString%></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.Link") %></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.LinkScore") %></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.IsLinkDisable") %></td>
                                                                        </tr>
                                                                        <%--Image--%>
                                                                        <tr>
                                                                            <td><%= Resources.lang.ImageString%></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.Link") %></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.LinkScore") %></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.IsLinkDisable") %></td>
                                                                        </tr>

                                                                        <%--Features--%>
                                                                        <tr>
                                                                            <td><%= Resources.lang.FeaturesString%></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.Features") %></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.FeaturesScore") %></td>
                                                                            <td><%# DataBinder.Eval(Container, "DataItem.IsFeaturesDisable") %></td>
                                                                        </tr>

                                                                    </tbody>
                                                                </table>
                                                            </div>

                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="25%" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="...">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblSelect" runat="server" Text=""></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" Style="display: none"><span class="glyphicon glyphicon-open"></asp:LinkButton>
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
                                                            <asp:Label ID="lblActualPage" runat="server" CssClass="btn-primary" BackColor="#428bca"></asp:Label></li>
                                                        <li class="previous">
                                                            <asp:LinkButton ID="btnForward" runat="server" CommandName="Forward" OnClick="btnPage_Click"><%= Resources.lang.ForwardString%></asp:LinkButton></li>
                                                    </ul>
                                                </asp:Panel>
                                            </PagerTemplate>
                                            <EmptyDataTemplate>
                                                não existem registos
                                            </EmptyDataTemplate>
                                            <FooterStyle Height="100%" />
                                            <SelectedRowStyle CssClass="info" />
                                        </asp:GridView>
                                    </div>

                                </div>

                            </div>

                            <div class="col-lg-1"></div>

                        </div>
                        <br />
                        <hr>


                        <%--Others--%>
                        <div class="row">
                            <h4><u><strong>
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:lang, OthersString %>"></asp:Label></strong></u></h4>
                            <div class="col-lg-1"></div>

                            <div class="col-lg-10">

                                <div class="form-inline">
                                    <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang, DiscontinuedString %>"></asp:Label>
                                    <asp:CheckBox ID="cbxDiscontinued" runat="server" CssClass="checkbox" type="checkbox" />
                                </div>

                                <br/>

                                <div class="form-inline">
                                    <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang, InactiveString %>"></asp:Label>
                                    <asp:CheckBox ID="cbxInactive" runat="server" CssClass="checkbox" type="checkbox" />
                                </div>

                                <br/>

                                <div>
                                    <div class="form-inline">
                                        <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang, CreationString %>"></asp:Label>
                                        <asp:Label ID="lblCreation" runat="server" CssClass="text-primary" />
                                    </div>
                                    <br />
                                    <div class="form-inline">
                                        <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang, VersionString %>"></asp:Label>
                                        <asp:Label ID="lblVersion" runat="server" CssClass="text-primary" />
                                    </div>
                                    <br />
                                    <div class="form-inline">
                                        <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang, ModeString %>"></asp:Label>
                                        <asp:Label ID="lblMode" runat="server" CssClass="text-primary" Text="(Alteração/Inserção)" />
                                    </div>
                                </div>


                            </div>

                            <div class="col-lg-1"></div>



                            <br />


                            <%-- buttons--%>
                            <div class="row">
                                <div class="col-lg-1"></div>
                                <div class="col-lg-10">
                                    <div class="control-form navbar-right">
                                        <asp:Button ID="btnOk" runat="server" CssClass="btn btn-primary left" Text="<%$ Resources:lang, OkString %>" OnClick="btnOk_Click" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary right" Text="<%$ Resources:lang, CancelString %>" OnClick="btnCancel_Click" CausesValidation="false" UseSubmitBehavior="false" />
                                    </div>

                                </div>
                                <div class="col-lg-1"></div>

                            </div>

                        </div>

                         <br />


                        <%--More Information Data--%>
                        <div class="row">

                            <div class="row">
                                <div class="col-xs-6"></div>
                                <div class="col-xs-6">
                                    <div class="navbar-btn navbar-right">
                                        <h2>
                                            <button id="btnMoreInfo1" class="btn btn-link btn-lg right" type="button" style="background: transparent; border-style: none"><%= Resources.lang.MoreInfoString%></button>
                                            <button id="btnMoreInfo2" class="btn btn-link btn-lg right" type="button" style="background: transparent; border-style: none; display: none"><%= Resources.lang.MoreInfoString%></button>

                                            <%--<asp:Button ID="btnMoreInfo" CssClass="btn btn-link btn-lg right" runat="server" Text="<%$ Resources:lang, OkString %>" UseSubmitBehavior="false"/>--%>

                                        </h2>
                                    </div>

                                </div>

                            </div>

                            <div id="MoreInfoDiv" style="display: none;">
                                <br />
                                <hr>
                                <%--Commercial Data--%>
                                <div class="row">
                                    <h4><u><strong>
                                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:lang, CommercialDataString %>"></asp:Label></strong></u></h4>
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-10">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, SupplierString %>"></asp:Label>
                                            <div class="col-sm-10">
                                                <asp:Label ID="lblSupplier" runat="server" CssClass="left" />
                                            </div>
                                        </div>
                                        <br />
                                        <br />

                                        <div class="form-group">
                                            <asp:Label ID="Label11" CssClass="control-label col-sm-2 col-md-2" runat="server" Text="<%$ Resources:lang, DescriptionString %>"></asp:Label>
                                            <div class="col-sm-10 col-md-10">
                                                <asp:Label ID="lblDescription" runat="server" BorderColor="Transparent" BorderStyle="None" BackColor="Transparent" BorderWidth="0" CssClass="left"/>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <%--Stock information--%>
                                        <div class="form-group">
                                            <asp:Label ID="Label12" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, StockString %>"></asp:Label>
                                            <div class="col-sm-10">
                                                <table class="table table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>n - 2</th>
                                                            <th>n - 1</th>
                                                            <th>n</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <%--Description--%>
                                                        <tr>
                                                            <td style="width:33.33%;">
                                                                <div class="col-sm-10 col-md-10">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblStockDaysU2" CssClass="control-label pull-left" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblStockDeltaU2" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <h5>
                                                                        <strong>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblStockDateU2" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblStockDescriptionU2" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                        </strong>
                                                                    </h5>
                                                                    
                                                                    <div class="row">
                                                                        <div class="col-sm-12">

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2 col-md-2">
                                                                    <asp:Label ID="lblStockU2Up" runat="server" CssClass="glyphicon glyphicon-arrow-up"></asp:Label>
                                                                    <asp:Label ID="lblStockU2Down" runat="server" CssClass="glyphicon glyphicon-arrow-down"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td style="width:33.33%;">
                                                                <div class="col-sm-10">
                                                                        <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblStockDaysU1" CssClass="control-label pull-left" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblStockDeltaU1" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <h5>
                                                                        <strong>
                                                                            <div class="row right">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblStockDateU1" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblStockDescriptionU1" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                        </strong>
                                                                    </h5>
                                                                    
                                                                    <div class="row">

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:Label ID="lblStockU1Up" runat="server" CssClass="glyphicon glyphicon-arrow-up"></asp:Label>
                                                                    <asp:Label ID="lblStockU1Down" runat="server" CssClass="glyphicon glyphicon-arrow-down"></asp:Label>
                                                                </div>

                                                            </td>
                                                            <td style="width:33.33%;"">
                                                                <div class="col-sm-10">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblStockDays" CssClass="control-label pull-left" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblStockDelta" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <h5>
                                                                        <strong>
                                                                            <div class="row right">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblStockDate" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblStockDescription" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </strong>
                                                                    </h5>
                                                                    <div class="row">

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2" >
                                                                    <asp:Label ID="lblStockUp" runat="server" CssClass="glyphicon glyphicon-arrow-up"></asp:Label>
                                                                    <asp:Label ID="lblStockDown" runat="server" CssClass="glyphicon glyphicon-arrow-down"></asp:Label>
                                                                </div>
                                                            </td>

                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <%--Price Cost information--%>
                                        <div class="form-group">
                                            <asp:Label ID="Label14" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, PriceString %>"></asp:Label>
                                            <div class="col-sm-10">
                                                <table class="table table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>n - 2</th>
                                                            <th>n - 1</th>
                                                            <th>n</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <%--Description--%>
                                                        <tr>
                                                            <td style="width:33.33%;">
                                                                <div class="col-sm-10 col-md-10">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblCostPriceDaysU2" CssClass="control-label pull-left" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblCostPriceDeltaU2" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <h5>
                                                                        <strong>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblCostPriceDateU2" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblCostPriceU2" CssClass="control-label pull-right" runat="server" Text="0,00"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                        </strong>
                                                                    </h5>

                                                                    <div class="row">

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2 col-md-2">
                                                                    <asp:Label ID="lblCostPriceU2Up" runat="server" CssClass="glyphicon glyphicon-arrow-up"></asp:Label>
                                                                    <asp:Label ID="lblCostPriceU2Down" runat="server" CssClass="glyphicon glyphicon-arrow-down"></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td style="width:33.33%;">
                                                                <div class="col-sm-10">
                                                                    <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblCostPriceDaysU1" CssClass="control-label pull-left" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblCostPriceDeltaU1" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <h5>
                                                                        <strong>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblCostPriceDateU1" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-sm-12">
                                                                                    <asp:Label ID="lblCostPriceU1" CssClass="control-label pull-right" runat="server" Text="0,00"></asp:Label>
                                                                                </div>

                                                                            </div>
                                                                        </strong>
                                                                    </h5>
                                                                 
                                                                    <div class="row">

                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:Label ID="lblCostPriceU1Up" runat="server" CssClass="glyphicon glyphicon-arrow-up"></asp:Label>
                                                                    <asp:Label ID="lblCostPriceU1Down" runat="server" CssClass="glyphicon glyphicon-arrow-down"></asp:Label>
                                                                </div>

                                                            </td>
                                                            <td style="width:33.33%;"">
                                                                <div class="col-sm-10">
                                                                         <div class="row">
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblCostPriceDays" CssClass="control-label pull-left" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <asp:Label ID="lblCostPriceDelta" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <h5><strong>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <asp:Label ID="lblCostPriceDate" CssClass="control-label pull-right" runat="server" Text="Default"></asp:Label>
                                                                        </div>
                                                                            
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-sm-12">
                                                                                <asp:Label ID="lblCostPrice" CssClass="control-label pull-right" runat="server" Text="0,00"></asp:Label>
                                                                        </div>
                                                                            
                                                                        </div>
                                                                        <div class="row">
                                                                        </div>
                                                                    </strong></h5>
                                                                </div>
                                                                <div class="col-sm-2" >
                                                                    <asp:Label ID="lblCostPriceUp" runat="server" CssClass="glyphicon glyphicon-arrow-up"></asp:Label>
                                                                    <asp:Label ID="lblCostPriceDown" runat="server" CssClass="glyphicon glyphicon-arrow-down"></asp:Label>
                                                                </div>
                                                            </td>

                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-lg-1"></div>

                                </div>


                                <br />
                                <hr>

                                <%--statistic Data--%>
                                <div class="row">
                                    <h4><u><strong>
                                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:lang, StatisticsString %>"></asp:Label></strong></u></h4>
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-10">
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-sm-2">
                                                    <a runat="server" href="#" data-toggle="tooltip" title='<%# this.GetGlobalResourceObject("lang", "EEPTextString") %>'><%--traduzir--%>
                                                        <span class="glyphicon glyphicon-info-sign"></span>
                                                    </a>
                                                    <asp:Label ID="Label15" CssClass="control-label" runat="server" Text="<%$ Resources:lang, EEPString %>"></asp:Label>
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="lblEEP" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="row">

                                            <div class="form-group">
                                                <div class="col-sm-2">
                                                    <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, ICPTextString %>'><%--traduzir--%>
                                                        <span class="glyphicon glyphicon-info-sign"></span>
                                                    </a>
                                                    <asp:Label ID="Label26" CssClass="control-label" runat="server" Text="<%$ Resources:lang, ICPString %>"></asp:Label>
                                                </div>
                                                <div class="col-sm-10">
                                                </div>
                                            </div>

                                        </div>
                                             <br />
                                            <br />
                                         <div class="row">
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-11">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                                <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, CETextString %>'><%--traduzir--%>
                                                                    <span class="glyphicon glyphicon-info-sign"></span>
                                                                </a>
                                                                <asp:Label ID="lblICPCE" CssClass="control-label" runat="server" Text="CEString"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label ID="lblCEPrice" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                                <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, CTTextString %>'><%--traduzir--%>
                                                                    <span class="glyphicon glyphicon-info-sign"></span>
                                                                </a>
                                                                <asp:Label ID="Label20" CssClass="control-label" runat="server" Text="CTString"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label ID="lblCTPrice" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                                <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang,CFTextString %>'><%--traduzir--%>
                                                                    <span class="glyphicon glyphicon-info-sign"></span>
                                                                </a>
                                                                <asp:Label ID="Label22" CssClass="control-label" runat="server" Text="CFString"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label ID="lblCFPrice" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                                <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, ICPTextString %>'><%--traduzir--%>
                                                                    <span class="glyphicon glyphicon-info-sign"></span>
                                                                </a>
                                                                <asp:Label ID="Label24" CssClass="control-label" runat="server" Text="ICPString"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label ID="lblICP" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="form-group">
                                                <div class="col-sm-2">
                                                    <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, EEDTextString %>'><%--traduzir--%>
                                                        <span class="glyphicon glyphicon-info-sign"></span>
                                                    </a>
                                                    <asp:Label ID="lblEED" CssClass="control-label" runat="server" Text="<%$ Resources:lang, EEDString %>"></asp:Label>
                                                </div>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="Label19" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                         <div class="row">

                                            <div class="form-group">
                                                <div class="col-sm-2">
                                                    <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, ICDTextString %>'><%--traduzir--%>
                                                        <span class="glyphicon glyphicon-info-sign"></span>
                                                    </a>
                                                    <asp:Label ID="Label18" CssClass="control-label" runat="server" Text="<%$ Resources:lang, ICDString %>"></asp:Label>
                                                </div>
                                                <div class="col-sm-10">
                                                </div>
                                            </div>
                                        </div>
                                             <br />
                                            <br />
                                         <div class="row">
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-11">
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                                <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, CETextString %>'><%--traduzir--%>
                                                                    <span class="glyphicon glyphicon-info-sign"></span>
                                                                </a>
                                                                <asp:Label ID="lblCE" CssClass="control-label" runat="server" Text="<%$ Resources:lang, CEString %>"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label ID="lblCEStock" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                                <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, CTTextString %>'><%--traduzir--%>
                                                                    <span class="glyphicon glyphicon-info-sign"></span>
                                                                </a>
                                                                <asp:Label ID="lblCT" CssClass="control-label" runat="server" Text="<%$ Resources:lang, CTString %>"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label ID="lblCTStock" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                                <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, CFTextString %>'><%--traduzir--%>
                                                                    <span class="glyphicon glyphicon-info-sign"></span>
                                                                </a>
                                                                <asp:Label ID="lblCF" CssClass="control-label" runat="server" Text="<%$ Resources:lang, CFString %>"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label ID="lblCFStock" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <div class="col-sm-2">
                                                                <a runat="server" href="#" data-toggle="tooltip" title='<%$ Resources:lang, ICDTextString %>'><%--traduzir--%>
                                                                    <span class="glyphicon glyphicon-info-sign"></span>
                                                                </a>
                                                                <asp:Label ID="Label30" CssClass="control-label" runat="server" Text="<%$ Resources:lang, ICDString %>"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-10">
                                                                <asp:Label ID="lblICD" runat="server" Text="gdhsa jhsgjhaa, jhsdjhahgj, hjhsja, jsahadj" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                    </div>

                                    <div class="col-lg-1"></div>

                                </div>


                                <br />
                                <hr>

                                <%--Atual Quotation--%>
                                <div class="row">
                                    <h4><u><strong>
                                        <asp:Label ID="Label6" runat="server" Text="<%$Resources:lang, CurrentQuotationString%>"></asp:Label></strong></u></h4>
                                    <div class="col-lg-1"></div>

                                    <div class="col-lg-10 form-control pull-right" style="min-height: 150px;  max-width:90%; margin-right:5%;">
                                        <%-- <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="multiline" Rows="5" Enabled="false">

                                         </asp:TextBox>--%>
                                        <asp:ListView runat="server" ID="lvCurrentQuotation" >
                                            <LayoutTemplate>
                                                <table runat="server" id="table1">
                                                    <tr runat="server" id="itemPlaceholder"></tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <%-- Data-bound content. --%>
                                                        <asp:Label ID="lblLabel" runat="server"
                                                            Text='<%#((WhereToBuy.entities.Quotation)Container.DataItem).ToString()%>'/>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="col-lg-1"></div>
                                </div>
                                <br />
                                <hr>

                                <%--Historical Quotation--%>
                                <div class="row">
                                    <h4><u><strong>
                                        <asp:Label ID="Label7" runat="server" Text="<%$Resources:lang, CurrentQuotationString%>"></asp:Label></strong></u></h4>
                                    <div class="col-lg-1"></div>

                                    <div class="col-lg-10 form-control pull-right" style="min-height: 150px;  max-width:90%; margin-right:5%;">
                                        <%-- <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="multiline" Rows="5" Enabled="false">

                                         </asp:TextBox>--%>
                                        <asp:ListView runat="server" ID="ListView1" >
                                            <LayoutTemplate>
                                                <table runat="server" id="table1">
                                                    <tr runat="server" id="itemPlaceholder"></tr>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr runat="server">
                                                    <td runat="server">
                                                        <%-- Data-bound content. --%>
                                                        <asp:Label ID="lblLabel" runat="server"
                                                            Text='<%#((WhereToBuy.entities.Quotation)Container.DataItem).ToString()%>'/>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                    <div class="col-lg-1"></div>
                                </div>
                            </div>


                        </div>




                        <br />
                        <hr>
                    </div>

                    <div class="col-sm-2 col-md-1 col-lg-1"></div>

                </div>



            </div>

            <div class="col-md-1 col-lg-1"></div>

        </asp:Panel>


    </ContentTemplate>
</asp:UpdatePanel>

<uc1:MessageUC runat="server" ID="MessageUC" />
