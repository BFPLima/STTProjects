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
    public partial class Workers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BindGridViewWorkers();

            this.gridViewWorkers.UseAccessibleHeader = true;       
        }

        private void BindGridViewWorkers()
        {
            WorkerBusinessLogic WorkerBusinessLogic = new WorkerBusinessLogic();

            gridViewWorkers.DataSource = WorkerBusinessLogic.GetAll();
            gridViewWorkers.DataBind();
        }

        protected void gridViewWorkers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("deleteWorker"))
            {
                DeleteRow(e);
            }
            else if (e.CommandName.Equals("editWorker"))
            {
                EditRow(e);
            }
        }


        #region Delete Region
        private void DeleteRow(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            string code = gridViewWorkers.DataKeys[index].Value.ToString();
            HfDeleteID.Value = code;

          
            ScriptManager.RegisterClientScriptBlock(this,
                                                    this.GetType(),
                                                    "DeleteModalScript",
                                                    Util.GetScriptToShowModalDialog("deleteModal"),
                                                    false);

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {

            WorkerBusinessLogic WorkerBusinessLogic = new WorkerBusinessLogic();
            Worker Worker = WorkerBusinessLogic.GetByID(Convert.ToInt32(HfDeleteID.Value));

            WorkerBusinessLogic.Delete(Worker);
            

            ScriptManager.RegisterClientScriptBlock(this,
                                                   this.GetType(),
                                                   "delHideModalScript",
                                                   Util.GetScriptToHideModalDialog("deleteModal"),
                                                   false);


            BindGridViewWorkers();
        }

        #endregion

        #region Update Region
        private void EditRow(GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow gvrow = gridViewWorkers.Rows[index];

            HfUpdateID.Value = HttpUtility.HtmlDecode(gvrow.Cells[0].Text);

            txtEditWorkerID.Text = HttpUtility.HtmlDecode(gvrow.Cells[0].Text);

            txtEditWorkerName.Text = HttpUtility.HtmlDecode(gvrow.Cells[1].Text);

            txtEditWorkerLastName.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text);



            lblResult.Visible = false;

          

            ScriptManager.RegisterClientScriptBlock(this,
                                                   this.GetType(),
                                                   "EditModalScript",
                                                   Util.GetScriptToShowModalDialog("editModal"),
                                                   false);


            lblEditWorkerMessage.Text = string.Empty;


        }


        protected void btnUpdateWorker_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtEditWorkerName.Text))
            {
                lblEditWorkerMessage.Text = "O nome deve ser preenchido!";
                return;
            }


            if (string.IsNullOrWhiteSpace(this.txtEditWorkerLastName.Text))
            {
                lblEditWorkerMessage.Text = "o Sobrenome deve ser preenchido!";
                return;
            }



            WorkerBusinessLogic workerBusinessLogic = new WorkerBusinessLogic();
            Worker worker = workerBusinessLogic.GetByID(Convert.ToInt32(HfUpdateID.Value));

            string currentName = worker.Name;
      
            worker.Name = txtEditWorkerName.Text;
            worker.LastName = txtEditWorkerLastName.Text;

            IResultOperation result = workerBusinessLogic.UpdatetWithRule(worker);

            if (result.Status != ResultOperationStatus.OK)
            {
                lblEditWorkerMessage.Text = result.Message;
                worker.Name = currentName;
                return;
            }   


            BindGridViewWorkers();

         
            ScriptManager.RegisterClientScriptBlock(this,
                                                this.GetType(),
                                                "EditHideModalScript",
                                                Util.GetScriptToHideModalDialog("editModal"),
                                                false);

            lblEditWorkerMessage.Text = string.Empty;
        }
        #endregion

        #region Create Region
        protected void btnAddWorkerModal_Click(object sender, EventArgs e)
        {            

            lblNewWorkerMessage.Text = string.Empty;
            txtNewWorkerName.Text = string.Empty;
            txtNewWorkerLastName.Text = string.Empty;

            

            ScriptManager.RegisterClientScriptBlock(this,
                                               this.GetType(),
                                               "AddShowModalScript",
                                               Util.GetScriptToShowModalDialog("addModal"),
                                               false);

           
        }

        protected void btnAddNewWorker_Click(object sender, EventArgs e)
        {
         

            if (string.IsNullOrWhiteSpace(this.txtNewWorkerName.Text))
            {
                this.lblNewWorkerMessage.Text = "O nome deve ser preenchido!";
                return;
            }


            if (string.IsNullOrWhiteSpace(this.txtNewWorkerLastName.Text))
            {
                this.lblNewWorkerMessage.Text = "o Sobrenome deve ser preenchido!";
                return;
            }



            WorkerBusinessLogic workerBusinessLogic = new WorkerBusinessLogic();
            Worker Worker = new Worker();

          
            Worker.Name = txtNewWorkerName.Text;
            Worker.LastName = txtNewWorkerLastName.Text;

            IResultOperation result = workerBusinessLogic.InsertWithRule(Worker);

            if (result.Status != ResultOperationStatus.OK)
            {
                lblNewWorkerMessage.Text = result.Message;
                return;
            }

            BindGridViewWorkers();

           

            ScriptManager.RegisterClientScriptBlock(this,
                                            this.GetType(),
                                            "AddHideModalScript",
                                            Util.GetScriptToHideModalDialog("addModal"),
                                            false);


            this.txtNewWorkerName.Text = string.Empty;
            this.txtNewWorkerLastName.Text = string.Empty;
            this.lblNewWorkerMessage.Text = string.Empty;
        }


        #endregion

        protected void gridViewWorkers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gridViewWorkers.PageIndex = e.NewPageIndex;
            BindGridViewWorkers();
        }




    }
}