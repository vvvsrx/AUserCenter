using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Routing;
using HtmlTags;
using Nancy.ViewEngines.Razor;

namespace Alipig.Framework.Web.Html
{
    public static class SelectHelpers
    {
        public static IHtmlString DropDownList(string name)
        {

            return SelectInternal(name,null,null);
        }


        public static IHtmlString DropDownList(string name, IEnumerable<SelectListItem> selectList)
        {
            return SelectInternal(name, selectList, null);
        }

        public static IHtmlString DropDownList(string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return SelectInternal(name, selectList, AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static IHtmlString DropDownList(string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(name, selectList, htmlAttributes);
        }

        private static IHtmlString SelectInternal(string name, IEnumerable<SelectListItem> selectList,IDictionary<string, object> htmlAttributes)
        {
            var tag = new SelectTag(x=>SetResult(x,selectList)).Id(name).Attr("name", name);

            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes)
                {
                    tag.Attr(item.Key, item.Value);
                }
            }
            return new NonEncodedHtmlString(tag.ToString());
        }

        private static object SetResult(SelectTag x, IEnumerable<SelectListItem> selectList)
        {
            foreach (var item in selectList)
            {
                if (item.Selected)
                {
                    x.TopOption(item.Text, item.Value);
                }
                else
                {
                    x.Option(item.Text, item.Value);
                }
            }
            return x;
        }


        public static RouteValueDictionary AnonymousObjectToHtmlAttributes(object htmlAttributes)
        {
            RouteValueDictionary result = new RouteValueDictionary();

            if (htmlAttributes != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(htmlAttributes))
                {
                    result.Add(property.Name.Replace('_', '-'), property.GetValue(htmlAttributes));
                }
            }

            return result;
        }
    }
}
