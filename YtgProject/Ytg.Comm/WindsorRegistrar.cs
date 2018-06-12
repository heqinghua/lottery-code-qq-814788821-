using System;
using Castle.MicroKernel.Registration;

namespace Ytg.Comm
{
    public class WindsorRegistrar
    {
        public static void RegisterSingleton(Type interfaceType, Type implementationType)
        {
            IoC.Container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.Singleton);
        }
        
        public static void Register(Type interfaceType, Type implementationType)
        {
            IoC.Container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.PerWebRequest);
        }

        public static void RegisterAllFromAssemblies(string a)
        {
            IoC.Container.Register(Classes.FromAssemblyNamed(a).Pick().WithService.DefaultInterfaces().LifestylePerWebRequest());
        }
    }
}