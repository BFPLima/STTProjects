using STTProjects.TimeSheet.Model;
using STTProjects.TimeSheet.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.BusinessLogic
{
    public class ReporterBusinessLogic
    {
        public IList<ReportObjectHoursByProject> GetReportHoursByProject()
        {
            return GetReportHoursByProject(null);
        }


        public IList<ReportObjectHoursByProject> GetReportHoursByProject(DateTime? date)
        {

            ProjectBusinessLogic projectBusinessLogic = new ProjectBusinessLogic();
            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();

            IList<Project> projects = projectBusinessLogic.GetAll();

            if (date != null)
            {
                projects = projects.Where(p => p.Activities.Any(a => a.Date == date)).ToList();
            }

            IList<Activity> listActivitiesTotal = activityBusinessLogic.GetAll();



            IList<ReportObjectHoursByProject> list = new List<ReportObjectHoursByProject>();


            foreach (Project project in projects)
            {
                double hours = 0;
                IList<Activity> listActivities = null;

                if (date != null)
                {
                    listActivities = listActivitiesTotal.Where(a => a.Date == date && a.Project == project).ToList();
                }
                else
                {
                    listActivities = project.Activities;
                }

                foreach (Activity activity in listActivities)
                {
                    hours += activity.ActivityWorkers.Sum(aw => aw.Hours);
                }

                list.Add(new ReportObjectHoursByProject()
                {
                    ProjectName = project.Name,
                    TotalHours = hours
                });
            }

            return list;
        }

        public IList<Worker> GetByMajorWorkedHours(out double hours)
        {
            IList<Worker> list = null;

            ActivityWorkerBusinessLogic activityWorkerBusinessLogic = new ActivityWorkerBusinessLogic();
            IList<ActivityWorker> listActivityWorkers = activityWorkerBusinessLogic.GetAll();

            double d = listActivityWorkers.Max(aw => aw.Hours);

            listActivityWorkers = listActivityWorkers.Where(aw => aw.Hours == d).ToList();

            list = listActivityWorkers.Select(aw => aw.Worker).Distinct().ToList();
            hours = d;
            return list;
        }


        public IList<ReportObjectHoursByWorker> GetWorkedHours(DateTime date)
        {

            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();

            IList<Activity> listActivity = activityBusinessLogic.GetAll();

            listActivity = listActivity.Where(a => a.Date == date).ToList();

            List<Worker> listWorkers = new List<Worker>();
            foreach (var item in listActivity)
            {
                listWorkers.AddRange(item.GetWorkers().Distinct());
            }

            var listWorkersDistinct = listWorkers.Distinct();


            IList<ReportObjectHoursByWorker> list = new List<ReportObjectHoursByWorker>();

            foreach (Worker worker in listWorkersDistinct)
            {
                double hours = 0;
                foreach (Activity activity in listActivity)
                {
                    ActivityWorker activityWorker = activity.GetActivityWorkerByWorker(worker);
                    if (activityWorker != null)
                    {
                        hours += activityWorker.Hours;
                    }

                }

                list.Add(new ReportObjectHoursByWorker()
                            {
                                Name = worker.FullName,
                                Hours = hours
                            });
            }

            return list;
        }

    }
}
