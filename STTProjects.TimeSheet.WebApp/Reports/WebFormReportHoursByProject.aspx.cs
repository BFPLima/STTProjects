using STTProjects.TimeSheet.BusinessLogic;
using STTProjects.TimeSheet.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STTProjects.TimeSheet.WebApp.Reports
{
    public partial class WebFormReportHoursByProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ReporterBusinessLogic reporterBusinessLogic = new ReporterBusinessLogic();
            IList<ReportObjectHoursByProject> list =  reporterBusinessLogic.GetReportHoursByProject();

            GridView1.DataSource = list;
            GridView1.DataBind();
        }
    }
}