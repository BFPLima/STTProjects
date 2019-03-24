using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.Model
{
    public class ActivityWorker : BaseObject
    {

        public virtual double Hours { get; set; }
        public virtual Activity Activity { get; set; }

        public virtual Worker Worker { get; set; }

        public virtual string Comment { get; set; }
    }
}
