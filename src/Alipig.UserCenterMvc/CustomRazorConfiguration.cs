using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ViewEngines.Razor;

namespace Alipig.UserCenterMvc
{
    public class CustomRazorConfiguration : IRazorConfiguration
    {
        public bool AutoIncludeModelNamespace
        {
            get
            {
                return true;
            }
        }

        public IEnumerable<string> GetAssemblyNames()
        {
            return new[]
                {
                    "Alipig.Framework",
                };
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            return new[]
                {
                    "Alipig.Framework",
                };
        }
    }
}