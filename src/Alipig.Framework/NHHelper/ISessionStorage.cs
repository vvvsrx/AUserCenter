using NHibernate;

namespace Alipig.Framework.NHHelper
{
    public interface ISessionStorage
    {
        ISession Get();
        void Set(ISession value);
    }
}
