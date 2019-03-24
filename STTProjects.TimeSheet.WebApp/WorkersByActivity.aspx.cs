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
    public partial class WorkersByActivity : System.Web.UI.Page
    {

        protected Activity activity = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.Page.Request.Params["ActivityID"] != null)
                {
                    Activity activity = CurrentActivity();
                    this.lblActivityName.Text = activity.Name;
                    this.lblActivityDate.Text = activity.Date.ToShortDateString();

                    BindDdListWorkers();
                    BindGridVeiwWorksByActivity();
                }
            }
        }

        protected Activity CurrentActivity()
        {
            if (this.activity == null)
            {
                int id = Convert.ToInt32(this.Page.Request.Params["ActivityID"]);
                ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();
                this.activity = activityBusinessLogic.GetByID(id);
            }

            return activity;
        }


        protected void BindDdListWorkers()
        {
            IList<Worker> listWorker = new List<Worker>();


            WorkerBusinessLogic workerBusinessLogic = new WorkerBusinessLogic();
            IList<Worker> listAllWorker = workerBusinessLogic.GetAll();

            IList<Worker> listActivityWorkers = CurrentActivity().GetWorkers();

            foreach (var worker in listAllWorker)
            {
                if (!listActivityWorkers.Contains(worker))
                {
                    listWorker.Add(worker);
                }
            }

            this.lBoxWorkers.DataSource = listWorker;
            this.lBoxWorkers.DataBind();

        }


        protected void BindGridVeiwWorksByActivity()
        {

            this.gridViewWorksByActivity.DataSource = CurrentActivity().ActivityWorkers;
            this.gridViewWorksByActivity.DataBind();
        }



        #region Delete Region
        private void DeleteRow(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            string code = gridViewWorksByActivity.DataKeys[index].Value.ToString();
            HfDeleteID.Value = code;

          
            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "DeleteModalScript",
                                                     Util.GetScriptToShowModalDialog("deleteModal"), 
                                                    false);
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ActivityWorkerBusinessLogic activityWorkerBusinessLogic = new ActivityWorkerBusinessLogic();
            ActivityWorker AativityWorker = activityWorkerBusinessLogic.GetByID(Convert.ToInt32(HfDeleteID.Value));


            Activity activity = AativityWorker.Activity;

            Worker worker = AativityWorker.Worker;

            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();

            activityBusinessLogic.DetachWoker(activity, worker);

            BindDdListWorkers();

            BindGridVeiwWorksByActivity();

           
            ScriptManager.RegisterClientScriptBlock(this,
                                                   this.GetType(),
                                                   "delHideModalScript",
                                                    Util.GetScriptToHideModalDialog("deleteModal"),
                                                   false);

        }

        #endregion

        protected void gridViewWorksByActivity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteCommand"))
            {
                DeleteRow(e);
            }
            else if (e.CommandName.Equals("editCommand"))
            {
                EditRow(e);
            }
        }



        #region Update Region
        private void EditRow(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            int id = Convert.ToInt32(gridViewWorksByActivity.DataKeys[index].Value.ToString());

            GridViewRow gvrow = gridViewWorksByActivity.Rows[index];

            this.HfUpdateID.Value = id.ToString();



            Activity activity = CurrentActivity();


            ActivityWorker activityWorker = activity.ActivityWorkers.Where(a => a.ID == id).SingleOrDefault();

            this.txtEditDate.Text = activity.Date.ToShortDateString();
            this.txtEditWorkername.Text = activityWorker.Worker.FullName;
            this.txtEditHours.Text = activityWorker.Hours.ToString("N2").Replace (",",".");
            this.txtEditComment.Text = activityWorker.Comment;

            this.lblEditErrorMessage.Text = string.Empty;


            Worker worker = activityWorker.Worker;

            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();

            double hoursAvaiable = activityBusinessLogic.GetAvaiablesHoursByWorkerOnDate(worker, activity.Date);

            this.lblEditAvaiableHours.Text = hoursAvaiable.ToString("N2").Replace(",", "."); ;




         
            ScriptManager.RegisterClientScriptBlock(this,
                                                this.GetType(),
                                                "EditModalScript",
                                                 Util.GetScriptToShowModalDialog("editModal"),
                                                false);




        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = null;

            double hoursWorked;

            if (!Double.TryParse(txtEditHours.Text.Replace('.', ','), out hoursWorked) || hoursWorked <= 0)
            {
                this.lblEditErrorMessage.Text = "Valor das horas não é válido!";
                return;
            }


            if (string.IsNullOrWhiteSpace(txtEditComment.Text))
            {
                this.lblEditErrorMessage.Text = "É necessário informar um comentário!";
                return;
            }


            int id = Convert.ToInt32(HfUpdateID.Value);
            Activity activity = CurrentActivity();

            ActivityWorker activityWorker = activity.ActivityWorkers.Where(a => a.ID == id).SingleOrDefault();

            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();

            double maxHoursByDay = ActivityBusinessLogic.MaxWorkHoursByDay();

            double temp = activityWorker.Hours;

            activityWorker.Hours = hoursWorked;

            double hoursAvaiable = activityBusinessLogic.GetAvaiablesHoursByWorkerOnDate(activityWorker.Worker, activity.Date);
                   

            if (hoursAvaiable < 0 || hoursWorked > maxHoursByDay || (hoursWorked - hoursAvaiable) > maxHoursByDay)
            {
                activityWorker.Hours = temp;
                this.lblEditErrorMessage.Text = "As horas trabalhas superam as horas dispónives!";
                return;
            }

            activityWorker.Hours = hoursWorked;
            activityWorker.Comment = txtEditComment.Text;


            ActivityWorkerBusinessLogic activityWorkerBusinessLogic = new ActivityWorkerBusinessLogic();
            activityWorkerBusinessLogic.Update(activityWorker);

            BindGridVeiwWorksByActivity();


          

            ScriptManager.RegisterClientScriptBlock(this,
                                        this.GetType(),
                                        "EditHideModalScript",
                                         Util.GetScriptToHideModalDialog("editModal"),
                                        false);



            this.txtEditDate.Text = string.Empty;
            this.txtEditWorkername.Text = string.Empty;
        }
        #endregion

        protected void btnAddActivityWorker_Click(object sender, EventArgs e)
        {
            if (lBoxWorkers.SelectedIndex <= -1)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("alert('É necessário selecionar um Colaborador')");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertSelectWorker", sb.ToString(), false);
                return;
            }




            int workerID = Convert.ToInt32(lBoxWorkers.SelectedValue);
            Activity activity = CurrentActivity();

            WorkerBusinessLogic workerBusinessLogic = new WorkerBusinessLogic();
            Worker worker = workerBusinessLogic.GetByID(workerID);
            hfNewActivityWorker.Value = workerID.ToString();

            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();

            double hoursAvaiable = activityBusinessLogic.GetAvaiablesHoursByWorkerOnDate(worker, activity.Date);


            this.txtAddDate.Text = activity.Date.ToShortDateString();
            this.txtAddWorkername.Text = worker.FullName;
            this.lblAddAvaiableHours.Text = hoursAvaiable.ToString();

            this.txtAddHours.Text = "0";
            this.txtAddComment.Text = string.Empty;


            ScriptManager.RegisterClientScriptBlock(
                                                      this,
                                                      this.GetType(),
                                                      "AddShowModalScript",
                                                      Util.GetScriptToShowModalDialog("addModal"),
                                                      false);


            BindDdListWorkers();



        }

        public void btnAddNewAtivityWorker_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = null;

            double hoursWorked;

            if (!Double.TryParse(txtAddHours.Text.Replace('.', ','), out hoursWorked) || hoursWorked <= 0)
            {
                this.lblAddErrorMessage.Text = "Valor das horas não é válido!";
                return;
            }


            if (string.IsNullOrWhiteSpace(txtAddComment.Text))
            {
                this.lblAddErrorMessage.Text = "É necessário informar um comentário!";
                return;
            }


            int workerID = Convert.ToInt32(hfNewActivityWorker.Value);

            Activity activity = CurrentActivity();

            WorkerBusinessLogic workerBusinessLogic = new WorkerBusinessLogic();
            Worker worker = workerBusinessLogic.GetByID(workerID);

            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();

            double hoursAvaiable = activityBusinessLogic.GetAvaiablesHoursByWorkerOnDate(worker, activity.Date);

            if (hoursWorked > hoursAvaiable)
            {
                this.lblAddErrorMessage.Text = "As horas trabalhas superam as horas dispónives!";
                return;
            }



            activityBusinessLogic.AttachWoker(activity, worker, hoursWorked, txtAddComment.Text);

         

            ScriptManager.RegisterClientScriptBlock(this,
                                      this.GetType(),
                                      "EditHideModalScript",
                                       Util.GetScriptToHideModalDialog("addModal"),
                                      false);

            BindDdListWorkers();
            BindGridVeiwWorksByActivity();
        }


    }
}