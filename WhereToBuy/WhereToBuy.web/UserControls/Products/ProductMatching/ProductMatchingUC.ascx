<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductMatchingUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.Products.ProductMatching.ProductMatchingUC" %>

<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>
<%@ Register Src="~/UserControls/Suppliers/SuppliersSelBox/SuppliersSelBox.ascx" TagPrefix="uc1" TagName="SuppliersSelBox" %>
<%@ Register Src="~/UserControls/Products/ProductsSelBox/ProductsSelBox.ascx" TagPrefix="uc2" TagName="ProductsSelBox" %>
<%@ Register Src="~/UserControls/Stocks/StocksSelBox/StocksSelBox.ascx" TagPrefix="uc3" TagName="StocksSelBox" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
         <script type="text/javascript">
            // O METODO AJAX Sys.Application.add_load É EXECUTADO SEMPRE QUE O CONTEUDO DO UPDATEPANEL É
            // CARREGADO PELO BROWSER. ASSIM, SEMPRE QUE EXISTE ATUALIZAÇÃO DO UPDATEPANEL
            // ISTO É EXECUTADO (PRETENDIDO).
            // Sys.Application.add_load => ADICIONA CONTEUDO AO LOAD DA PAGINA
            Sys.Application.add_load(function () {
                $(document).ready(function () {
                    var pmPanel = '#' + '<%= pnlProductMatching.ClientID %>';

                    $(pmPanel + ' .spinner .btn:first-of-type').on('click', function () {
                        $(pmPanel + ' .spinner input').val((parseInt($(pmPanel + ' .spinner input').val()) + 1));

                    });
                    $(pmPanel + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseInt($(pmPanel + ' .spinner input').val()) > 0) {
                            $(pmPanel + ' .spinner input').val((($(pmPanel + ' .spinner input').val()) - 1));
                        }


                    });

                  

                });
            });

        </script>

        <asp:Panel ID="pnlProductMatching" runat="server" CssClass="container-fluid">
                <div class="col-lg-1"></div>
                <div class="col-lg-10">
                    <%--title row--%>
                    <div class="row">
                        <div class="col-lg-6">
                            <h2>
                                <asp:Label ID="TitleLabel" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, ProductMatchingString %>"></asp:Label>
                            </h2>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-6"></div>
                                <div class="col-lg-6">
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
                        <div class="col-xs-2 col-sm-2 col-lg-1"></div>
                        <div class="col-xs-8 col-sm-8 col-lg-10">
                            <%--supplier--%>
                            <div class="row">
                                <div class="col-xs-8 col-sm-8 col-lg-6">
                                    <%--supplier--%>
                                    <div>
                                        <asp:Label ID="lblSupplier" runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, SupplierString %>"></asp:Label>
                                        <div class="col-sm-10">
                                            <uc1:SuppliersSelBox runat="server" ID="SuppliersSelBox" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xs-4 col-sm-4 col-lg-6"></div>
                            </div>
                            <br>
                            <br />
                            <hr>
                            <%--external information--%>
                            <div class="row">
                                <h4><u><strong>
                                    <asp:Label ID="lblExternalInformation" runat="server" Text="<%$ Resources:lang, ExternalInformationString %>"></asp:Label></strong></u></h4>
                                <div class="col-lg-1"></div>
                                <div class="col-lg-10">
                                    <div>
                                        <div class="form-group">
                                            <asp:Label ID="lblCode" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, CodeString %>"></asp:Label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-control left" />
                                            </div>
                                            <div class="col-sm-6">
                                                <%--<asp:LinkButton ID="lnkCodeSearch" runat="server" CssClass="btn btn-primary dropdown-toggle left" OnClick="lnkCodeSearch_Click" ToolTip="<%$ Resources:lang, LoadString %>"><span class="glyphicon glyphicon-download-alt" ></span></asp:LinkButton>--%>
                                            </div>
                                        </div>
                                        <br>
                                        <br />
                                        <br>
                                        <br />
                                        <div class="form-group">
                                            <asp:Label ID="lblSupplement" runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, SupplementString %>"></asp:Label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtSupplement" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:LinkButton ID="lnkCodeSearch" runat="server" CssClass="btn btn-primary dropdown-toggle left" OnClick="lnkCodeSearch_Click" ToolTip="<%$ Resources:lang, LoadString %>"><span class="glyphicon glyphicon-download-alt" ></span></asp:LinkButton>
                                            </div>
                                        </div>
                                        
                                    </div>
                                </div>
                                <div class="col-lg-1"></div>
                            </div>
                            <br>
                            <br />
                            <hr>
                            <%--internal information--%>
                            <div class="row">
                                <h4><u><strong>
                                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:lang, InternalInformationString %>"></asp:Label></strong></u></h4>
                                <div class="col-lg-1"></div>
                                <div class="col-lg-10">
                                    <div>
                                        <div class="form-group">
                                            <asp:Label ID="Label2" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, ProductString %>"></asp:Label>
                                            <div class="col-sm-8">
                                                <uc2:ProductsSelBox runat="server" ID="ProductsSelBox" />
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-1"></div>
                            </div>
                            <br>
                            <br />
                            <hr>

                            <%--Rules Data--%>
                            <div class="row">
                                <h4><u><strong>
                                    <asp:Label ID="Label4" runat="server" Text="<%$ Resources:lang, RulesString %>"></asp:Label></strong></u></h4>
                                <div class="col-lg-1"></div>
                                <div class="col-lg-10">


                                    <div>

                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="control-label col-sm-2" data-toggle="popover" title="Popover Header"
                                            data-content="Some content inside the popover" Text="<%$ Resources:lang, ExpirationString%>"></asp:HyperLink>


                                        <div class="col-sm-10">
                                            <div class="input-group spinner">
                                                <asp:TextBox ID="txtExpiration" runat="server" type="text" CssClass="form-control" Enabled="false" />
                                                <div class="input-group-btn-vertical">
                                                    <button id="btnRuleUp" class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button id="btnRuleDown" class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                    <br />
                                    <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="control-label col-sm-2" data-toggle="popover" title="Popover Header"
                                            data-content="Some content inside the popover" Text="<%$ Resources:lang, SubstituteStockString %>"></asp:HyperLink>

                                        <div class="col-sm-8">
                                            <uc3:StocksSelBox runat="server" ID="StocksSelBox" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>

                                    <br />
                                    <br />
                                    <div class="form-group">
                                        <asp:HyperLink ID="HyperLink3" runat="server" CssClass="control-label col-sm-2" data-toggle="popover" title="Popover Header"
                                            data-content="Some content inside the popover" Text="<%$ Resources:lang, DataResetString %>"></asp:HyperLink>
                                        <div class="col-sm-3">
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <div id="drpDataReset" class="input-group dropdown">
                                                        <asp:TextBox ID="txtDataReset" runat="server" CssClass="form-control countrycode dropdown-toggle" Text="" Enabled="false"></asp:TextBox>
                                                        <div class="dropdown-menu">
                                                            <asp:ListView ID="lvDataReset" runat="server">
                                                                <LayoutTemplate>
                                                                    <table>
                                                                        <tr runat="server" id="itemPlaceholder"></tr>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td id='<%# Container.DataItemIndex %>'>
                                                                            <asp:LinkButton ID="SelectedItemButton" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Key") %>' CssClass="btn btn-link" ForeColor="#428bca" Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>'
                                                                                OnClick="SelectedItemButton_Click"></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:ListView>
                                                        </div>
                                                        <span role="button" class="input-group-addon btn-primary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><span class="glyphicon glyphicon-triangle-bottom text-white"></span></span>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    </div>

                                    <br />
                                    <br />
                                    <div class="form-inline">
                                            <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, DefaultString %>" ToolTip="<%$ Resources:lang, DefaultString %>"></asp:Label>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="cbxDPPD" runat="server" CssClass="checkbox" />
                                            </div>
                                        <div class="col-sm-8">
                                                <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, NeedPreventionFakeStockString %>"></asp:Label>
                                         </div>
                                    </div>

                                    <br />
                                    <br />
                                    <div class="form-inline">
                                            <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, DefaultString %>"></asp:Label>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="cbxDPPD2" runat="server" CssClass="checkbox" />
                                            </div>
                                         <div class="col-sm-8">
                                                <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, NeedPreventionFakeStockString %>"></asp:Label>
                                         </div>
                                    </div>

                                    <br />
                                    <br />
                                    <div>
                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, NotesString %>"></asp:Label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="multiline" Rows="5" />
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
                                    <div>
                                        <div class="form-inline">
                                            <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, InactiveString %>"></asp:Label>
                                            <asp:CheckBox ID="cbxInactive" runat="server" CssClass="checkbox" />
                                        </div>
                                        <br>
                                        <br />
                                        <div class="form-inline">
                                            <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang,CreationString %>"></asp:Label>
                                            <asp:Label ID="lblCreation" runat="server" CssClass="text-primary" />
                                        </div>
                                        <br>
                                        <br />
                                        <div class="form-inline">
                                            <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang,VersionString %>"></asp:Label>
                                            <asp:Label ID="lblVersion" runat="server" CssClass="text-primary" />
                                        </div>
                                        <br>
                                        <br />
                                        <div class="form-inline">
                                            <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang,ModeString %>"></asp:Label>
                                            <asp:Label ID="lblMode" runat="server" CssClass="text-primary" Text="(Alteração/Inserção)" />
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-1"></div>
                            </div>
                            <br>
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
                        <div class="col-xs-2 col-sm-2 col-lg-1">
                        </div>
                    </div>
                </div>
                <div class="col-lg-1"></div>
                 </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<uc1:MessageUC runat="server" ID="MessageUC" />
