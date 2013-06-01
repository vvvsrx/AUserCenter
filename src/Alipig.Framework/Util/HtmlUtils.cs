using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.Web.Html;
using Alipig.Framework.Util;
using Alipig.Framework.Web;
using System.Web;
using Nancy.ViewEngines.Razor;
using HtmlTags;
using FluentValidation.Results;

namespace Alipig.Framework.Util
{
    public static class HtmlUtils
    {
        public static Nancy.ViewEngines.Razor.IHtmlString UserGenderToText(UserGender gender)
        {
            return new NonEncodedHtmlString(UserGender.Male.ToText());
        }

        public static Nancy.ViewEngines.Razor.IHtmlString PermissionItemDisplayStyleToText(PermissionItemDisplayStyle displaystyle)
        {
            return new NonEncodedHtmlString(displaystyle.ToText());
        }

        public static string ToText(this UserGender gender)
        {
            switch (gender)
            {
                case UserGender.Male:
                    return "男";
                case UserGender.Female:
                    return "女";
                default:
                    throw new Exception("Wrong value of UserGender.");
            }
        }

        public static string ToText(this UserAccountStatuses status)
        {
            switch (status)
            {
                case UserAccountStatuses.ApprovalPending:
                    return "待审批";
                case UserAccountStatuses.Approved:
                    return "正常";
                case UserAccountStatuses.Banned:
                    return "禁用";
                case UserAccountStatuses.Disapproved:
                    return "未批准";
                case UserAccountStatuses.NotActive:
                    return "未激活";
                default:
                    return "未知";
            }
        }

        public static string ToText(this PermissionItemDisplayStyle displayStyle)
        {
            switch (displayStyle)
            {
                case PermissionItemDisplayStyle.CheckBox:
                    return "单选框";
                case PermissionItemDisplayStyle.TextBox:
                    return "文本框";
                case PermissionItemDisplayStyle.DropDownList:
                    return "下拉框";
                case PermissionItemDisplayStyle.TreeView:
                    return "树视图";
                default:
                    throw new Exception("Wrong value of PermissionItemDisplayStyle.");
            }
        }

        public static string ToText(this Charisma_AlertType type)
        {
            switch (type)
            {
                case Charisma_AlertType.error:
                    return "alert-error";
                case Charisma_AlertType.success:
                    return "alert-success";
                case Charisma_AlertType.info:
                    return "alert-info";
                case Charisma_AlertType.block:
                    return "alert-block";
                default:
                    return "";
            }
        }

        public static Nancy.ViewEngines.Razor.IHtmlString UserStatusesToSpan(UserAccountStatuses status)
        {
            var tag = new HtmlTag("span").AddClass("label");

            switch (status)
            {
                case UserAccountStatuses.ApprovalPending:
                    tag.Text(UserAccountStatuses.ApprovalPending.ToText());
                    break;
                case UserAccountStatuses.Approved:
                    tag.AddClass("label-success").Text(UserAccountStatuses.Approved.ToText());
                    break;
                case UserAccountStatuses.Banned:
                    tag.AddClass("label-important").Text(UserAccountStatuses.Banned.ToText());
                    break;
                case UserAccountStatuses.Disapproved:
                    tag.AddClass("label-warning").Text(UserAccountStatuses.Disapproved.ToText());
                    break;
                case UserAccountStatuses.NotActive:
                    tag.AddClass("label-inverse").Text(UserAccountStatuses.NotActive.ToText());
                    break;
                default:
                    tag.AddClass("label-info").Text("未知");
                    break;
            }
            return new NonEncodedHtmlString(tag.ToString());
        }

        public static IList<SelectListItem> GetUserGenderSelectList(object selectedValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (selectedValue == null)
            {
                selectedValue = UserGender.Male;
            }

            list.Add(new SelectListItem { Text = UserGender.Male.ToText(), Value = UserGender.Male.ToString(), Selected = UserGender.Male == (UserGender)selectedValue });
            list.Add(new SelectListItem { Text = UserGender.Female.ToText(), Value = UserGender.Female.ToString(), Selected = UserGender.Female == (UserGender)selectedValue });

            return list;
        }

        public static IList<SelectListItem> GetPermissionItemDisplayStyleSelectList(object selectedValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (selectedValue == null)
            {
                selectedValue = PermissionItemDisplayStyle.CheckBox;
            }

            list.Add(new SelectListItem 
            { 
                Text = PermissionItemDisplayStyle.CheckBox.ToText(), 
                Value = PermissionItemDisplayStyle.CheckBox.ToString(), 
                Selected = PermissionItemDisplayStyle.CheckBox == (PermissionItemDisplayStyle)selectedValue
            });

            list.Add(new SelectListItem
            {
                Text = PermissionItemDisplayStyle.DropDownList.ToText(),
                Value = PermissionItemDisplayStyle.DropDownList.ToString(),
                Selected = PermissionItemDisplayStyle.DropDownList == (PermissionItemDisplayStyle)selectedValue
            });

            list.Add(new SelectListItem
            {
                Text = PermissionItemDisplayStyle.TextBox.ToText(),
                Value = PermissionItemDisplayStyle.TextBox.ToString(),
                Selected = PermissionItemDisplayStyle.TextBox == (PermissionItemDisplayStyle)selectedValue
            });

            list.Add(new SelectListItem
            {
                Text = PermissionItemDisplayStyle.TreeView.ToText(),
                Value = PermissionItemDisplayStyle.TreeView.ToString(),
                Selected = PermissionItemDisplayStyle.TreeView == (PermissionItemDisplayStyle)selectedValue
            });

            return list;
        }

        public static IList<SelectListItem> GetUserStatusSelectList(object selectedValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (selectedValue == null)
            {
                selectedValue = UserAccountStatuses.NotActive;
            }

            foreach (UserAccountStatuses item in Enum.GetValues(typeof(UserAccountStatuses)))
            {
                list.Add(new SelectListItem { Text = item.ToText(), Value = item.ToString(), Selected = item == (UserAccountStatuses)selectedValue });
            }

            //foreach (var item in sitelist.Value)
            //{
            //    list.Add(new SelectListItem { Text = item.SiteName, Value = item.ID.ToString(), Selected = item.ID.ToString().Equals(selectedValue) });
            //}

            return list;
        }

        public static IList<SelectListItem> GetSiteSelectList(dynamic sitelist,object selectedValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (selectedValue == null)
            {
                selectedValue = "";
            }
            list.Add(new SelectListItem { Text = "---请选择---", Value = "" });

            foreach (var item in sitelist.Value)
            {
                list.Add(new SelectListItem { Text = item.SiteName, Value = item.ID.ToString(), Selected = item.ID.ToString().Equals(selectedValue) });
            }

            return list;
        }

        public static IList<SelectListItem> GetRoleSelectList(dynamic sitelist, object selectedValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (selectedValue == null)
            {
                selectedValue = "";
            }

            foreach (var item in sitelist.Value)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString(), Selected = item.ID.ToString().Equals(selectedValue) });
            }

            return list;
        }

        public static IList<SelectListItem> GetPermissionGroupSelectList(dynamic sitelist, object selectedValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (selectedValue == null)
            {
                selectedValue = "";
            }
            list.Add(new SelectListItem { Text = "---请选择---", Value = "" });

            foreach (var item in sitelist.Value)
            {
                list.Add(new SelectListItem { Text = item.Name, Value = item.ID.ToString(), Selected = item.ID.ToString().Equals(selectedValue) });
            }

            return list;
        }

        public static string GetCharisma_Alert(Charisma_AlertType type, string title, string msg)
        {
            var tag = new HtmlTag("div").AddClass("alert").AddClass(type.ToText());
            tag.Add("button").AddClass("close").Attr("type", "button").Attr("data-dismiss", "alert").Text("×");
            tag.Add("h4").AddClass("alert-heading").Text(title);
            tag.Add("p").Text(msg);
            return tag.ToString();
        }

        public static string GetCharisma_Alert(Charisma_AlertType type, string title, IList<ValidationFailure> content)
        {
            var tag = new HtmlTag("div").AddClass("alert").AddClass(type.ToText());
            tag.Add("button").AddClass("close").Attr("type", "button").Attr("data-dismiss", "alert").Text("×");
            tag.Add("h4").AddClass("alert-heading").Text(title);
            foreach (var item in content)
            {
                tag.Add("p").Text(item.ErrorMessage);
            }
            return tag.ToString();
        }

        public static Nancy.ViewEngines.Razor.IHtmlString DisplayHtml(string html)
        {
            return new NonEncodedHtmlString(html);
        }

        public static List<PermissionItem> GetOrderedItems(PermissionGroupItemsModel permissionGroupItems)
        {
            return permissionGroupItems.GetItemsByDisplayStyle(PermissionItemDisplayStyle.CheckBox)
                .Union(permissionGroupItems.GetItemsByDisplayStyle(PermissionItemDisplayStyle.TextBox))
                .Union(permissionGroupItems.GetItemsByDisplayStyle(PermissionItemDisplayStyle.DropDownList))
                .Union(permissionGroupItems.GetItemsByDisplayStyle(PermissionItemDisplayStyle.TreeView)).ToList();
        }

        public static List<PermissionItem> GetItemsByDisplayStyle(this PermissionGroupItemsModel permissionGroupItems, PermissionItemDisplayStyle displayStyle)
        {
            var list = from p in permissionGroupItems.Items where p.DisplayStyle == displayStyle select p;

            return list.ToList();
        }
    }
}
