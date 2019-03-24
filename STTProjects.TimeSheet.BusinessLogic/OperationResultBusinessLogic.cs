using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.BusinessLogic
{
    internal class OperationResultBusinessLogic : IResultOperation
    {
       
        public OperationResultBusinessLogic(string message,
                                  ResultOperationStatus status)
            : this(message, status, null)
        {

        }
        public OperationResultBusinessLogic(string message,
                                  ResultOperationStatus status,
                                  Exception exception)
        {
            this.Message = message;
            this.Status = status;
            this.Exception = exception;
        }

        public string Message
        {
            get;
            protected set;
        }


        public ResultOperationStatus Status
        {
            get;
            protected set;
        }


        public Exception Exception
        {
            get;
            protected set;
        }
    }
}
