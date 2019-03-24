using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.BusinessLogic
{
    public abstract class BaseRuleBusinessLogic<T> : BaseBusinessLogic<T> where T : BaseObject
    {

        public abstract IResultOperation InsertWithRule(T obj);


        public abstract IResultOperation UpdatetWithRule(T obj);


        public override void Insert(T obj)
        {
            IResultOperation result = InsertWithRule(obj);

            if (result.Status == ResultOperationStatus.OK)
            {
                return;
            }

            string message = string.Format("A entidade {0} não passou nas regras do método InsertWithRule "
                                              + Environment.NewLine
                                              + "{1}",
                                               this.GetType().ToString(),
                                               result.Message);

            throw new Exception(message, result.Exception);

        }

        public override void Update(T obj)
        {
            IResultOperation result = UpdatetWithRule(obj);

            if (result.Status == ResultOperationStatus.OK)
            {
                return;
            }

            string message = string.Format("A entidade {0} não passou nas regras do método UpdatetWithRule "
                                              + Environment.NewLine
                                              + "{1}",
                                               this.GetType().ToString(),
                                               result.Message);

            throw new Exception(message, result.Exception);
        }

    }
}
