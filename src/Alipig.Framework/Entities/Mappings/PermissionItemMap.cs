using FluentNHibernate.Mapping;

namespace Alipig.Framework.Entities.Mappings
{
    public class PermissionItemMap : ClassMap<PermissionItem>
    {
        public PermissionItemMap()
        {
            Table("t_permissionitem");
            Id(x => x.ID).Column("F_Id").GeneratedBy.GuidComb();
            Map(x => x.CreateTime).Column("F_CreateTime");
            Map(x => x.UpdateTime).Column("F_UpdateTime");
            Map(x => x.IsDel).Column("F_IsDel");
            Map(x => x.AutoId).Column("F_AutoId");
            Version(x => x.Version);
            Map(x => x.PermissionGroupId).Column("F_PermissionGroupId");
            Map(x => x.Code).Column("F_Code").Length(50);
            Map(x => x.DisplayName).Column("F_DisplayName").Length(100);
            Map(x => x.DisplayStyle).Column("F_DisplayStyle").CustomType<PermissionItemDisplayStyle>();
            Map(x => x.JsonDataUrl).Column("F_JsonDataUrl").Length(320);
            Map(x => x.JsonDataConst).Column("F_JsonDataConst").Length(1000);
            Map(x => x.Order).Column("F_Order");


            References(x => x.PermissionGroupRow).Column("F_PermissionGroupId").Class(typeof(PermissionGroup)).Not.Insert().Not.Update();
        }
    }
}
