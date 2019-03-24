<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="STTProjects.TimeSheet.WebApp.Projects" %>

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

     

        var validationRulesAddNewProject = {
            <%=txtNewPrjectName.ClientID%>: {
                required: true
              
                
            },
            <%=txtNewPrjectDescription.ClientID%>: {
                required: true
            }
        };



        var validationMessagesAddNewProject = {
            <%=txtNewPrjectName.ClientID%>: {
                required: "Informe um nome.",
                remote: "Nome existente!"
            },
            <%=txtNewPrjectDescription.ClientID%>: {
                required: "Informe uma descrição."
            }
           
        };

        

        
        var validationRulesEditProject = {
            <%=txtEditProjectName.ClientID%>: {
                required: true
            },
            <%=txtEditProjectDescription.ClientID%>: {
                required: true
            }
        };
        var validationMessagesEditProject = {
            <%=txtEditProjectName.ClientID%>: {
                required: "Informe um nome."
            },
            <%=txtEditProjectDescription.ClientID%>: {
                required: "Informe uma descrição."
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


                Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(onEachRequest);
             

                function onEachRequest(sender, args) {
                    initializeRequestHandler(sender, args);
                }
     
              
                function configureFormValidation(newRules, newMessages)
                {
                    
                    validation.settings.rules = newRules;
                    validation.settings.messages = newMessages;
                    validation = $('#form1').validate(); 
                   
                }
       
                function initializeRequestHandler(sender, args) {

                    if (args.get_postBackElement().id == '<%=btnAddNewProject.ClientID%>' ) {
              
                        configureFormValidation(validationRulesAddNewProject, validationMessagesAddNewProject);
                                          
                    }
                    else  if (args.get_postBackElement().id == '<%=btnUpdateProject.ClientID%>' ) {
                     
                        configureFormValidation(validationRulesEditProject,validationMessagesEditProject);
                                          
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
                                    <h1 style="color: white">LISTA DE PROJETOS</h1>
                                </td>
                                <td>
                                    <asp:Button ID="btnAddProjectModal" runat="server" Text="Criar Projeto" CssClass="btn btn-info" OnClick="btnAddProjectModal_Click" />
                                </td>
                            </tr>

                        </table>


                    </div>


                    <asp:GridView ID="gridViewProjects" runat="server" AllowPaging="True" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gridViewProjects_RowCommand" Style="width: 90%; margin-right: 5%; margin-left: 5%; text-align: center" CssClass="table table-hover table-striped" OnPageIndexChanging="gridViewProjects_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="Name" HeaderText="NOME" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                            <asp:BoundField DataField="Description" HeaderText="DESCRIÇÃO" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />


                            <asp:ButtonField CommandName="editProject" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="Editar" HeaderText="">
                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                            </asp:ButtonField>


                            <asp:ButtonField CommandName="deleteProject" ControlStyle-CssClass="btn btn-info" ButtonType="Button" Text="Deletar" HeaderText="">
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
                            <h3 id="delModalLabel">Deletar Projeto</h3>
                        </div>


                        <asp:UpdatePanel ID="upDel" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    Tem certeza que deseja deletar o projeto?
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
                            <h3 id="addModalLabel">Adicionar novo Projeto</h3>
                        </div>
                        <asp:UpdatePanel ID="upAdd" runat="server">



                            <ContentTemplate>

                                <div class="modal-body">

                                    <table class="table table-bordered table-hover" style="text-align: left; vertical-align: central">
                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">Nome :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewPrjectName" runat="server" ClientIDMode="Static" CausesValidation="true" Width="200"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">Descrição :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewPrjectDescription" runat="server" ClientIDMode="Static" Width="200"></asp:TextBox></td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:Label ID="lblNewPrjectMessage" runat="server" ClientIDMode="Static" ForeColor="Red" Font-Bold ="true"></asp:Label></td>
                                        </tr>

                                    </table>


                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAddNewProject" runat="server" Text="Adicionar" CssClass="btn btn-info" OnClick="btnAddNewProject_Click" ClientIDMode="Static" />
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Fechar</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAddNewProject" EventName="Click" />
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
                            <h3 id="editModalLabel">Editar Projeto</h3>
                        </div>
                        <asp:UpdatePanel ID="upEdit" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HfUpdateID" runat="server" />
                                    <table class="table" style="text-align: left; vertical-align: central">

                                        <tr>
                                            <td style="width: 50px">
                                                <h4><span class="label label-pill label-info">ID :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditProjectID" runat="server" ReadOnly="true" Style="background-color: whitesmoke;" Width="50"></asp:TextBox></td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">Nome :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditProjectName" runat="server" Width="200"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <h4><span class="label label-pill label-info">Descrição :</span></h4>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEditProjectDescription" runat="server" Width="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="2">
                                                <asp:Label ID="lblEditPrjectMessage" runat="server" ClientIDMode="Static" ForeColor="Red" Font-Bold ="true"></asp:Label></td>
                                        </tr>

                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                                    <asp:Button ID="btnUpdateProject" runat="server" Text="Atualizar" CssClass="btn btn-info" OnClick="btnUpdateProject_Click" />
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Fechar</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gridViewProjects" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="btnUpdateProject" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>

        </div>
    </form>



</body>
</html>
