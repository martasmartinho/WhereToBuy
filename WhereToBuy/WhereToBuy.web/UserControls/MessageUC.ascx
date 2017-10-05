<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageUC.ascx.cs" Inherits="WhereToBuy.web.UserControls.MessageUC" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline" >
    <ContentTemplate>
       

          

        <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2"></div>
        <div class="col-xs-8 col-sm-8 col-md-8 col-lg8  container">
            <!-- Trigger the modal with a button -->
            <button id="btnDummy" type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal" style="display:none" ></button>

            <!-- Modal -->
            
            <div class="modal fade" id="myModal" role="dialog"  style="width:100%" >
               
                <div class="modal-dialog model-lg" >

                    <div class="modal-content" >
 
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4><asp:Label ID="TitleLabel" runat="server" Text="Title" /></h4>
                        </div>
                        <div class="modal-body">
                            <blockquote>
                                <asp:label ID="MessageLabel" runat="server" Text="Message" ></asp:label>
                            </blockquote>
                        </div>
                        <div class="modal-footer">
                            <button id="btnCloseModal" type="button" class="btn btn-default" data-dismiss="modal" >Close</button>
                        </div>
                    </div>

                </div>
                
            </div>
            
        </div>
        <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2"></div>
    </ContentTemplate>
</asp:UpdatePanel>


