<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkersByActivity.aspx.cs" Inherits="STTProjects.TimeSheet.WebApp.WorkersByActivity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/jquery.validate.min.js"></script>
    <script src="Scripts/bootstrap.js"></script>

     <script>
        
         var validation;

         var validationRulesAddNewWorker = {
             <%=txtAddHours.ClientID%>: {
                 required: true,
                 number: true
            },
            <%=txtAddComment.ClientID%>: {
                required: true
            }
        };
        var validationMessagesAddNewWorker = {
            <%=txtAddHours.ClientID%>: {
                required: "Informe as horas trabalhadas!",
                number: "Este campo é numérico!"
            },
            <%=txtAddComment.ClientID%>: {
                required: "Informe um comentário!"
            }
           
        };

          

        
        var validationRulesEditWorker = {
            <%=txtEditHours.ClientID%>: {
                required: true,
                number: true
            },
            <%=txtEditComment.ClientID%>: {
                  required: true
              }
        };
        var validationMessagesEditWorker = {
            <%=txtEditHours.ClientID%>: {
                required: "Informe as horas trabalhadas!",
                number: "Este campo é numérico!"
              },
              <%=txtEditComment.ClientID%>: {
                  required: "Informe um comentário!"
            }
           
          };
      

          $(document).ready(function () {

              validation = $('#form1').validate(); 


          }); 

      


    </script>




       <style type="text/css">
        label.error {
            color: red;
            display: inline-flex;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

                  <script type="text/javascript">
     
                      Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler);

                      function configureFormValidation(newRules, newMessages)
                      {
                          validation.resetForm();
                          validation.settings.rules = newRules;
                          validation.settings.messages = newMessages;
                  
                      }
       
                      function initializeRequestHandler(sender, args) {

                                  
                          if (args.get_postBackElement().id == '<%=btnAddNewAtivityWorker.ClientID%>' ) {
              
                        configureFormValidation(validationRulesAddNewWorker, validationMessagesAddNewWorker);
                                          
                    }
                    else  if (args.get_postBackElement().id == '<%=btnUpdate.ClientID%>' ) {
                     
                        configureFormValidation(validationRulesEditWorker,validationMessagesEditWorker);
                                          
                    }
                    
                    if ($("#form1").valid() !== true) {
                        args.set_cancel(true);
                    }  
                    
                }
            </script>

            <div style="width: 90%; margin-right: 5%; margin-left: 5%;">

                <fieldset>
                    <legend>Atividade</legend>

                    <table style="width: 90%; text-align: left; vertical-align: central" border="0">

                        <tr>
                            <td style="width: 100px">
                                <h4><span class="label label-pill label-info">Nome :</span></h4>
                            </td>
                            <td style="width: 275px">
                                <h4>
                                    <asp:Label ID="lblActivityName" runat="server" Text=""></asp:Label></h4>

                            </td>

                            <td style="width: 50px">
                                <h4><span class="label label-pill label-info">Data :</span></h4>
                            </td>
                            <td>
                                <h4>
                                    <asp:Label ID="lblActivityDate" runat="server" Text=""></asp:Label></h4>
                            </td>
                        </tr>

                    </table>


                </fieldset>

            </div>



            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                <ContentTemplate>




                    <div style="width: 90%; margin-right: 5%; margin-left: 5%; text-align: center" cssclass="table table-hover table-striped">


                        <table>
                            <tr>
                                <td style="vertical-align: top">
                                    <div style="text-align: left">
                                        <div  >

                                            <h4><span class="label label-pill label-info">Colaboradores Disponíveis</span></h4>
                                        </div>
                                        <div >
                                            <asp:ListBox ID="lBoxWorkers" runat="server" DataValueField="ID" DataTextField="FullName" Width="200" Height="250"></asp:ListBox>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 200px;vertical-align: top;margin-top:20%">
                                    <asp:Button ID="btnAddActivityWorker" runat="server" Text=">>>" CssClass="btn btn-info" OnClick="btnAddActivityWorker_Click" Height="290" /></td>

                                <td style="vertical-align: top; width: 80%">
                                 
                                    <asp:GridView ID="gridViewWorksByActivity" Width="100%" runat="server" AllowPaging="false" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gridViewWorksByActivity_RowCommand" CssClass="table table-hover table-striped">
                                        <Columns>
                                            <asp:ButtonField CommandName="deleteCommand" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="<<<" HeaderText="">
                                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" Visible="false" />

                                            <asp:BoundField DataField="Worker.FullName" HeaderText="NOME" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                                            <asp:BoundField DataField="Hours" HeaderText="HORAS" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large"  DataFormatString="{0:N2}"/>

                                            <asp:BoundField DataField="Comment" HeaderText="COMENTÁRIO" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />

                                            <asp:ButtonField CommandName="editCommand" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="Editar" HeaderText="">
                                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                                            </asp:ButtonField>




                                        </Columns>

                                    </asp:GridView>
                               
                                </td>
                            </tr>

                        </table>


                    </div>
                    </p>
                    </p>
                       

                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>


            <div id="deleteModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="delModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 id="delModalLabel">Remover colaborador da Atividade</h3>
                        </div>


                        <asp:UpdatePanel ID="upDel" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    Tem certeza que deseja remover o Colaborador?
                            <asp:HiddenField ID="HfDeleteID" runat="server" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnDelete" runat="server" Text="Confirmar" CssClass="btn btn-info" OnClick="btnDelete_Click" />
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>



            <div id="editModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 id="editModalLabel">Edição de atividade do colaborador</h3>
                        </div>
                        <asp:UpdatePanel ID="upEdit" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HfUpdateID" runat="server" />
                                    <table class="table">


                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Nome :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditWorkername" runat="server" Width="200" ReadOnly="true" BackColor="WhiteSmoke"></asp:TextBox></td>

                                        </tr>

                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Data :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditDate" runat="server" Width="100" ReadOnly="true" BackColor="WhiteSmoke"> </asp:TextBox></td>

                                        </tr>

                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Horas Disponívels :</span></h4>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEditAvaiableHours" runat="server" ForeColor="Green" Font-Size="X-Large" Font-Bold="true">5</asp:Label>
                                        </tr>
                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Horas :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditHours" runat="server" Width="50"></asp:TextBox></td>

                                        </tr>

                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Comentário :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditComment" runat="server" Width="200"></asp:TextBox></td>

                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:Label ID="lblEditErrorMessage" runat="server" ClientIDMode="Static" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                        </tr>

                                    </table>
                                </div>
                                <div class="modal-footer">

                                    <asp:Button ID="btnUpdate" runat="server" Text="Atualizar" CssClass="btn btn-info" OnClick="btnUpdate_Click" />

                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Fechar</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridViewWorksByActivity" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>

            <div id="addModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="addModalLabel" aria-hidden="true">

                <div class="modal-dialog">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 id="addModalLabel">Adicionar</h3>
                        </div>
                        <asp:UpdatePanel ID="upAdd" runat="server">



                            <ContentTemplate>

                                <asp:HiddenField ID="hfNewActivityWorker" runat="server" />
                                <div class="modal-body">


                                    <table class="table table-bordered table-hover" style="text-align: left; vertical-align: central">

                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Nome :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddWorkername" runat="server" Width="200" ReadOnly="true" BackColor="WhiteSmoke"></asp:TextBox></td>

                                        </tr>

                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Data :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddDate" runat="server" Width="100" ReadOnly="true" BackColor="WhiteSmoke"> </asp:TextBox></td>

                                        </tr>

                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Horas Disponívels :</span></h4>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAddAvaiableHours" runat="server" ForeColor="Green" Font-Size="X-Large" Font-Bold="true">5</asp:Label>
                                        </tr>
                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Horas :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddHours" runat="server" Width="50"></asp:TextBox></td>

                                        </tr>

                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Comentário :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAddComment" runat="server" Width="200"></asp:TextBox></td>
                                           
                                        </tr>

                                        <tr>

                                            <td colspan="2">
                                                <asp:Label ID="lblAddErrorMessage" runat="server" ClientIDMode="Static" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>

                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAddNewAtivityWorker" runat="server" Text="Adicionar" CssClass="btn btn-info" OnClick="btnAddNewAtivityWorker_Click" ClientIDMode="Static" />
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Fechar</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAddNewAtivityWorker" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>






        </div>
    </form>
</body>
</html>
