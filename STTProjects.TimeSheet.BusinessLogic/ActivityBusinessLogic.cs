using STTProjects.TimeSheet.DataAccessLayer;
using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.BusinessLogic
{


    public class ActivityBusinessLogic : BaseBusinessLogic<Activity>
    {

        protected static double maxWorkHoursByDay = -1;
        public ActivityBusinessLogic()
        {
            base.baseDataAccessLayer = new ActivityDataAccessLayer();
        }

        public override void Delete(Activity obj)
        {
            obj.Project.DetachActivity(obj);
            base.Delete(obj);
        }


        public double GetAvaiablesHoursByWorkerOnDate(Worker worker, DateTime date)
        {
            ActivityWorkerBusinessLogic activityWorkerBusinessLogic = new ActivityWorkerBusinessLogic();

            IList<ActivityWorker> list = activityWorkerBusinessLogic.GetAll().Where(o => o.Worker == worker && o.Activity.Date == date).ToList();

            double workedHours = list.Sum(o => o.Hours);

            double avaiableHours = MaxWorkHoursByDay() - workedHours;

            return avaiableHours;
        }

        public ActivityWorker AttachWoker(Activity activity, Worker worker, double hours, string comment)
        {
            ActivityWorkerBusinessLogic activityWorkerBusinessLogic = new ActivityWorkerBusinessLogic();
            ActivityWorker activityWorker = null;


            if (activity.GetWorkers().Contains(worker))
            {
                activityWorker = activity.GetActivityWorkerByWorker(worker);
                if (activityWorker.Hours != hours)
                {
                    activityWorker.Hours = hours;
                    activityWorkerBusinessLogic.Update(activityWorker);
                }

                return activityWorker;
            }


            activityWorker = new ActivityWorker()
            {
                Activity = activity,
                Worker = worker,
                Hours = hours,
                Comment = comment
            };

            activityWorkerBusinessLogic.Insert(activityWorker);

            activity.ActivityWorkers.Add(activityWorker);

            return activityWorker;

        }

        public void DetachWoker(Activity activity, Worker worker)
        {
            if (!activity.GetWorkers().Contains(worker))
            {
                return;
            }


            ActivityWorker activityWorker = activity.GetActivityWorkerByWorker(worker);

            activity.ActivityWorkers.Remove(activityWorker);

            ActivityWorkerBusinessLogic activityWorkerBusinessLogic = new ActivityWorkerBusinessLogic();

            activityWorkerBusinessLogic.Delete(activityWorker);

        }


        public static double MaxWorkHoursByDay()
        {
            if (maxWorkHoursByDay <= -1)
            {
                maxWorkHoursByDay = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["MAX_WORK_HOURS_BY_DAY"]);
            }

            return maxWorkHoursByDay;
        }
    }
}
