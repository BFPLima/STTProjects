using STTProjects.TimeSheet.BusinessLogic;
using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STTProjects.TimeSheet.WebApp
{
    public partial class Projects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                BindGridViewProjects();

                this.gridViewProjects.UseAccessibleHeader = true;             
            }




        }

        private void BindGridViewProjects()
        {
            ProjectBusinessLogic projectBusinessLogic = new ProjectBusinessLogic();

            this.gridViewProjects.DataSource = projectBusinessLogic.GetAll();
            this.gridViewProjects.DataBind();
        }

        protected void gridViewProjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteProject"))
            {
                DeleteRow(e);
            }
            else if (e.CommandName.Equals("editProject"))
            {
                EditRow(e);
            }
        }


        #region Delete Region
        private void DeleteRow(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            string code = gridViewProjects.DataKeys[index].Value.ToString();
            this.HfDeleteID.Value = code;

             ScriptManager.RegisterClientScriptBlock(this, 
                                                    this.GetType(),
                                                    "DeleteModalScript",
                                                    Util.GetScriptToShowModalDialog("deleteModal"),
                                                    false);

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {

            ProjectBusinessLogic projectBusinessLogic = new ProjectBusinessLogic();
            Project project = projectBusinessLogic.GetByID(Convert.ToInt32(HfDeleteID.Value));

            projectBusinessLogic.Delete(project);

            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "delHideModalScript",
                                                    Util.GetScriptToHideModalDialog("deleteModal"),
                                                    false);

            BindGridViewProjects();
        }

        #endregion

        #region Update Region
        private void EditRow(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow gvrow = gridViewProjects.Rows[index];

            this.HfUpdateID.Value = HttpUtility.HtmlDecode(gvrow.Cells[0].Text);

            this.txtEditProjectID.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text);

            this.txtEditProjectName.Text = HttpUtility.HtmlDecode(gvrow.Cells[1].Text);

            this.txtEditProjectDescription.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text);


            this.lblResult.Visible = false;

            ScriptManager.RegisterClientScriptBlock(this,
                                              this.GetType(),
                                              "EditModalScript",
                                              Util.GetScriptToShowModalDialog("editModal"),
                                              false);


            this.lblEditPrjectMessage.Text = string.Empty;



        }


        protected void btnUpdateProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtEditProjectName.Text))
            {
                lblEditPrjectMessage.Text = "O nome deve ser preenchido!";
                return;
            }


            if (string.IsNullOrWhiteSpace(this.txtEditProjectDescription.Text))
            {
                lblEditPrjectMessage.Text = "A descrição deve ser preenchido!";
                return;
            }


            ProjectBusinessLogic projectBusinessLogic = new ProjectBusinessLogic();
            Project project = projectBusinessLogic.GetByID(Convert.ToInt32(HfUpdateID.Value));
            string currentName = project.Name;

            project.Name = txtEditProjectName.Text;
            project.Description = txtEditProjectDescription.Text;

            IResultOperation result = projectBusinessLogic.UpdatetWithRule(project);

            if (result.Status != ResultOperationStatus.OK)
            {
                this.lblEditPrjectMessage.Text = result.Message;
                project.Name = currentName;
                return;
            }



            BindGridViewProjects();
          

            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "EditHideModalScript",
                                                  Util.GetScriptToHideModalDialog("editModal"),
                                                  false);

            lblEditPrjectMessage.Text = string.Empty;
        }
        #endregion

        #region Create Region
        protected void btnAddProjectModal_Click(object sender, EventArgs e)
        {          

            this.lblNewPrjectMessage.Text = string.Empty;
            this.txtNewPrjectName.Text = string.Empty;
            this.txtNewPrjectDescription.Text = string.Empty;

           

            ScriptManager.RegisterClientScriptBlock(this,
                                                     this.GetType(),
                                                     "AddShowModalScript",
                                                     Util.GetScriptToShowModalDialog("addModal"),
                                                     false);

        }

        protected void btnAddNewProject_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(this.txtNewPrjectName.Text))
            {
                 lblNewPrjectMessage.Text = "O nome deve ser preenchido!";
                 return;
            }


            if (string.IsNullOrWhiteSpace(this.txtNewPrjectDescription.Text))
            {
                lblNewPrjectMessage.Text = "A descrição deve ser preenchido!";
                return;
            }


            ProjectBusinessLogic projectBusinessLogic = new ProjectBusinessLogic();

            Project project = new Project()
              {
                  Name = txtNewPrjectName.Text,
                  Description = txtNewPrjectDescription.Text
              };


            IResultOperation result = projectBusinessLogic.InsertWithRule(project);

            if (result.Status != ResultOperationStatus.OK)
            {
                lblNewPrjectMessage.Text = result.Message;
                return;
            }

            BindGridViewProjects();
         

            ScriptManager.RegisterClientScriptBlock(this,
                                                  this.GetType(),
                                                  "AddHideModalScript",
                                                  Util.GetScriptToHideModalDialog("addModal"),
                                                  false);


            this.txtNewPrjectName.Text = string.Empty;
            this.txtNewPrjectDescription.Text = string.Empty;
        }


        #endregion

        protected void gridViewProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridViewProjects.PageIndex = e.NewPageIndex;
            BindGridViewProjects();
        }




    }
}