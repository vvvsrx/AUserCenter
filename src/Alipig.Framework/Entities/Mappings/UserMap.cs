using FluentNHibernate.Mapping;

namespace Alipig.Framework.Entities.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("t_user");
            Id(x => x.ID).Column("F_Id").GeneratedBy.GuidComb();
            Map(x => x.CreateTime).Column("F_CreateTime");
            Map(x => x.UpdateTime).Column("F_UpdateTime");
            Map(x => x.IsDel).Column("F_IsDel");
            Map(x => x.AutoId).Column("F_AutoId");
            Version(x => x.Version);

            Map(x => x.UserName).Column("F_UserName").Length(128);
            Map(x => x.Password).Column("F_Password").Length(128);
            Map(x => x.PasswordSalt).Column("F_PasswordSalt").Length(128);
            Map(x => x.PasswordFormat).Column("F_PasswordFormat").CustomType<UserPasswordFormats>();
            Map(x => x.PrivateEmail).Column("F_PrivateEmail").Length(128);
            Map(x => x.MembershipID).Column("F_MembershipID");
            Map(x => x.Nickname).Column("F_Nickname").Length(128);
            Map(x => x.IPCreated).Column("F_IPCreated").Length(64);
            Map(x => x.IPLastActivity).Column("F_IPLastActivity").Length(64);
            Map(x => x.Status).Column("F_Status").CustomType<UserAccountStatuses>();
            Map(x => x.Gender).Column("F_Gender").CustomType<UserGender>();
        }
    }
}
