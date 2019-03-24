

using STTProjects.TimeSheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NHibernate;
using NHibernate.Linq;

using NHibernate.Cfg;
using System.Collections;

using System.Data;
using System.Reflection;
using NHibernate.Criterion;
using STTProjects.TimeSheet.DataAccessLayer;



namespace STTProjects.TimeSheet.DataAccessLayer
{
    public abstract class DataAccess<T> : IDisposable where T : BaseObject
    {
        protected NHibernateHelper nHibernateHelper;

        protected  ISession nhSession = null;
        public DataAccess()
        {
            this.nhSession = NHibernateHelper.GetCurrentSession();
        }

        public void Insert(T obj)
        {
            this.nhSession.Save(obj);
            this.nhSession.Flush();            
        }

        public void Delete(T obj)
        {
            this.nhSession.Delete(obj);
            this.nhSession.Flush();
        }

        public void Update(T obj)
        {
            this.nhSession.Update(obj);
            this.nhSession.Flush();
        }

        public T GetByID<T>(int id)
        {
            return this.nhSession.Get<T>(id);
        }

        public List<T> GetAll<T>()
        {
            return this.nhSession.Query<T>().ToList();
            
        }
        
        public void Dispose()
        {
            if (this.nhSession != null)
            {
                this.nhSession.Close();
                this.nhSession.Dispose();
                this.nhSession = null;
            }

            GC.Collect();
        }

       
    }
}