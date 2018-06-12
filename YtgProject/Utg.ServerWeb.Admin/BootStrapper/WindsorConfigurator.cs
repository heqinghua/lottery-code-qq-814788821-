using Castle.Facilities.WcfIntegration;
using Castle.Windsor.Installer;
using System.Reflection;
using Ytg.Comm;
using Ytg.Core.Security;
using Ytg.Core.Service;
using Ytg.Core.Service.Lott;
using Ytg.Data;
using Ytg.Service;
using Ytg.Service.Lott;


namespace Utg.ServerWeb.Admin
{
    public class WindsorConfigurator
    {
        public static void Configure()
        {
            WindsorRegistrar.Register(typeof(IDbContextFactory), typeof(DbContextFactory));//数据上下文
            WindsorRegistrar.Register(typeof(IFormsAuthentication), typeof(FormAuthService));//身份
            WindsorRegistrar.Register(typeof(IHasher), typeof(Hasher));//加密
            WindsorRegistrar.Register(typeof(IPlayNumTypeService), typeof(PlayNumTypeService));//加密
            

            WindsorRegistrar.RegisterAllFromAssemblies("Ytg.Core");
            WindsorRegistrar.RegisterAllFromAssemblies("Ytg.Data");
            WindsorRegistrar.RegisterAllFromAssemblies("Ytg.Service");
        }

    }
}