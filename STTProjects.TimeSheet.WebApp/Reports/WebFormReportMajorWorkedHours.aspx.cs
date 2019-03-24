using STTProjects.TimeSheet.BusinessLogic;
using STTProjects.TimeSheet.Model;
using STTProjects.TimeSheet.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STTProjects.TimeSheet.WebApp.Reports
{
    public partial class WebFormReportMajorWorkedHours : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
         

            ReporterBusinessLogic reporterBusinessLogic = new ReporterBusinessLogic();
            double hours = 0;
            IList<Worker> list = reporterBusinessLogic.GetByMajorWorkedHours(out hours);

            spanLabel.InnerText = "Colaboradores com mais esforço em horas de trabalho : " + hours.ToString("N2");

            GridView1.DataSource = list;
            GridView1.DataBind();
        }
    }
}