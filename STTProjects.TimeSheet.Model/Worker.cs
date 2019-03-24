using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.Model
{
    public class Worker : BaseObject
    {
        public virtual string Name { get; set; }

        public virtual string LastName { get; set; }


        public virtual string FullName
        {
            get
            {
                return this.Name + " " + this.LastName;
            }
        }


    }
}
