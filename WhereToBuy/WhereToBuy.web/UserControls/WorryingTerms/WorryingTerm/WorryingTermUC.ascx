<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WorryingTermUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.WorryingTerms.WorryingTerm.WorryingTermUC" %>
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
                    var wtrPanel = '#' + '<%= pnlWorryingTerm.ClientID %>';

                    $(wtrPanel + ' .spinner .btn:first-of-type').on('click', function () {
                        $(wtrPanel + ' .spinner input').val((parseInt($(wtrPanel + ' .spinner input').val()) + 1));

                    });
                    $(wtrPanel + ' .spinner .btn:last-of-type').on('click', function () {

                        if (parseInt($(wtrPanel + ' .spinner input').val()) > 0) {
                            $(wtrPanel + ' .spinner input').val((($(wtrPanel + ' .spinner input').val()) - 1));
                        }


                    });
                });
            });

        </script>

        <asp:Panel ID="pnlWorryingTerm" runat="server" CssClass="container-fluid">

            <div class="col-lg-1"></div>

            <div class="col-lg-10">

                <%--title row--%>
                <div class="row">
                    <div class="col-lg-6">
                        <h2>
                            <asp:Label ID="lblTitle" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, WorryingTermString %>"></asp:Label>
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
                                <div class="form-group">
                                    <asp:Label ID="lblTerm" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, TermString %>"></asp:Label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTerm" runat="server" CssClass="form-control left" />
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:LinkButton ID="lnkTermSearch" runat="server" CssClass="btn btn-primary dropdown-toggle left" OnClick="lnkTermSearch_Click" ToolTip="<%$ Resources:lang, LoadString %>"><span class="glyphicon glyphicon-download-alt"></span></asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                            <div class="col-lg-1"></div>

                        </div>


                        <br />
                        <hr>

                        <%--Configuration Data--%>
                        <div class="row">
                            <h4><u><strong>
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:lang, ConfigurationsString %>"></asp:Label></strong></u></h4>
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">

                                <div>
                                    <div>
                                        <asp:Label ID="Label4" runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, IndexString %>"></asp:Label>
                                        <div class="col-sm-10">
                                            <div class="input-group spinner">
                                                <asp:TextBox ID="txtIndex" runat="server" type="text" CssClass="form-control" Enabled="false" />
                                                <div class="input-group-btn-vertical">
                                                    <button id="btnWarryingUp" class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-up"></i></button>
                                                    <button id="btnWarryingDown" class="btn btn-primary btn-xs" type="button"><i class="glyphicon glyphicon-chevron-down"></i></button>
                                                </div>
                                            </div>

                                        </div>

                                    </div>

                                    <br />
                                    <br />
                                    <br />

                                    <div>
                                        <asp:Label runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, NotesString %>"></asp:Label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="multiline" Rows="5" />
                                        </div>
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
                                        <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang, InactiveString %>"></asp:Label>
                                        <asp:CheckBox ID="cbxInactive" runat="server" CssClass="checkbox" type="checkbox" />
                                    </div>
                                    <br />
                                    <div class="form-inline">
                                        <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang,CreationString %>"></asp:Label>
                                        <asp:Label ID="lblCreation" runat="server" CssClass="text-primary" />
                                    </div>
                                    <br />
                                    <div class="form-inline">
                                        <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang,VersionString %>"></asp:Label>
                                        <asp:Label ID="lblVersion" runat="server" CssClass="text-primary" />
                                    </div>
                                    <br />
                                    <div class="form-inline">
                                        <asp:Label runat="server" class="control-label col-sm-2" Text="<%$ Resources:lang,ModeString %>"></asp:Label>
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
                    </div>

                    <div class="col-xs-2 col-sm-2 col-lg-1"></div>

                </div>



            </div>

            <div class="col-lg-1"></div>

        </asp:Panel>


    </ContentTemplate>
</asp:UpdatePanel>

<uc1:MessageUC runat="server" ID="MessageUC" />
