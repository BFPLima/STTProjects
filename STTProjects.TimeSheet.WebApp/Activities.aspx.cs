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
    public partial class Activities : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ProjectBusinessLogic projectBusinessLogic = new ProjectBusinessLogic();
                var list = projectBusinessLogic.GetAll().OrderBy(o => o.Name).ToList();
                this.dDListProjects.DataSource = list;
                this.dDListProjects.DataBind();

                if (list.Count() >= 1)
                {
                    this.dDListProjects.SelectedIndex = 0;
                    this.gridViewActivities.DataSource = list[0].Activities;
                    this.gridViewActivities.DataBind();
                }

            }

        }


        protected Project GetCurrentProjet()
        {
            if (string.IsNullOrWhiteSpace(this.dDListProjects.SelectedValue))
                return null;


            int projectID = Convert.ToInt32(this.dDListProjects.SelectedValue);

            ProjectBusinessLogic projectBusinessLogic = new ProjectBusinessLogic();

            Project project = projectBusinessLogic.GetByID(projectID);

            return project;
        }

        protected void dDListProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindActivities();
        }

        protected void BindActivities()
        {
            this.gridViewActivities.DataSource = GetCurrentProjet().Activities;
            this.gridViewActivities.DataBind();
        }

        protected void btnAddActivityModal_Click(object sender, EventArgs e)
        {

            Project project = GetCurrentProjet();

            if (project == null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append(@"<script type='text/javascript'>");

                sb.Append("alert('Para criar uma Atividade é necessário informar um Projeto!');");

                sb.Append(@"</script>");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "scriptNoProject", sb.ToString(), false);

                return;
            }



            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "AddShowModalScript",
                                                    Util.GetScriptToShowModalDialog("addModal"),
                                                    false);

            this.lblNewActivityMessage.Text = string.Empty;

        }

        protected void btnAddNewActivity_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtNewActivityName.Text))
            {
                this.lblNewActivityMessage.Text = "O nome deve ser preenchido!";
                return;
            }


            if (string.IsNullOrWhiteSpace(this.txtNewActivityDescription.Text))
            {
                this.lblNewActivityMessage.Text = "A descrição deve ser preenchida!";
                return;
            }


            if (DateTime.MinValue.Equals(Calendar1.SelectedDate))
            {
                this.lblNewActivityMessage.Text = "É necessário informar uma Data";
                return;
            }

            Project project = GetCurrentProjet();

            if (project == null)
            {
                this.lblNewActivityMessage.Text = "Para criar uma Atividade é necessário informar um Projeto!";
                return;
            }


            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();
            Activity activity = new Activity();


            project.AttachActivity(activity);
            activity.Name = txtNewActivityName.Text;
            activity.Description = txtNewActivityDescription.Text;
            activity.Date = Calendar1.SelectedDate;

            activityBusinessLogic.Insert(activity);

            BindActivities();

            ScriptManager.RegisterClientScriptBlock(this,
                                                   this.GetType(),
                                                   "AddHideModalScript",
                                                   Util.GetScriptToHideModalDialog("addModal"),
                                                   false);


            this.txtNewActivityName.Text = string.Empty;
            this.txtNewActivityDescription.Text = string.Empty;
            this.Calendar1.SelectedDate = DateTime.Now;
        }

        protected void gridViewActivities_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("manageWorkers"))
            {
                ShowWorkers(e);
            }
            else if (e.CommandName.Equals("editActivity"))
            {
                EditRow(e);
            }
            else if (e.CommandName.Equals("deleteActivity"))
            {
                DeleteRow(e);
            }
        }

        protected void ShowWorkers(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            string id = gridViewActivities.DataKeys[index].Value.ToString();

            string src = "WorkersByActivity.aspx?ActivityID=" + id;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");
            sb.Append("var $iframe = $('#' + 'iFrmaeWorkersByActivity');");
            sb.Append("if ( $iframe.length ) {");
            sb.Append("$iframe.attr('src','" + src + "');  ");
            sb.Append("}");
            sb.Append(@"</script>");



            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "SetIframeSrc",
                                                    sb.ToString(),
                                                    false);



            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "AddShowModalScript",
                                                    Util.GetScriptToShowModalDialog("managerWorkersModal"),
                                                    false);

        }


        #region Update Region
        private void EditRow(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow gvrow = gridViewActivities.Rows[index];

            this.HfUpdateID.Value = HttpUtility.HtmlDecode(gvrow.Cells[0].Text);

            this.txtEditActivityID.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text);

            this.txtEditActivityName.Text = HttpUtility.HtmlDecode(gvrow.Cells[1].Text);

            this.txtEditActivityDescription.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text);
            this.Calendar2.SelectedDate = Convert.ToDateTime(HttpUtility.HtmlDecode(gvrow.Cells[3].Text));


            this.lblResult.Visible = false;


            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "EditModalScript",
                                                     Util.GetScriptToShowModalDialog("editModal"),
                                                     false);


            this.lblEditActivityMessage.Text = string.Empty;



        }


        protected void btnUpdateActivity_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(this.txtEditActivityName.Text))
            {
                this.lblEditActivityMessage.Text = "O nome deve ser preenchido!";
                return;
            }


            if (string.IsNullOrWhiteSpace(this.txtEditActivityDescription.Text))
            {
                this.lblEditActivityMessage.Text = "A descrição deve ser preenchida!";
                return;
            }

            if (DateTime.MinValue.Equals(Calendar2.SelectedDate))
            {
                this.lblEditActivityMessage.Text = "É necessário informar uma Data";
                return;
            }


            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();
            Activity activity = activityBusinessLogic.GetByID(Convert.ToInt32(HfUpdateID.Value));

            activity.Name = txtEditActivityName.Text;
            activity.Description = txtEditActivityDescription.Text;
            activity.Date = Calendar2.SelectedDate;

            activityBusinessLogic.Update(activity);

            BindActivities();

            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "EditHideModalScript",
                                                     Util.GetScriptToHideModalDialog("editModal"),
                                                     false);

            this.txtEditActivityName.Text = string.Empty;
            this.txtEditActivityDescription.Text = string.Empty;
            this.lblEditActivityMessage.Text = string.Empty;
        }
        #endregion


        #region Delete Region
        private void DeleteRow(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            string code = gridViewActivities.DataKeys[index].Value.ToString();
            HfDeleteID.Value = code;

            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "DeleteModalScript",
                                                     Util.GetScriptToShowModalDialog("deleteModal"),
                                                     false);

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {

            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();
            Activity activity = activityBusinessLogic.GetByID(Convert.ToInt32(HfDeleteID.Value));

            activityBusinessLogic.Delete(activity);


            ScriptManager.RegisterClientScriptBlock(this,
                                                     this.GetType(),
                                                     "delHideModalScript",
                                                      Util.GetScriptToHideModalDialog("deleteModal"),
                                                      false);


            BindActivities();
        }

        #endregion

        protected void gridViewActivities_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridViewActivities.PageIndex = e.NewPageIndex;
            BindActivities();
        }


    }
}