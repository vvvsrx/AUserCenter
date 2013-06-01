using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alipig.Data.Extensions;
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
    public class PermissionModule : NancyModule
    {
        public PermissionModule(ISiteService _siteService,
            IPermissionGroupService _permissionGroupService,
            IMiscService _miscService,
            IPermissionItemService _permissionItemService)
            : base("/permission")
        {
            PermissionGroupValidator permissionGroupValidator = new PermissionGroupValidator();
            PermissionItemValidator permissionItemValidator = new PermissionItemValidator();
            Get["/list"] = x =>
            {
                ViewBag.SiteId = Request.Query.siteId;
                ViewBag.SiteList = _siteService.GetAllSite();
                Guid siteid;
                if (!String.IsNullOrEmpty(Request.Query.siteId))
                {
                    siteid = new Guid(Request.Query.siteId);
                }
                else
                {
                    siteid = new Guid();
                }
                return View["Permission/List", _miscService.GetPermissionGroupItemsBySiteId(siteid)];
            };

            Get["/addgroup"] = x =>
            {
                ViewBag.SiteId = Request.Query.siteId;
                ViewBag.SiteList = _siteService.GetAllSite();
                return View["Permission/AddGroup", new PermissionGroup()];
            };

            Post["/addgroup"] = x =>
            {
                ViewBag.Errored = true;
                var permissiongroup = this.Bind<PermissionGroup>();
                ViewBag.SiteId = Request.Query.siteId;
                ViewBag.SiteList = _siteService.GetAllSite();
                ValidationResult results = permissionGroupValidator.Validate(permissiongroup);
                if (!results.IsValid)
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", results.Errors);
                }
                else if (_permissionGroupService.CreatePermissionGroup(permissiongroup))
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.success, "成功信息", "添加成功");
                }
                else
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", "未知错误,请联系管理员");
                }


                return View["Permission/AddGroup", permissiongroup];
            };

            Get["/additem"] = x =>
            {
                var permissiongroupid = Request.Query.permissiongroupid;
                var siteid = Request.Query.siteid;
                ViewBag.SiteId = siteid;
                ViewBag.PermissionGroupId = permissiongroupid;
                ViewBag.SiteName = _siteService.GetSite(new Guid(siteid)).SiteName;
                ViewBag.PermissionGroupList = _permissionGroupService.GetAllBySiteId(new Guid(siteid));
                return View["Permission/AddItem", new PermissionItem()];
            };

            Post["/additem"] = x =>
            {
                var permissiongroupid = Request.Query.permissiongroupid;
                var siteid = Request.Query.siteid;
                ViewBag.SiteId = siteid;
                ViewBag.Errored = true;
                ViewBag.PermissionGroupId = permissiongroupid;
                ViewBag.SiteName = _siteService.GetSite(new Guid(siteid)).SiteName;
                ViewBag.PermissionGroupList = _permissionGroupService.GetAllBySiteId(new Guid(siteid));

                var permissionitem = this.Bind<PermissionItem>();
                ValidationResult results = permissionItemValidator.Validate(permissionitem);
                if (!results.IsValid)
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", results.Errors);
                }
                else if (_permissionItemService.CreatePermissionItem(permissionitem))
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.success, "成功信息", "添加成功");
                }
                else
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", "未知错误,请联系管理员");
                }
                return View["Permission/AddItem", permissionitem];
            };



            Post["/deleteitem"] = x =>
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
                else
                {
                    try
                    {
                        _permissionItemService.DeletePermissionItem(ids);
                        result.code = NotyType.success.ToString();
                        result.msg = "删除成功!";
                    }
                    catch
                    {
                        result.code = NotyType.error.ToString();
                        result.msg = "删除失败!请联系管理员!";
                    }
                }
                return this.Response.AsJson<NotyResult>(result);
            };

            Post["/deletegroup"] = x =>
            {
                var result = new NotyResult();
                dynamic id = new Guid(Request.Form.id);
                id = id ?? new Guid();

                if (id == new Guid())
                {
                    result.code = NotyType.warning.ToString();
                    result.msg = "你没有选择!";
                }
                else
                {
                    try
                    {
                        _permissionGroupService.DeletePermissionGroup(id);
                        result.code = NotyType.success.ToString();
                        result.msg = "删除成功!";
                    }
                    catch
                    {
                        result.code = NotyType.error.ToString();
                        result.msg = "删除失败!请联系管理员!";
                    }
                }
                return this.Response.AsJson<NotyResult>(result);
            };



            Get["/viewgrouporder"] = x =>
            {
                var id = new Guid(Request.Query.siteId);
                return View["Permission/ViewGroupOrder", _permissionGroupService.GetAllBySiteId(id)];
            };

            Get["/viewitemorder"] = x =>
            {
                var id = new Guid(Request.Query.permissionGroupId);
                return View["Permission/ViewItemOrder", _permissionItemService.GetAllByPermissionGroupId(id)];
            };

            Post["/savegrouporder"] = x =>
            {
                var result = new NotyResult();
                Guid[] ids = RequestResultUtil.GetIdsByGuid(Request.Form.ids);
                var list = (ids ?? new Guid[0]);

                if (list.Length == 0)
                {
                    result.code = NotyType.warning.ToString();
                    result.msg = "你没有选择!";
                }
                else if (_permissionGroupService.SetOrderByIds(ids))
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

            Post["/saveitemorder"] = x =>
            {
                var result = new NotyResult();
                Guid[] ids = RequestResultUtil.GetIdsByGuid(Request.Form.ids);
                var list = (ids ?? new Guid[0]);

                if (list.Length == 0)
                {
                    result.code = NotyType.warning.ToString();
                    result.msg = "你没有选择!";
                }
                else if (_permissionItemService.SetOrderByIds(ids))
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

            Post["/editgroup"] = x =>
            {
                var result = new NotyResult();
                var id = new Guid(Request.Form.id);
                var name = Request.Form.name;

                if (_permissionGroupService.ModifyPermissionGroup(id,name))
                {
                    result.code = NotyType.success.ToString();
                    result.msg = "修改成功";
                }
                else
                {
                    result.code = NotyType.error.ToString();
                    result.msg = "修改失败!请联系管理员!";
                }
                return this.Response.AsJson<NotyResult>(result);
            };
        }
    }
}