using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace Ytg.Comm
{
    public class CastleInstanceProvider : IInstanceProvider
    {
        readonly IWindsorContainer mContainer;

        readonly Type mServiceType;

        public CastleInstanceProvider(IWindsorContainer container, Type serviceType)
        {
            this.mContainer = container;
            this.mServiceType = serviceType;
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            try
            {
                var item = mContainer.Resolve(mServiceType);
                return item;
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(System.ServiceModel.InstanceContext instanceContext, object instance)
        {
            if (instance is IDisposable)
                ((IDisposable)instance).Dispose();
        }
    }
}