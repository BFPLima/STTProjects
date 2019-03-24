
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;


namespace STTProjects.TimeSheet.DataAccessLayer
{

    public class NHibernateHelper
    {

        private static ISessionFactory sessionFactory = null;
        private static ISession session = null;

 
        public static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    Configuration configuration = new Configuration();
                    configuration.Configure();
               
                    sessionFactory = configuration.BuildSessionFactory();
                }
                return sessionFactory;
            }
        }


        public void CreateSession()
        {
            CurrentSessionContext.Bind(SessionFactory.OpenSession());
        }

      
        public void CloseSession()
        {
            if (CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Unbind(SessionFactory).Dispose();
            }
        }
 
        public static ISession GetCurrentSession()
        {
            if (session == null)
            {
                session = SessionFactory.OpenSession();
            }

            return session;
           
        }
 
    }
}



