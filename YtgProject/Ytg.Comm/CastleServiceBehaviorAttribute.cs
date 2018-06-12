using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;


namespace Ytg.Comm
{
    public class CastleServiceBehaviorAttribute : Attribute, IServiceBehavior
    {

        public CastleServiceBehaviorAttribute()
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
           
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                //    Type contractType = (from endpoint in serviceHostBase.Description.Endpoints
                //                         where endpoint.Contract.Name == endpointDispatcher.ContractName && endpoint.Contract.Namespace == endpointDispatcher.ContractNamespace
                //                         select endpoint.Contract.ContractType).FirstOrDefault();
                //    if (null == contractType)
                //    {
                //        continue;
                //    }

                //    IoC.Container.Register(Castle.MicroKernel.Registration.Component.For(contractType).LifeStyle.Transient);
                    
                    endpointDispatcher.DispatchRuntime.InstanceProvider = new CastleInstanceProvider(IoC.Container, serviceDescription.ServiceType);
                }
            }

        }

        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            
        }
    }
}