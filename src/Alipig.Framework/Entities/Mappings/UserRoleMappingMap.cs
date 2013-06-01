using FluentNHibernate.Mapping;

namespace Alipig.Framework.Entities.Mappings
{
    public class UserRoleMappingMap : ClassMap<UserRoleMapping>
    {
        public UserRoleMappingMap()
        {
            Table("t_userrolemapping");
            Id(x => x.ID).Column("F_Id").GeneratedBy.GuidComb();
            Map(x => x.CreateTime).Column("F_CreateTime");

            Map(x => x.UserId).Column("F_UserId");
            Map(x => x.RoleId).Column("F_RoleId");

            References(x => x.RoleRow).Column("F_RoleId").Class(typeof(Role)).Not.Insert().Not.Update();
        }
    }
}
