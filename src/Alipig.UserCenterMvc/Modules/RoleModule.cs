using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alipig.Data.Service;
using Alipig.Framework.Entities;
using Alipig.Framework.Entities.JsonModel;
using Alipig.Framework.Entities.Validator;
using Alipig.Framework.Util;
using FluentValidation.Results;
using Nancy;
using Nancy.ModelBinding;

namespace Alipig.UserCenterMvc.Modules
{
    public class RoleModule : NancyModule
    {
        public RoleModule(ISiteService _siteService,
            IRoleService _roleService)
            : base("/role")
        {
            RoleValidator roleValidator = new RoleValidator();

            Get["/list"] = x =>
            {
                ViewBag.SiteId = Request.Query.siteId;
                ViewBag.SiteList = _siteService.GetAllSite();
                if (!String.IsNullOrEmpty(Request.Query.siteId))
                {
                    return View["Role/List", _roleService.GetAllBySiteId(new Guid(Request.Query.siteId))];
                }
                else
                {
                    return View["Role/List",new List<Role>()];
                }
            };

            Get["/add"] = x =>
            {
                ViewBag.SiteId = Request.Query.siteId;
                ViewBag.SiteList = _siteService.GetAllSite();
                return View["Role/Add", new Role()];
            };

            Post["/add"] = x =>
            {
                ViewBag.SiteId = Request.Query.siteId;
                ViewBag.SiteList = _siteService.GetAllSite();
                ViewBag.Errored = true;

                var role = this.Bind<Role>();
                ValidationResult results = roleValidator.Validate(role);
                if (!results.IsValid)
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", results.Errors);
                }
                else if (_roleService.CreateRole(role))
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.success, "成功信息", "添加成功");
                }
                else
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", "未知错误,请联系管理员");
                }
                return View["Role/Add", role];
            };

            Get["/edit/{id}"] = x =>
            {
                ViewBag.SiteList = _siteService.GetAllSite();

                var model = _roleService.GetByAutoId(x.id);

                return View["Role/Edit", model];
            };

            Post["/edit/{id}"] = x =>
            {
                ViewBag.SiteList = _siteService.GetAllSite();
                ViewBag.Errored = true;

                var role = this.Bind<Role>();
                ValidationResult results = roleValidator.Validate(role);
                if (!results.IsValid)
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", results.Errors);
                }
                else if (_roleService.ModifyRole(role))
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.success, "成功信息", "修改成功");
                }
                else
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", "未知错误,请联系管理员");
                }
                return View["Role/Edit", role];
            };

            Get["/delete/{id}"] = x =>
            {
                var model = _roleService.GetByAutoId((int)x.id);
                _roleService.DeleteRole(model.ID);
                return Response.AsRedirect("/role/list?siteId=" + model.ID.ToString());
            };

            Post["/delete"] = x =>
            {
                var result = new NotyResult();
                Guid[] ids;
                try
                {
                    ids = RequestResultUtil.GetIdsByGuid(Request.Form.id);
                }
                catch
                {
                    Guid strongid = new Guid(Request.Form.id);
                    ids = new Guid[1];
                    ids[0] = strongid;
                }

                var list = (ids ?? new Guid[0]);

                if (list.Length == 0)
                {
                    result.code = NotyType.warning.ToString();
                    result.msg = "你没有选择!";
                }
                else if (_roleService.DeleteByIds(ids))
                {
                    result.code = NotyType.success.ToString();
                    result.msg = "删除成功!";
                }
                else
                {
                    result.code = NotyType.error.ToString();
                    result.msg = "删除失败!请联系管理员!";
                }
                return this.Response.AsJson<NotyResult>(result);
            };

            Get["/vieworder"] = x =>
            {
                var id = new Guid(Request.Query.siteId);
                return View["Role/ViewOrder", _roleService.GetAllBySiteId(id)];
            };

            Post["/saveorder"] = x =>
            {
                var result = new NotyResult();
                Guid[] ids = RequestResultUtil.GetIdsByGuid(Request.Form.ids);
                var list = (ids ?? new Guid[0]);

                if (list.Length == 0)
                {
                    result.code = NotyType.warning.ToString();
                    result.msg = "你没有选择!";
                }
                else if (_roleService.SetOrderByIds(ids))
                {
                    result.code = NotyType.success.ToString();
                    result.msg = "排序成功";
                }
                else
                {
                    result.code = NotyType.error.ToString();
                    result.msg = "排序失败!请联系管理员!";
                }
                return this.Response.AsJson<NotyResult>(result);
            };
        }
    }
}