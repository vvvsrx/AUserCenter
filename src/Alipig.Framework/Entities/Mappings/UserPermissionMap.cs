using FluentNHibernate.Mapping;

namespace Alipig.Framework.Entities.Mappings
{
    public class UserPermissionMap: ClassMap<UserPermission>
    {
        public UserPermissionMap()
        {
            Table("T_UserPermission");
            Id(x => x.ID).Column("F_Id").GeneratedBy.GuidComb();
            Map(x => x.CreateTime).Column("F_CreateTime");
            Map(x => x.UpdateTime).Column("F_UpdateTime");
            Map(x => x.IsDel).Column("F_IsDel");
            Map(x => x.AutoId).Column("F_AutoId");
            Version(x => x.Version);

            Map(x => x.UserId).Column("F_UserId");
            Map(x => x.SiteId).Column("F_SiteId");
            Map(x => x.PermissionJsonData).Column("F_PermissionJsonData");
        }
    }
}
