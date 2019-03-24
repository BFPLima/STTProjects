using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.Model
{
    public class Activity : BaseObject
    {

        public Activity()
        {
            this.ActivityWorkers = new List<ActivityWorker>();
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual Project Project { get; set; }

        public virtual IList<ActivityWorker> ActivityWorkers { get; set; }

        public virtual IList<Worker> GetWorkers()
        {
            return this.ActivityWorkers.Where(w => w != null).Select(s => s.Worker).ToList();
        }

        public virtual ActivityWorker GetActivityWorkerByWorker(Worker worker)
        {
            ActivityWorker activityWorker = this.ActivityWorkers.Where(o => o.Worker == worker).SingleOrDefault();

            return activityWorker;
        }

    }
}
