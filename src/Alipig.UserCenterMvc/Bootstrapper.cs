using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alipig.Data.Extensions;
using Alipig.Data.Repositories;
using Alipig.Data.Service;
using Alipig.Framework.NHHelper;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.ViewEngines.Razor;

namespace Alipig.UserCenterMvc
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            NHibernateHelper.sessionStorage = new HttpSessionStorage();
            SetDI(container);
        }




        public void SetDI(TinyIoCContainer container)
        {
            //container.Register<IRazorConfiguration, CustomRazorConfiguration>();
            #region Repositories
            container.Register<IPermissionGroupRepository, PermissionGroupRepository>();
            container.Register<IPermissionItemRepository, PermissionItemRepository>();
            container.Register<IRoleRepository, RoleRepository>();
            container.Register<ISiteRepository, SiteRepository>();
            container.Register<IUserPermissionRepository, UserPermissionRepository>();
            container.Register<IUserRepository, UserRepository>();
            container.Register<IUserRoleMappingRepository, UserRoleMappingRepository>();
            #endregion
            #region Service
            container.Register<IPermissionGroupService, PermissionGroupService>();
            container.Register<IPermissionItemService, PermissionItemService>();
            container.Register<IRoleService, RoleService>();
            container.Register<ISiteService, SiteService>();
            container.Register<IUserPermissionService, UserPermissionService>();
            container.Register<IUserRoleMappingService, UserRoleMappingService>();
            container.Register<IUserService, UserService>();
            #endregion
            container.Register<IMiscService, MiscService>();
        }
    }
}