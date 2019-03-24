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
    public partial class WebFormReportMaijorWorkedHoursOnDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string dateString = this.Request.Params["Date"];
            DateTime date = Convert.ToDateTime(dateString);

            ReporterBusinessLogic reporterBusinessLogic = new ReporterBusinessLogic();

            IList<ReportObjectHoursByWorker> list = reporterBusinessLogic.GetWorkedHours(date);
            spanLabel.InnerText = "Quantidade de horas trabalhadas por colaborador na data  : " + date.ToShortDateString();

            string hoursString = this.Request.Params["Hours"];
            if (!string.IsNullOrWhiteSpace(hoursString))
            {
                double hours = Convert.ToDouble(hoursString);
                list = list.Where(w => w.Hours >= hours).ToList();

                spanLabel.InnerText += " com horas acima de : " + hours.ToString ("N2");
            }

            
            

            GridView1.DataSource = list;
            GridView1.DataBind();
        }
    }
}