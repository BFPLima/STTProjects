<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activities.aspx.cs" Inherits="STTProjects.TimeSheet.WebApp.Activities" EnableEventValidation="false" %>

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

        var validationRulesAddNewActivity = {
            <%=txtNewActivityName.ClientID%>: {
                required: true
            },
            <%=txtNewActivityDescription.ClientID%>: {
                required: true
            }
        };
        var validationMessagesAddNewActivity = {
            <%=txtNewActivityName.ClientID%>: {
                required: "Informe um nome."
            },
            <%=txtNewActivityDescription.ClientID%>: {
                required: "Informe a Descrição."
            }
           
        };

        

        
        var validationRulesEditActivity = {
            <%=txtEditActivityName.ClientID%>: {
                required: true
            },
            <%=txtEditActivityDescription.ClientID%>: {
                required: true
            }
        };
        var validationMessagesEditActivity = {
            <%=txtEditActivityName.ClientID%>: {
                required: "Informe um nome."
            },
            <%=txtEditActivityDescription.ClientID%>: {
                required: "Informe a Descrição."
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

                                  
                    if (args.get_postBackElement().id == '<%=btnAddNewActivity.ClientID%>' ) {
              
                        configureFormValidation(validationRulesAddNewActivity, validationMessagesAddNewActivity);
                                          
                    }
                    else  if (args.get_postBackElement().id == '<%=btnUpdateActivity.ClientID%>' ) {
                     
                        configureFormValidation(validationRulesEditActivity,validationMessagesEditActivity);
                                          
                    }
                    
                    if ($("#form1").valid() !== true) {
                        args.set_cancel(true);
                    }  
                    
                }
            </script>


            <div style="width: 90%; margin-right: 5%; margin-left: 5%;">
                <table>
                    <tr>
                        <td>
                            <h3><span class="label label-pill label-info">Projetos :</span></h3>
                        </td>
                        <td>
                            <h3><span class="label label-pill label-info">
                                <asp:DropDownList ID="dDListProjects" runat="server" AutoPostBack="True" DataTextField="Name" DataValueField="ID" OnSelectedIndexChanged="dDListProjects_SelectedIndexChanged" Width="300"></asp:DropDownList>
                            </span>
                            </h3>
                        </td>
                    </tr>

                </table>


            </div>

            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                <ContentTemplate>
                    <div style="width: 90%; margin-right: 5%; margin-left: 5%; background-color: darkcyan;">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <h1 style="color: white">LISTA DE ATIVIDADES</h1>
                                </td>
                                <td>
                                    <asp:Button ID="btnAddActivityModal" runat="server" Text="Adicionar Atividade" CssClass="btn btn-info" OnClick="btnAddActivityModal_Click" />
                                </td>
                            </tr>

                        </table>


                    </div>
                    <asp:GridView ID="gridViewActivities" runat="server" AllowPaging="True" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gridViewActivities_RowCommand" Style="width: 90%; margin-right: 5%; margin-left: 5%; text-align: center" CssClass="table table-hover table-striped" OnPageIndexChanging="gridViewActivities_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="Name" HeaderText="NOME" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="Description" HeaderText="DESCRIÇÃO" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="Date" HeaderText="DATA" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" DataFormatString="{0:dd/MM/yyyy}" />




                            <asp:ButtonField CommandName="manageWorkers" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="Gerenciar Colaboradores" HeaderText="">
                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                            </asp:ButtonField>



                            <asp:ButtonField CommandName="editActivity" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="Editar" HeaderText="">
                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                            </asp:ButtonField>


                            <asp:ButtonField CommandName="deleteActivity" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="Deletar" HeaderText="">
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
                            <h3 id="delModalLabel">Excluir Atividade</h3>
                        </div>


                        <asp:UpdatePanel ID="upDel" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    Tem certeza que deseja excluir a Atividade?
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
                            <h3 id="addModalLabel">Adicionar nova Atividade</h3>
                        </div>
                        <asp:UpdatePanel ID="upAdd" runat="server">



                            <ContentTemplate>
                                <form id="formNewActivity">
                                    <div class="modal-body">


                                        <table class="table table-bordered table-hover" style="text-align: left; vertical-align: central">
                                            <tr>
                                                <td style="width: 50px">
                                                    <h4><span class="label label-pill label-info">Nome :</span></h4>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewActivityName" runat="server" ClientIDMode="Static" CausesValidation="true" Width="200"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h4><span class="label label-pill label-info">Descrição :</span></h4>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewActivityDescription" runat="server" ClientIDMode="Static" Width="200"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h4><span class="label label-pill label-info">Data :</span></h4>
                                                </td>
                                                <td>
                                                    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td colspan="2">
                                                    <asp:Label ID="lblNewActivityMessage" runat="server" ClientIDMode="Static" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                            </tr>
                                        </table>

                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnAddNewActivity" runat="server" Text="Adicionar" CssClass="btn btn-info" OnClick="btnAddNewActivity_Click" ClientIDMode="Static" />
                                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Fechar</button>
                                    </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAddNewActivity" EventName="Click" />
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
                            <h3 id="editModalLabel">Editar dados da Atividade</h3>
                        </div>
                        <asp:UpdatePanel ID="upEdit" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HfUpdateID" runat="server" />
                                    <table class="table" style="text-align: left; vertical-align: central">

                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">ID :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditActivityID" runat="server" ReadOnly="true" Style="background-color: whitesmoke;" Width="50"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">Nome :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditActivityName" runat="server" Width="200"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">Descrição :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditActivityDescription" runat="server" Width="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">Data :</span></h4>
                                            </td>
                                            <td>
                                                <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
                                            </td>
                                        </tr>
                                         <tr>

                                                <td colspan="2">
                                                    <asp:Label ID="lblEditActivityMessage" runat="server" ClientIDMode="Static" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                            </tr>

                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                                    <asp:Button ID="btnUpdateActivity" runat="server" Text="Atualizar" CssClass="btn btn-info" OnClick="btnUpdateActivity_Click" />
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Fechar</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridViewActivities" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="btnUpdateActivity" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>


            <div class="modal fade" id="managerWorkersModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 100%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                            <h4 class="modal-title" id="myModalLabel">Gerenciamento de Colaboradores por Atividade</h4>
                        </div>
                        <div class="modal-body" id="content">
                            <iframe id="iFrmaeWorkersByActivity" style="width: 100%;" height="600" frameborder="0" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>



</body>
</html>
