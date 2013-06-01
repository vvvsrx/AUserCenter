using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alipig.Data.Service;
using Alipig.Framework.Entities;
using Nancy.ModelBinding;
using Nancy;
using Alipig.Framework.Entities.Validator;
using FluentValidation.Results;
using Alipig.Framework.Util;
using Alipig.Framework.Entities.JsonModel;

namespace Alipig.UserCenterMvc
{
    public class SiteModule : NancyModule
    {
        public SiteModule(ISiteService _siteService)
            : base("/site")
        {
            SiteValidator sitevalidator = new SiteValidator();
            Get["/list"] = x =>
            {
                return View["Site/List",_siteService.GetAllSite()];
            };

            Get["/add"] = x =>
            {
                return View["Site/Add",new Site()];
            };

            Post["/add"] = x =>
            {
                var site = this.Bind<Site>();
                ViewBag.Errored = true;
                ValidationResult results = sitevalidator.Validate(site);
                if (!results.IsValid)
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", results.Errors);
                }
                else if (_siteService.CreateSite(site))
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.success, "成功信息","添加网站成功");
                }
                else
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", "未知错误,请联系管理员");
                }
                return View["Site/Add",site];
            };


            Get["/edit/{id}"] = x =>
            {
                var model = _siteService.GetSite((int)x.id);
                return View["Site/Edit", model];
            };

            Post["/edit/{id}"] = x =>
            {
                var site = this.Bind<Site>();
                ViewBag.Errored = true;
                ValidationResult results = sitevalidator.Validate(site);
                if (!results.IsValid)
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", results.Errors);
                }
                else if (_siteService.ModifySite(site))
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.success, "成功信息", "修改网站成功");
                }
                else
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", "未知错误,请联系管理员");
                }
                return View["Site/Edit", site];
            };

            Get["/delete/{id}"] = x =>
            {
                _siteService.DeleteSite((int)x.id);
                return Response.AsRedirect("/site/list");
            };

            Get["/editdomain/{id}"] = x =>
            {
                var model = _siteService.GetSite((int)x.id);
                return View["Site/EditDomain", model];
            };

            Post["/adddomain"] = x =>
            {
                var domainName = Request.Form.domain.ToString();
                var id = new Guid(Request.Form.id.ToString());
                var result = new NotyResult();

                if (String.IsNullOrEmpty(domainName))
                {
                    result.code = NotyType.warning.ToString();
                    result.msg = "域名不能为空!";
                }
                else if (!_siteService.IsDomainOnly(id,domainName))
                {
                    result.code = NotyType.warning.ToString();
                    result.msg = "域名已存在!";
                }
                else if (_siteService.AddDomain(id,domainName))
                {
                    result.code = NotyType.success.ToString();
                    result.msg = "添加成功";
                }
                else
                {
                    result.code = NotyType.error.ToString();
                    result.msg = "添加失败!请联系管理员!";
                }
                return this.Response.AsJson<NotyResult>(result);
            };
        }
    }
}