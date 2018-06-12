using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Ytg.Comm
{
    public class CastleServiceHost : ServiceHost
    {
        public CastleServiceHost(Type serviceType,  params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
  
        }
        protected override void OnOpening()
        {
            base.OnOpening();
            if (this.Description.Behaviors.Find<CastleServiceBehaviorAttribute>() == null)
            {
                this.Description.Behaviors.Add(new CastleServiceBehaviorAttribute());
            }
        }
    }
}


