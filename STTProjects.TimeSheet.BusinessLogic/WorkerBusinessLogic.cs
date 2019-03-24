using STTProjects.TimeSheet.DataAccessLayer;
using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.BusinessLogic
{
    public class WorkerBusinessLogic : BaseRuleBusinessLogic<Worker>
    {
        public WorkerBusinessLogic()
        {
            base.baseDataAccessLayer = new WorkerDataAccessLayer();
        }

        public override IResultOperation InsertWithRule(Worker obj)
        {
            IResultOperation result = null;
            string message = string.Empty;
            obj.Name = obj.Name.Trim();

            IList<Worker> list = this.GetAll().Where(p => p.Name.Equals(obj.Name)).ToList();

            if (list.Count() >= 1)
            {
                message = "Esse nome já existe!";
                result = new OperationResultBusinessLogic(message, ResultOperationStatus.Problem);
                return result;
            }

            this.baseDataAccessLayer.Insert(obj);

            result = new OperationResultBusinessLogic("Colaborador cadastrado com sucesso", ResultOperationStatus.OK);

            return result;
        }

        public override IResultOperation UpdatetWithRule(Worker obj)
        {
            IResultOperation result = null;
            string message = string.Empty;
            obj.Name = obj.Name.Trim();

            IList<Worker> list = this.GetAll().Where(p => p.Name.Equals(obj.Name) && p.ID != obj.ID).ToList();

            if (list.Count() >= 1)
            {
                message = "Esse nome já existe!";
                result = new OperationResultBusinessLogic(message, ResultOperationStatus.Problem);
                return result;
            }

            this.baseDataAccessLayer.Insert(obj);

            result = new OperationResultBusinessLogic("Colaborador cadastrado com sucesso", ResultOperationStatus.OK);

            return result;
        }

        public override void Delete(Worker obj)
        {
            ActivityWorkerBusinessLogic activityWorkerBusinessLogic = new ActivityWorkerBusinessLogic();
            ActivityBusinessLogic activityBusinessLogic = new ActivityBusinessLogic();

            var listWorked = activityWorkerBusinessLogic.GetAll().Where(o => o.Worker == obj).ToList();

            foreach (var item in listWorked)
            {
                activityBusinessLogic.DetachWoker(item.Activity, item.Worker);
            }

            this.baseDataAccessLayer.Delete(obj);
        }




    }
}
