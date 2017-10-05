<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriesSelBox.ascx.cs" Inherits="WhereToBuy.web.UserControls.Categories.CategoriesSelBox.CategoriesSelBox" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
         <script type="text/javascript">
             // O METODO AJAX Sys.Application.add_load É EXECUTADO SEMPRE QUE O CONTEUDO DO UPDATEPANEL É
             // CARREGADO PELO BROWSER. ASSIM, SEMPRE QUE EXISTE ATUALIZAÇÃO DO UPDATEPANEL
             // ISTO É EXECUTADO (PRETENDIDO).
             // Sys.Application.add_load => ADICIONA CONTEUDO AO LOAD DA PAGINA
             Sys.Application.add_load(function () {
                 $(document).ready(function () {
                     var pnlCategoryId = '#' + '<%= pnlCategorySelBox.ClientID %>';



                // TRATA O ENTER COMO PERDER O FOCUS EM FAVOR DE LINKSELECIONAR
                $(pnlCategoryId + ' input').keydown(function (e) {
                    // processa este bloco somente se for pressionado um enter ou um tab
                    if (e.which == 13) {
                        // NÃO EXECUTA O COMPORTAMENTO POR DEFAULT
                        e.preventDefault();

                        $(pnlCategoryId + ' #<%= lkBtnSearch.ClientID%>').focus();

                        }
                    });
            });
        });
    </script>
    <asp:Panel ID="pnlCategorySelBox" runat="server" CssClass="row">
        <div class="col-xs-10 col-sm-10 col-lg-11">
                <div class="dropdown">
                    <asp:TextBox ID="txtCategory" CssClass="form-control" runat="server" AutoCompleteType="Disabled" AutoPostBack="true" onFocus="this.select()"></asp:TextBox>


                    <button id="btnCategoryDummy" type="button" data-toggle="dropdown" style="display: none"></button>


                    <div class="dropdown-menu dropdown-menu-left" style="width: 100%">
                        <asp:ListView ID="lvCategories" runat="server">
                            <LayoutTemplate>
                                <table>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td id='<%# Container.DataItemIndex %>'>
                                        <asp:LinkButton ID="lkBtnItem" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-link" ForeColor="#428bca" Text='<%# string.Format("{0} - {1}",((WhereToBuy.entities.Category)Container.DataItem).Code, 
                                                                                                                                                        ((WhereToBuy.entities.Category)Container.DataItem).Description )%>'
                                            OnClick="lkBtnItem_Click"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>

                        </asp:ListView>
                    </div>

                </div>
            </div>

            <div class="col-xs-2 col-sm-2 col-lg-1">
                <asp:LinkButton ID="lkBtnSearch" runat="server" class="btn btn-primary dropdown-toggle" OnClick="lkBtnSearch_Click" onFocus="this.click()" ToolTip="<%$ Resources:lang, SearchString %>"><span class="glyphicon glyphicon-search" ></span></asp:LinkButton>
            </div>
    </asp:Panel>
    </ContentTemplate>
   
</asp:UpdatePanel>
