﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryMatchingUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.Categories.CategoryMatching.CategoryMatchingUC" %>
<%@ Register Src="~/UserControls/MessageUC.ascx" TagPrefix="uc1" TagName="MessageUC" %>
<%@ Register Src="~/UserControls/Categories/CategoriesSelBox/CategoriesSelBox.ascx" TagPrefix="uc1" TagName="CategoriesSelBox" %>
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
                                <asp:Label ID="TitleLabel" CssClass="text-primary top-left" runat="server" Text="<%$ Resources:lang, CategoryMatchingString %>"></asp:Label>
                            </h2>
                        </div>
                        <div class="col-lg-6">
                            <div class="row">
                                <div class="col-lg-6"></div>
                                <div class="col-lg-6">
                                    <div class="navbar-btn navbar-right">
                                        <h2>
                                            <asp:Button ID="btnNew" CssClass="btn btn-link btn-lg left " runat="server" Text="<%$ Resources:lang, NewString %>" OnClick="btnNew_Click" />
                                            <asp:Button ID="btnDelete" CssClass="btn btn-link btn-lg right" runat="server" Text="<%$ Resources:lang, DeleteString %>" OnClick="btnDelete_Click" /></h2>
                                    </div>

                                </div>

                            </div>
                        </div>

                    </div>

                    <%--body--%>
                    <div class="row">
                        <div class="col-xs-2 col-sm-2 col-lg-1">
                        </div>


                        <div class="col-xs-8 col-sm-8 col-lg-10">
                            <%--supplier--%>
                            <div class="row">

                                <div class="col-xs-8 col-sm-8 col-lg-8">
                                    <%--supplier--%>
                                    <div>
                                        <asp:Label ID="lblSupplier" runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, SupplierString %>"></asp:Label>
                                        <div class="col-sm-10">
                                            <uc1:SuppliersSelBox runat="server" ID="SuppliersSelBox" />
                                        </div>

                                    </div>

                                </div>

                                <div class="col-xs-4 col-sm-4 col-lg-">
                                </div>

                            </div>

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
                                                <asp:LinkButton ID="lnkCodeSearch" runat="server" CssClass="btn btn-primary dropdown-toggle left" OnClick="lnkCodeSearch_Click" ToolTip="<%$ Resources:lang, LoadString %>"><span class="glyphicon glyphicon-download-alt" ></span></asp:LinkButton>
                                            </div>
                                        </div>

                                        <br />
                                        <br />
                                        <br />

                                        <div class="form-group">
                                            <asp:Label ID="lblDescription" runat="server" CssClass="control-label col-sm-2" Text="<%$ Resources:lang, DescriptionString %>"></asp:Label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-1"></div>

                            </div>

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
                                            <asp:Label ID="Label2" CssClass="control-label col-sm-2" runat="server" Text="<%$ Resources:lang, CategoryString %>"></asp:Label>
                                            <div class="col-sm-8">
                                                <uc1:CategoriesSelBox runat="server" ID="CategoriesSelBox" />
                                            </div>
                                            <div class="col-sm-2">
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

                            </div>

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


            </div>
    </ContentTemplate>
</asp:UpdatePanel>

<uc1:MessageUC runat="server" ID="MessageUC" />
