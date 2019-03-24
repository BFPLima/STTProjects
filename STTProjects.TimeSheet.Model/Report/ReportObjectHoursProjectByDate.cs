using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.Model.Report
{
    public class ReportObjectHoursProjectByDate : ReportObjectHoursByProject
    {
        public DateTime Date { get; set; }
    }
}
