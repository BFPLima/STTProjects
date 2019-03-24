using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.Model
{
    public enum ResultOperationStatus
    {
        OK = 1,
        Problem = 2
    }

    public interface IResultOperation
    {


        ResultOperationStatus Status { get; }
        string Message { get; }

        Exception Exception { get; }
    }
}
