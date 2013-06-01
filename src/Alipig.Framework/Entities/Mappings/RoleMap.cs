using FluentNHibernate.Mapping;

namespace Alipig.Framework.Entities.Mappings
{
    public class RoleMap:ClassMap<Role>
    {
        public RoleMap()
        {
            Table("t_role");
            Id(x => x.ID).Column("F_Id").GeneratedBy.GuidComb();
            Map(x => x.CreateTime).Column("F_CreateTime");
            Map(x => x.UpdateTime).Column("F_UpdateTime");
            Map(x => x.IsDel).Column("F_IsDel");
            Map(x => x.AutoId).Column("F_AutoId");
            Version(x => x.Version);

            Map(x => x.SiteId).Column("F_SiteId");
            Map(x => x.Name).Column("F_Name").Length(200);
            Map(x => x.Description).Column("F_Description").Length(1000);
            Map(x => x.Order).Column("F_Order");
            Map(x => x.PermissionJsonData).Column("F_PermissionJsonData");
        }
    }
}
