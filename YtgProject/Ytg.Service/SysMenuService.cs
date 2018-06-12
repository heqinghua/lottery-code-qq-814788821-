using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Ytg.BasicModel;
using Ytg.Core.Repository;
using Ytg.Core.Service;

namespace Ytg.Service
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SysMenuService : CrudService<SysMenu>, ISysMenuService
    {
        public SysMenuService(IRepo<SysMenu> repo)
            : base(repo)
        {
        }

        public List<SysMenu> GetAllMenu()
        {
            return this.GetAll().ToList();
        }


        public bool UpdateMenu(SysMenu item)
        {
            var c = this.Get(item.Id);
            if (null == c)
                return false;
            c.IsVisible = item.IsVisible;
            c.MenuName = item.MenuName;
            c.ParentId = item.ParentId;
            c.URL = item.URL;
            c.Description = item.Description;
            this.Save();
            return true;
        }

        public bool AddMenu(SysMenu item)
        {
            this.Create(item);
            this.Save();
            return true;
        }

        public bool Del(int id)
        {
            this.Delete(id);
            return true;
        }
    }
}
