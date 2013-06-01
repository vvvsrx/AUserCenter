using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Util
{
    public static class RequestResultUtil
    {
        public static Guid[] GetIdsByGuid(dynamic requestIds)
        {
            if (requestIds != null)
            {
                if (!String.IsNullOrEmpty(requestIds.ToString()))
                {
                    string[] strongids = requestIds.ToString().Split(',');
                    Guid[] ids = new Guid[strongids.Length];
                    for (int i = 0; i < strongids.Length; i++)
                    {
                        ids[i] = new Guid(strongids[i]);
                    }
                    return ids;
                }
            }
            return null;
        }
    }
}
