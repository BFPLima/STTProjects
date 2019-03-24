using STTProjects.TimeSheet.DataAccessLayer;
using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.BusinessLogic
{
    public class ProjectBusinessLogic : BaseRuleBusinessLogic<Project>
    {
        public ProjectBusinessLogic()
        {
            this.baseDataAccessLayer = new ProjectDataAccessLayer();
        }


        public override IResultOperation InsertWithRule(Project obj)
        {
            IResultOperation result = null;
            string message = string.Empty;
            obj.Name = obj.Name.Trim();

            IList<Project> list = this.GetAll().Where(p => p.Name.Equals(obj.Name)).ToList();

            if (list.Count() >= 1)
            {
                message = "Esse nome de projeto já existe!";
                result = new OperationResultBusinessLogic(message, ResultOperationStatus.Problem);
                return result;
            }

            this.baseDataAccessLayer.Insert(obj);

            result = new OperationResultBusinessLogic("Projeto inserido com sucesso", ResultOperationStatus.OK);

            return result;
        }

        public override IResultOperation UpdatetWithRule(Project obj)
        {
            IResultOperation result = null;
            string message = string.Empty;
            obj.Name = obj.Name.Trim();

            IList<Project> list = this.GetAll().Where(p => p.Name.Equals(obj.Name) && p.ID != obj.ID).ToList();

            if (list.Count() >= 1)
            {
                message = "Esse nome de projeto já existe!";
                result = new OperationResultBusinessLogic(message, ResultOperationStatus.Problem);
                return result;
            }

            this.baseDataAccessLayer.Insert(obj);

            result = new OperationResultBusinessLogic("Projeto atualizado com sucesso", ResultOperationStatus.OK);

            return result;
        }


    }
}
