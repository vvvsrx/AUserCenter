using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Alipig.Data.Service;
using Alipig.Framework.Entities;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Extensions;
using Alipig.Framework.Entities.Validator;
using FluentValidation.Results;
using Alipig.Framework.Util;
using Alipig.Framework.Entities.JsonModel;

namespace Alipig.UserCenterMvc
{
    public class UserModule : NancyModule
    {
        public UserModule(IUserService _userService,
            ISiteService _siteService,
            IRoleService _roleService,
            IUserRoleMappingService _userRoleMappingService)
            : base("/user")
        {
            UserValidator uservalidator = new UserValidator();
            Get["/list"] = x =>
            {
                return View["User/List",_userService.GetAllUser()];
            };

            Get["/add"] = x =>
            {
                ViewBag.Errored = false;
                return View["User/Add", new User()];
            };

            Post["/add"] = x =>
            {
                User user = this.Bind<User>();
                ValidationResult results = uservalidator.Validate(user);
                if (!results.IsValid)
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error,"错误信息", results.Errors);
                    ViewBag.Errored = true;
                    return View["User/Add", user];
                }
                if (_userService.CreateUser(user))
                {
                    ViewBag.ErrorMsg = "<strong>OK~</strong>";
                    ViewBag.Errored = true;
                    return View["User/Add", user];
                }
                
                ViewBag.ErrorMsg = "<strong>出错啦~</strong>";
                ViewBag.Errored = true;
                return View["User/Add", user];
                //return this.Context.GetRedirect("~/user/add?error=true");
                //return View["User/Add"];
            };

            Get["/edit/{id}"] = x =>
            {
                return View["User/Edit", _userService.GetByAutoId((int)x.id)];
            };

            Post["/edit/{id}"] = x =>
            {
                ViewBag.Errored = true;
                var user = this.Bind<User>();
                var model = _userService.GetById(user.ID);
                if (!String.IsNullOrEmpty(user.Password))
                {
                    model.PasswordSalt = PasswordUtil.GenerateSalt();
                    model.Password = PasswordUtil.EncodePassword(user.Password, model.PasswordFormat, model.PasswordSalt);
                    model.passwordConfirm = PasswordUtil.EncodePassword(user.passwordConfirm, model.PasswordFormat, model.PasswordSalt);
                }
                else
                {
                    model.passwordConfirm = model.Password;
                }
                model.PrivateEmail = user.PrivateEmail;
                model.Nickname = user.Nickname;
                model.Gender = user.Gender;
                model.Status = user.Status;
                ValidationResult results = uservalidator.Validate(model);
                if (!results.IsValid)
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", results.Errors);
                    return View["User/Edit", user];
                }
                if (user.ID == Guid.Empty)
                {
                    return Response.AsRedirect("/user/list");
                }
                if (_userService.ModifyUser(model))
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.success, "成功信息", "修改用户信息成功");
                }
                else
                {
                    ViewBag.ErrorMsg = HtmlUtils.GetCharisma_Alert(Charisma_AlertType.error, "错误信息", "未知错误,请联系管理员");
                }
                return View["User/Edit", user];
            };

            Get["/delete/{id}"] = x =>
            {
                var model = _userService.GetByAutoId((int)x.id);
                _userService.DeleteUser(model.ID);
                return Response.AsRedirect("/user/list");
            };

            Get["/editrole"] = x =>
            {
                Guid userId = new Guid(Request.Query.userId);
                dynamic SiteId = null;
                SiteId = !String.IsNullOrEmpty(Request.Query.siteId.ToString()) && Request.Query.siteId != null ? new Guid(Request.Query.siteId) : Guid.Empty;
                var usermodel = _userService.GetById(userId);
                ViewBag.UserName = usermodel.UserName;
                ViewBag.UserId = usermodel.ID;
                ViewBag.SiteId = SiteId;
                ViewBag.SiteList = _siteService.GetAllSite();

                List<Role> siteRoles = _roleService.GetAllBySiteId(SiteId);
                List<Role> rightRoles = _roleService.GetAllBySiteIdAndUserId(SiteId, userId);
                ViewBag.NoSystemRoles = (SiteId != Guid.Empty && siteRoles.Count == 0);
                var leftRoles = (from p in siteRoles where !rightRoles.Exists(r => r.ID == p.ID) select p).ToList();
                ViewBag.LeftRoles = leftRoles;
                ViewBag.RightRoles = rightRoles;
                return View["User/EditRole"];
            };

            Post["/saverole"] = x =>
            {
                var result = new NotyResult();
                Guid userId = new Guid(Request.Form.userId);
                dynamic SiteId = null;
                SiteId = !String.IsNullOrEmpty(Request.Form.siteId.ToString()) && Request.Form.siteId != null ? new Guid(Request.Form.siteId) : Guid.Empty;
                Guid[] ids = RequestResultUtil.GetIdsByGuid(Request.Form.ids);

                var list = (ids ?? new Guid[0]);
                if (SiteId == Guid.Empty)
                {
                    result.code = NotyType.warning.ToString();
                    result.msg = "你没有选择站点!";
                }
                else
                {
                    List<Role> rightRoles = _roleService.GetAllBySiteIdAndUserId(SiteId, userId);
                    bool changed = false;
                    bool success = false;

                    List<object> added = new List<object>();
                    List<object> removed = new List<object>();

                    // 处理加角色的情况
                    foreach (Guid roleId in list)
                    {
                        if (!rightRoles.Exists(r=>r.ID == roleId))
                        {
                            changed = true;
                            success = _userRoleMappingService.CreateUserRoleMapping(userId, roleId);
                            added.Add(new { userId = userId, siteId = SiteId, roleId = roleId });
                        }
                    }

                    // 处理减角色的情况
                    foreach (Role role in rightRoles)
                    {
                        if (!list.ToList().Exists(r=>r == role.ID))
                        {
                            changed = true;
                            success = _userRoleMappingService.DeleteByUserIdAndRoleId(userId, role.ID);
                            removed.Add(new { userId = userId, siteId = SiteId, roleId = role.ID });
                        }
                    }

                    if (!changed || (changed && success))
                    {
                        result.code = NotyType.success.ToString();
                        result.msg = "保存成功!";
                    }
                    else
                    {
                        result.code = NotyType.error.ToString();
                        result.msg = "保存失败!请联系管理员!";
                    }
                }
                //if (list.Length == 0)
                //{
                //    result.code = NotyType.warning.ToString();
                //    result.msg = "你没有选择!";
                //}
                //else
                //{
                //    result.code = NotyType.success.ToString();
                //    result.msg = "排序成功";
                //}
                //else
                //{
                //    result.code = NotyType.error.ToString();
                //    result.msg = "排序失败!请联系管理员!";
                //}
                return this.Response.AsJson<NotyResult>(result);
            };
        }
    }
}