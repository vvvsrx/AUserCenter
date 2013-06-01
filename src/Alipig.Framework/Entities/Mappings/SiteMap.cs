using FluentNHibernate.Mapping;

namespace Alipig.Framework.Entities.Mappings
{
    public class SiteMap : ClassMap<Site>
    {
        public SiteMap()
        {
            Table("t_site");
            Id(x => x.ID).Column("F_Id").GeneratedBy.GuidComb();
            Map(x => x.CreateTime).Column("F_CreateTime");
            Map(x => x.UpdateTime).Column("F_UpdateTime");
            Map(x => x.IsDel).Column("F_IsDel");
            Map(x => x.AutoId).Column("F_AutoId");
            Version(x => x.Version);

            Map(x => x.SiteName).Column("F_SiteName").Length(100);
            Map(x => x.Description).Column("F_Description").Length(500);
            Map(x => x.Order).Column("F_Order");
            Map(x => x.DomainList).Column("F_DomainList").Length(4000);
        }
    }
}
