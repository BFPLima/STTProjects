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
    public partial class WebFormReportHoursByProjectOnDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string dateString = this.Request.Params["Date"];
            DateTime date = Convert.ToDateTime(dateString);
            spanDate.InnerText = "Horas gastas por projeto na data : " + date.ToShortDateString();

            ReporterBusinessLogic reporterBusinessLogic = new ReporterBusinessLogic();
            IList<ReportObjectHoursByProject> list = reporterBusinessLogic.GetReportHoursByProject(date);

            GridView1.DataSource = list;
            GridView1.DataBind();
        }
    }
}