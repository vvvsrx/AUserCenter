using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Alipig.Framework.Common;
using Alipig.Framework.Entities;

namespace Alipig.Framework.Util
{
    public static class PasswordUtil
    {
        public static bool CheckPassword(string password, string storedPassword, UserPasswordFormats passwordFormat, string passwordSalt)
        {
            string str = EncodePassword(password, passwordFormat, passwordSalt);
            return ((str != null) && str.Equals(storedPassword, StringComparison.CurrentCultureIgnoreCase));
        }

        public static string EncodePassword(string password, UserPasswordFormats passwordFormat, string passwordSalt)
        {
            if (passwordFormat == UserPasswordFormats.Clear)
            {
                return password;
            }
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] src = Convert.FromBase64String(passwordSalt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            switch (passwordFormat)
            {
                case UserPasswordFormats.HashedFromMembership:
                    return Convert.ToBase64String(HashAlgorithm.Create(Membership.HashAlgorithmType).ComputeHash(dst));
                case UserPasswordFormats.EncryptedFromMembership:
                    MembershipProvider provider = Membership.Provider;
                    MethodInfo method = typeof(MembershipProvider).GetMethod("EncodePassword", BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (method == null)
                    {
                        return null;
                    }
                    return (method.Invoke(provider, new object[] { password, MembershipPasswordFormat.Encrypted, passwordSalt }) as string);
                case UserPasswordFormats.EncryptedFromSpaceBuilder:
                    return EncryptManager.EncryptTokenForPassword(password);
                case UserPasswordFormats.MD5:
                    return EncryptManager.MD5(password);
            }
            return null;
        }

        public static string GenerateSalt()
        {
            byte[] data = new byte[0x18];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }


    }
}
