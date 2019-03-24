using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.Model
{
    public class Project : BaseObject
    {
        public Project() 
        {
            this.Activities = new List<Activity>();
        }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }


        public virtual IList<Activity> Activities { get; set; }




        public virtual void AttachActivity(Activity activity)
        {
            if (!this.Activities.Contains(activity))
            {
                this.Activities.Add(activity);
                activity.Project = this;
            }
        }

        public virtual void DetachActivity(Activity activity)
        {
            if (this.Activities.Contains(activity))
            {
                this.Activities.Remove(activity);
                activity.Project = null;
            }
        }
    }
}
