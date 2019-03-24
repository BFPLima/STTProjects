
using STTProjects.TimeSheet.DataAccessLayer;
using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STTProjects.TimeSheet.BusinessLogic
{
    public abstract class BaseBusinessLogic<T> where T : BaseObject
    {
        protected BaseDataAccessLayer<T> baseDataAccessLayer;


        public virtual void Insert(T obj)
        {
            this.baseDataAccessLayer.Insert(obj);
        }

        public virtual void Delete(T obj)
        {
            this.baseDataAccessLayer.Delete(obj);
        }

        public virtual void Update(T obj)
        {
            this.baseDataAccessLayer.Update(obj);
        }

        public virtual T GetByID(int id)
        {
            return this.baseDataAccessLayer.GetByID<T>(id);
        }

        public virtual List<T> GetAll()
        {
            return this.baseDataAccessLayer.GetAll<T>();
        }

    }
}
