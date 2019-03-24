<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workers.aspx.cs" Inherits="STTProjects.TimeSheet.WebApp.Workers" %>

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
            <%=txtNewWorkerName.ClientID%>: {
                required: true
            },
            <%=txtNewWorkerLastName.ClientID%>: {
                required: true
            }
        };
        var validationMessagesAddNewWorker = {
            <%=txtNewWorkerName.ClientID%>: {
                required: "Informe um nome."
            },
            <%=txtNewWorkerLastName.ClientID%>: {
                  required: "Informe o sobrenome."
              }
           
        };

        

        
        var validationRulesEditWorker = {
            <%=txtEditWorkerName.ClientID%>: {
                  required: true
              },
              <%=txtEditWorkerLastName.ClientID%>: {
                required: true
            }
          };
          var validationMessagesEditWorker = {
              <%=txtEditWorkerName.ClientID%>: {
                required: "Informe um nome."
            },
            <%=txtEditWorkerLastName.ClientID%>: {
                required: "Informe o sobrenome."
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
        <div style="text-align: center">
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

                                  
                    if (args.get_postBackElement().id == '<%=btnAddNewWorker.ClientID%>' ) {
              
                        configureFormValidation(validationRulesAddNewWorker, validationMessagesAddNewWorker);
                                          
                    }
                    else  if (args.get_postBackElement().id == '<%=btnUpdateWorker.ClientID%>' ) {
                     
                        configureFormValidation(validationRulesEditWorker,validationMessagesEditWorker);
                                          
                    }
                    
                    if ($("#form1").valid() !== true) {
                        args.set_cancel(true);
                    }  
                    
                }
            </script>

            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                <ContentTemplate>
                    <div style="width: 90%; margin-right: 5%; margin-left: 5%; background-color: darkcyan;">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <h1 style="color: white">LISTA DE COLABORADORES</h1>
                                </td>
                                <td>
                                    <asp:Button ID="btnAddWorkerModal" runat="server" Text="Adicionar Coloborador" CssClass="btn btn-info" OnClick="btnAddWorkerModal_Click" />
                                </td>
                            </tr>

                        </table>


                    </div>
                    <asp:GridView ID="gridViewWorkers" runat="server" AllowPaging="True" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gridViewWorkers_RowCommand" Style="width: 90%; margin-right: 5%; margin-left: 5%; text-align: center" CssClass="table table-hover table-striped" OnPageIndexChanging="gridViewWorkers_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="Name" HeaderText="NOME" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="LastName" HeaderText="SOBRENOME" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />


                            <asp:ButtonField CommandName="editWorker" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="Editar" HeaderText="">
                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                            </asp:ButtonField>


                            <asp:ButtonField CommandName="deleteWorker" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="Deletar" HeaderText="">
                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                            </asp:ButtonField>

                        </Columns>

                    </asp:GridView>
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
                            <h3 id="delModalLabel">Excluir Colaborador</h3>
                        </div>


                        <asp:UpdatePanel ID="upDel" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    Tem certeza que deseja excluir o Colaborador?
                            <asp:HiddenField ID="HfDeleteID" runat="server" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnDelete" runat="server" Text="Confirmar Deleção" CssClass="btn btn-info" OnClick="btnDelete_Click" />
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


            <div id="addModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="addModalLabel" aria-hidden="true">

                <div class="modal-dialog">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 id="addModalLabel">Adicionar novo Colaborador</h3>
                        </div>
                        <asp:UpdatePanel ID="upAdd" runat="server">



                            <ContentTemplate>
                                <form id="formNewWorker">
                                    <div class="modal-body">

                                        <table class="table table-bordered table-hover" style="text-align: left; vertical-align: central">
                                            <tr>
                                                <td style="width: 50px">
                                                    <h4><span class="label label-pill label-info">Nome :</span></h4>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewWorkerName" runat="server" ClientIDMode="Static" Width="200"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h4><span class="label label-pill label-info">Sobrenome :</span></h4>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewWorkerLastName" runat="server" ClientIDMode="Static" Width="200"></asp:TextBox></td>
                                            </tr>

                                            <tr>

                                                <td colspan="2">
                                                    <asp:Label ID="lblNewWorkerMessage" runat="server" ClientIDMode="Static" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                            </tr>

                                        </table>


                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnAddNewWorker" runat="server" Text="Adicionar" CssClass="btn btn-info" OnClick="btnAddNewWorker_Click" ClientIDMode="Static" />
                                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Fechar</button>
                                    </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAddNewWorker" EventName="Click" />
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
                            <h3 id="editModalLabel">Editar dados do Colaborador</h3>
                        </div>
                        <asp:UpdatePanel ID="upEdit" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HfUpdateID" runat="server" />
                                    <table class="table" style="text-align: left; vertical-align: central">

                                        <tr>
                                            <td style="width:50px">
                                                <h4><span class="label label-pill label-info">ID :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditWorkerID" runat="server" ReadOnly="true" Style="background-color: whitesmoke;" Width="50"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">Nome :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditWorkerName" runat="server" Width="200"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">Sobrenome :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditWorkerLastName" runat="server" Width="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:Label ID="lblEditWorkerMessage" runat="server" ClientIDMode="Static" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                        </tr>

                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                                    <asp:Button ID="btnUpdateWorker" runat="server" Text="Atualizar" CssClass="btn btn-info" OnClick="btnUpdateWorker_Click" />
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Fechar</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridViewWorkers" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="btnUpdateWorker" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>

        </div>
    </form>



</body>
</html>
