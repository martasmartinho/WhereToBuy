<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.Suppliers.Supplier.SupplierUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>




<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <script type="text/javascript">
            // O METODO AJAX Sys.Application.add_load É EXECUTADO SEMPRE QUE O CONTEUDO DO UPDATEPANEL É
            // CARREGADO PELO BROWSER. ASSIM, SEMPRE QUE EXISTE ATUALIZAÇÃO DO UPDATEPANEL
            // ISTO É EXECUTADO (PRETENDIDO).
            // Sys.Application.add_load => ADICIONA CONTEUDO AO LOAD DA PAGINA
            Sys.Application.add_load(function () {
                $(document).ready(function () {
                    var supplierPanel = '#' + '<%= pnlSupplier.ClientID %>';
                    var pnl1 = '#' + '<%= Panel1.ClientID %>';
                    var pnl2 = '#' + '<%= Panel2.ClientID %>';
                    var pnl3 = '#' + '<%= Panel3.ClientID %>';
                    var pnl4 = '#' + '<%= Panel4.ClientID %>';
                    var pnl5 = '#' + '<%= Panel5.ClientID %>';
                    var pnl6 = '#' + '<%= Panel6.ClientID %>';
                    var pnl7 = '#' + '<%= Panel7.ClientID %>';
                 <%--   var pnl8 = '#' + '<%= Panel8.ClientID %>';--%>

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


                    //####


                    $(pnl5 + ' .spinner .btn:first-of-type').on('click', function () {
                        $(pnl5 + ' .spinner input').val((parseInt($(pnl5 + ' .spinner input').val()) + 1));

                    });
                    $(pnl5 + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseInt($(pnl5 + ' .spinner input').val()) > 0) {
                            $(pnl5 + ' .spinner input').val((($(pnl5 + ' .spinner input').val()) - 1));
                        }


                    });


                    //####


                    $(pnl6 + ' .spinner .btn:first-of-type').on('click', function () {
                        if (parseFloat($(pnl6 + ' .spinner input').val()) < 100) {
                            $(pnl6 + ' .spinner input').val((parseFloat($(pnl6 + ' .spinner input').val()) + 0.10).toFixed(2));
                        }

                    });

                    $(pnl6 + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseFloat($(pnl6 + ' .spinner input').val()) > 0) {
                            $(pnl6 + ' .spinner input').val((($(pnl6 + ' .spinner input').val()) - 0.10).toFixed(2));
                        }
                    });

                    //####


                    $(pnl7 + ' .spinner .btn:first-of-type').on('click', function () {
                        if (parseFloat($(pnl7 + ' .spinner input').val()) < 100) {
                            $(pnl7 + ' .spinner input').val((parseFloat($(pnl7 + ' .spinner input').val()) + 0.10).toFixed(2));
                        }

                    });

                    $(pnl7 + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseFloat($(pnl7 + ' .spinner input').val()) > 0) {
                            $(pnl7 + ' .spinner input').val((($(pnl7 + ' .spinner input').val()) - 0.10).toFixed(2));
                        }


                    });

                    //####


                    //$(pnl8 + ' .spinner .btn:first-of-type').on('click', function () {
                    //    if (parseFloat($(pnl8 + ' .spinner input').val()) < 100) {
                    //        $(pnl8 + ' .spinner input').val((parseFloat($(pnl8 + ' .spinner input').val()) + 0.10).toFixed(2));
                    //    }

                    //});

                    //$(pnl8 + ' .spinner .btn:last-of-type').on('click', function () {

                    //    if (parseFloat($(pnl8 + ' .spinner input').val()) > 0) {
                    //        $(pnl8 + ' .spinner input').val((($(pnl8 + ' .spinner input').val()) - 0.10).toFixed(2));
                    //    }


                    //});


                    //####
                });
            });

        </script>

        <asp:Panel id="pnlSupplier" runat="server" class="container-fluid">

            <div class="col-lg-1"></div>

            <div class="col-lg-10">

                <%--title row--%>
                <div class="row">

                    <div class="col-lg-6">
                        <h2>
                            <asp:Label ID="lblTitle" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, SupplierString %>"></asp:Label>
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

                        <%--General Data--%>
                        <div class="row">
                            <h4><u><strong>
                                <asp:Label ID="lblGeneralData" runat="server" Text="<%$ Resources:lang, GeneralDataString %>"></asp:Label></strong></u></h4>

                            <div class="col-lg-1"></div>

                            <div class="col-lg-10">

                                <div>

                                    <div class="form-group">
                                        <asp:Label ID="lblCode" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, CodeString %>"></asp:Label>

                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control left" />
                                        </div>

                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lnkCodeSearch" runat="server" CssClass="btn btn-primary dropdown-toggle left" OnClick="lnkCodeSearch_Click" ToolTip="<%$ Resources:lang, LoadString %>"><span class="glyphicon glyphicon-download-alt"></span></asp:LinkButton>
                                        </div>

                                    </div>

                                    <br />
                                    <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:Label ID="lblName" runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, NameString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                                        </div>

                                    </div>

                                     <br />
                                    <br />
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, AddressString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="multiline" Rows="5"/>
                                            <br />
                                            
                                            <%--<div class="form-group">
                                        
                                                 <div class="col-sm-8">
                                                       <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control"/>
                                                  </div>

                                                 <div class="col-sm-4">
                                                     <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"/>
                                                 </div>

                                            </div>--%>
                                        </div>
                                      
                                      
                                    </div>

                                      <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:Label ID="Label13" runat="server" CssClass="control-label col-sm-2" Text=""></asp:Label>

                                        <div class="form-inline col-sm-10">
                                           
                                                <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control"/>

                                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"/>
                                             
                                        </div>

                                    </div>
                                    
                                     <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                   
                                    <div class="form-group">

                                        <asp:Label ID="Label5" runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, IdentificationNumberString %>"></asp:Label>

                                        <div class="form-inline col-sm-10">
                                            <asp:TextBox ID="txtTaxNumber" runat="server" CssClass="form-control " />
                                        </div>

                                    </div>

                                </div>

                            </div>

                            <div class="col-lg-1"></div>

                        </div>

                        <br/>
                        <hr/>

                         <%--Salesman Data--%>
                        <div class="row">

                            <h4><u><strong>
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:lang, SalesmanString %>"></asp:Label></strong></u></h4>

                            <div class="col-lg-1"></div>

                            <div class="col-lg-10">

                                <div >

                                    <div class="form-group">

                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, NameString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtSalesmanName" runat="server" CssClass="form-control" />
                                        </div>

                                    </div>

                                    <br />
                                    <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, EmailString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                                        </div>

                                    </div>

                                    <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, PhoneString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                                        </div>

                                    </div>

                                    <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, MobileString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" />
                                        </div>

                                    </div>

                                    <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, SmsString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtSms" runat="server" CssClass="form-control" />
                                        </div>

                                    </div>

                                    <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, UsernameString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Text="" />
                                        </div>

                                    </div>

                                    <br />
                                    <br />

                                    <div class="form-group">

                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, PasswordString %>"></asp:Label>

                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Text="" />
                                        </div>

                                    </div>

                                    <br />
                                    <br />

                                     <div class="form-group">
                                        <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang, OnlineActiveAccessString%>"></asp:Label>
                                         <div class="col-sm-10">
                                            <asp:CheckBox ID="cbxActiveAccess" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>

                                </div>

                            </div>

                            <div class="col-lg-1"></div>

                        </div>



                        <br/>
                        <hr/>

                        <%--Configuration Data--%>
                        <div class="row">

                            <h4><u><strong>
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:lang, ConfigurationsString %>"></asp:Label></strong></u></h4>

                            <div class="col-sm-1 col-md-1 col-lg-1"></div>

                            <div class="col-sm-10 col-md-10 col-lg-10">

                                <div >

                                   
                                    <asp:Panel ID="Panel1" runat="server" CssClass="form-group">
                                        <div class="col-sm-9 col-md-9 col-lg-9">
                                            <asp:Label ID="Label4" runat="server" CssClass="control-label" Text="<%$ Resources:lang, SupplierBigText1String %>"></asp:Label>
                                        </div>
                                        

                                        <div class="col-sm-3 col-md-3 col-lg-3">

                                            <div class="input-group spinner" style="width:100%">

                                                <asp:TextBox ID="txtExpirationHours" runat="server" Enabled="false" CssClass="form-control" />

                                                <div class="input-group-btn-vertical">
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>



                                    <br />
                                    <br />
                                    <br />

                                    <asp:Panel ID="Panel2" runat="server" CssClass="form-group">
                                        <div class="col-sm-9 col-md-10 col-lg-11">
                                            <asp:Label ID="Label7" runat="server" CssClass="control-label" Text="<%$ Resources:lang, SupplierBigText9String %>"></asp:Label>
                                        </div>
                                        

                                        <div class="col-sm-3 col-md-2 col-lg-1">

                                            <div class="input-group spinner" style="width:100%; height:20%">

                                                <asp:TextBox ID="txtProductDescriptionScore" runat="server" Enabled="false" CssClass="form-control" />

                                                <div class="input-group-btn-vertical">
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>



                                    <br />
                                    <br />

                                    <asp:Panel ID="Panel3" runat="server" CssClass="form-group">
                                        <div class="col-sm-10 col-md-11 col-lg-11">
                                            <asp:Label ID="Label8" runat="server" CssClass="control-label" Text="<%$ Resources:lang, SupplierBigText10String %>"></asp:Label>
                                        </div>
                                        

                                        <div class="col-sm-2 col-md-1 col-lg-1">

                                            <div class="input-group spinner" style="width:100%">

                                                <asp:TextBox ID="txtProductFeaturesScore" runat="server" Enabled="false" CssClass="form-control" />

                                                <div class="input-group-btn-vertical">
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>



                                    <br />
                                    <br />

                                    <asp:Panel ID="Panel4" runat="server" CssClass="form-group">
                                        <div class="col-sm-10 col-md-11 col-lg-11">
                                            <asp:Label ID="Label9" runat="server" CssClass="control-label" Text="<%$ Resources:lang, SupplierBigText11String %>"></asp:Label>
                                        </div>
                                        

                                        <div class="col-sm-2 col-md-1 col-lg-1">

                                            <div class="input-group spinner" style="width:100%">

                                                <asp:TextBox ID="txtProductLinkScore" runat="server" Enabled="false" CssClass="form-control" />

                                                <div class="input-group-btn-vertical">
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>



                                    <br />
                                    <br />

                                    <asp:Panel ID="Panel5" runat="server" CssClass="form-group">
                                        <div class="col-sm-10 col-md-11 col-lg-11">
                                            <asp:Label ID="Label10" runat="server" CssClass="control-label" Text="<%$ Resources:lang, SupplierBigText12String %>"></asp:Label>
                                        </div>
                                        

                                        <div class="col-sm-2 col-md-1 col-lg-1">

                                            <div class="input-group spinner" style="width:100%">

                                                <asp:TextBox ID="txtProductImageScore" runat="server" Enabled="false" CssClass="form-control" />

                                                <div class="input-group-btn-vertical">
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>



                                    <br />
                                    <br />

                                    <asp:Panel ID="Panel6" runat="server" CssClass="form-group">
                                        <div class="col-sm-10 col-md-11 col-lg-11">
                                            <asp:Label ID="Label11" runat="server" CssClass="control-label" Text="<%$ Resources:lang, SupplierBigText13String %>"></asp:Label>
                                        </div>
                                        

                                        <div class="col-sm-2 col-md-1 col-lg-1">

                                            <div class="input-group spinner" style="width:100%">

                                                <asp:TextBox ID="txtProductPriceTrust" runat="server" Enabled="false" CssClass="form-control" />

                                                <div class="input-group-btn-vertical">
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>



                                    <br />
                                    <br />

                                    <asp:Panel ID="Panel7" runat="server" CssClass="form-group">
                                        <div class="col-sm-10 col-md-10 col-lg-11">
                                            <asp:Label ID="Label12" runat="server" CssClass="control-label" Text="<%$ Resources:lang, SupplierBigText14String %>"></asp:Label>
                                        </div>
                                        

                                        <div class="col-sm-2 col-md-2 col-lg-1">

                                            <div class="input-group spinner" style="width:100%">

                                                <asp:TextBox ID="txtProductAvailableTrust" runat="server" Enabled="false" CssClass="form-control" />

                                                <div class="input-group-btn-vertical">
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>


                                    <br />
                                    <br />

                                 <%--    <asp:Panel ID="Panel8" runat="server" CssClass="form-group">
                                        <div class="col-sm-10 col-md-10 col-lg-11">
                                            <asp:Label ID="Label14" runat="server" CssClass="control-label" Text="<%$ Resources:lang, SupplierBigText14String %>"></asp:Label>
                                        </div>
                                        

                                        <div class="col-sm-2 col-md-2 col-lg-1">

                                            <div class="input-group spinner" style="width:100%">

                                                <asp:TextBox ID="txtTrustIndex" runat="server" Enabled="false" CssClass="form-control" />

                                                <div class="input-group-btn-vertical">
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>

                                            </div>

                                        </div>

                                    </asp:Panel>


                                    <br />
                                    <br />--%>

                                     <div class="form-group">
                                        
                                          <div class="col-sm-11">
                                              <asp:Label runat="server" class="control-label" Text="<%$ Resources:lang, SupplierBigText2String%>"></asp:Label>
                                          </div>
                                         <div class="col-sm-1">
                                            <asp:CheckBox ID="cbxAutomaticMatching" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>

                                    <br />
                                    <br />

                                     <div class="form-group">
                                        
                                          <div class="col-sm-11">
                                              <asp:Label runat="server" class="control-label" Text="<%$ Resources:lang, SupplierBigText3String%>"></asp:Label>
                                          </div>
                                         <div class="col-sm-1">
                                            <asp:CheckBox ID="cbxAutomaticProducts" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>

                                    <br />
                                    <br />

                                    <div class="form-group">
                                        
                                          <div class="col-sm-11">
                                              <asp:Label runat="server" class="control-label" Text="<%$ Resources:lang, SupplierBigText4String%>"></asp:Label>
                                          </div>
                                         <div class="col-sm-1">
                                            <asp:CheckBox ID="cbxShowProductDetail" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>
                                     

                                    <br />
                                    <br />

                                     <div class="form-group">
                                        
                                          <div class="col-sm-11">
                                              <asp:Label runat="server" class="control-label" Text="<%$ Resources:lang, SupplierBigText15String%>"></asp:Label>
                                          </div>
                                         <div class="col-sm-1">
                                            <asp:CheckBox ID="cbxAutomaticProductUpdate" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>

                                    <br />
                                    <br />

                                     <div class="form-group">
                                        
                                          <div class="col-sm-11">
                                              <asp:Label runat="server" class="control-label" Text="<%$ Resources:lang, SupplierBigText5String%>"></asp:Label>
                                          </div>
                                         <div class="col-sm-1">
                                            <asp:CheckBox ID="cbxShowDescription" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>

                                    <br />
                                    <br />

                                    <div class="form-group">
                                        
                                          <div class="col-sm-11">
                                              <asp:Label runat="server" class="control-label" Text="<%$ Resources:lang, SupplierBigText6String%>"></asp:Label>
                                          </div>
                                         <div class="col-sm-1">
                                            <asp:CheckBox ID="cbxShowFeatures" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>
 
                                    <br />
                                    <br />

                                     <div class="form-group">
                                        
                                          <div class="col-sm-11">
                                              <asp:Label runat="server" class="control-label" Text="<%$ Resources:lang, SupplierBigText7String%>"></asp:Label>
                                          </div>
                                         <div class="col-sm-1">
                                            <asp:CheckBox ID="cbxShowLink" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>

                                    <br />
                                    <br />

                                     <div class="form-group">
                                        
                                          <div class="col-sm-11">
                                              <asp:Label runat="server" class="control-label" Text="<%$ Resources:lang, SupplierBigText8String%>"></asp:Label>
                                          </div>
                                         <div class="col-sm-1">
                                            <asp:CheckBox ID="cbxShowImage" runat="server" CssClass="checkbox" type="checkbox" />
                                         </div>
                                        
                                    </div>

                                </div>

                            </div>

                            <div class="col-lg-1"></div>

                        </div>



                        <br/>
                        <hr/>

                        <%--Others--%>
                        <div class="row">
                            <h4><u><strong>
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:lang, OthersString %>"></asp:Label></strong></u></h4>
                            <div class="col-lg-1"></div>

                            <div class="col-lg-10">

                                <div class="form-inline">
                                    <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang, InactiveString %>"></asp:Label>
                                    <asp:CheckBox ID="cbxInactive" runat="server" CssClass="checkbox" type="checkbox" />
                                </div>

                                <br/>

                                <div class="form-inline">
                                                <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang,CreationString %>"></asp:Label>
                                                <asp:Label ID="lblCreation" runat="server" CssClass="text-primary" />
                                            </div>

                                <br/>

                                <div class="form-inline">
                                                <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang,VersionString %>"></asp:Label>
                                                <asp:Label ID="lblVersion" runat="server" CssClass="text-primary" />
                                            </div>

                                <br/>

                                <div class="form-inline">
                                                <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang,ModeString %>"></asp:Label>
                                                <asp:Label ID="lblMode" runat="server" CssClass="text-primary" Text="(Alteração/Inserção)" />
                                            </div>


                            </div>

                            <div class="col-lg-1"></div>

                        </div>

                        <br/>
                        

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



                    <div class="col-xs-2 col-sm-2 col-lg-1"></div>

                </div>

            </div>

            <div class="col-lg-1"></div>

        </asp:Panel>

    </ContentTemplate>

</asp:UpdatePanel>

<uc1:MessageUC runat="server" ID="MessageUC" />
