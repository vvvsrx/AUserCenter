using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace Alipig.UserCenterMvc
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = x =>
            {
                return Response.AsRedirect("~/main");
            };

            Get["/main"] = x =>
            {
                return View["Home/main"];
            };

            Get["/charisma.js"] = x =>
            {
                return Response.AsJs("Script/charisma.js");
            };

            Get["/noty.util.js"] = x =>
            {
                return Response.AsJs("Script/noty.util.js");
            };

            Get["/xxx"] = x => xxx(this, x);
        }


        private dynamic xxx(HomeModule homeModule, dynamic x)
        {
            
            throw new NotImplementedException();
        }
    }
}