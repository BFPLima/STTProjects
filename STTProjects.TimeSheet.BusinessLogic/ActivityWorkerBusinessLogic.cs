using STTProjects.TimeSheet.DataAccessLayer;
using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.BusinessLogic
{
    public class ActivityWorkerBusinessLogic : BaseBusinessLogic<ActivityWorker>
    {
        public ActivityWorkerBusinessLogic()
        {
            base.baseDataAccessLayer = new ActivityWorkerDataAccessLayer();
        }

    }
}
