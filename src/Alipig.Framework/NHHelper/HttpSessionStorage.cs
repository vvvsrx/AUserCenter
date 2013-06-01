using System;
using System.Web;
using NHibernate;
namespace Alipig.Framework.NHHelper
{
    public class HttpSessionStorage : ISessionStorage
    {
        #region ISessionStorage 成员

        public ISession Get()
        {
            //return (ISession)AppDomain.CurrentDomain.GetData("NhbSession");
            return (ISession)HttpContext.Current.Items["NhbSession"];
        }

        public void Set(ISession value)
        {
            if (value != null)
            {
                //AppDomain.CurrentDomain.SetData("NhbSession", value);
                HttpContext.Current.Items.Add("NhbSession", value);
            }
        }

        #endregion
    }
}
