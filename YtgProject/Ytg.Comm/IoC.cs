using System;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Facilities.WcfIntegration;

namespace Ytg.Comm
{
    public static class IoC
    {
        private static readonly object LockObj = new object();

        private static IWindsorContainer container = new WindsorContainer(new XmlInterpreter("windsor.xml")).AddFacility<WcfFacility>(f => f.CloseTimeout = TimeSpan.Zero);



        public static IWindsorContainer Container
        {
            get { return container; }

            set
            {
                lock (LockObj)
                {
                    container = value;
                }
            }
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }

      

        public static object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}