using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Framework.Common
{
    public sealed class EncryptManager
    {
        // Fields
        private static readonly string IVStringForInviteFriend = "rdiLyAQj7OM=";
        private static readonly string IVStringForPassword = "7RCcUDNagvI=";
        private static readonly string KeyStringForInviteFriend = "NhqPjL8Mf3g=";
        private static readonly string KeyStringForPassword = "2UkoifGvu4T1XT1BRFMiuhOYTBZPEKE9";

        // Methods
        public static string Decrypt(SymmetricEncryptType encryptType, string encryptedString, string ivString, string keyString)
        {
            SymmetricEncrypt encrypt = new SymmetricEncrypt(encryptType)
            {
                IVString = ivString,
                KeyString = keyString
            };
            return encrypt.Decrypt(encryptedString);
        }

        public static string DecryptTokenForInviteFriend(string encryptedString)
        {
            string str = string.Empty;
            try
            {
                str = Decrypt(SymmetricEncryptType.DES, encryptedString, IVStringForInviteFriend, KeyStringForInviteFriend);
            }
            catch
            {
            }
            return str;
        }

        public static string DecryptTokenForPassword(string encryptedString)
        {
            return Decrypt(SymmetricEncryptType.TripleDES, encryptedString, IVStringForPassword, KeyStringForPassword);
        }

        public static string DecryptTokenForVerifyCode(string encryptedString)
        {
            string str = string.Empty;
            try
            {
                str = Decrypt(SymmetricEncryptType.DES, encryptedString, IVStringForInviteFriend, KeyStringForInviteFriend);
            }
            catch
            {
            }
            return str;
        }

        private static string Encrypt(SymmetricEncryptType encryptType, string originalString, string ivString, string keyString)
        {
            SymmetricEncrypt encrypt = new SymmetricEncrypt(encryptType)
            {
                IVString = ivString,
                KeyString = keyString
            };
            return encrypt.Encrypt(originalString);
        }

        public static string EncryptTokenForInviteFriend(string originalString)
        {
            string str = string.Empty;
            try
            {
                str = Encrypt(SymmetricEncryptType.DES, originalString, IVStringForInviteFriend, KeyStringForInviteFriend);
            }
            catch
            {
            }
            return str;
        }

        public static string EncryptTokenForPassword(string originalString)
        {
            return Encrypt(SymmetricEncryptType.TripleDES, originalString, IVStringForPassword, KeyStringForPassword);
        }

        public static string EncryptTokenForVerifyCode(string originalString)
        {
            string str = string.Empty;
            try
            {
                str = Encrypt(SymmetricEncryptType.DES, originalString, IVStringForInviteFriend, KeyStringForInviteFriend);
            }
            catch
            {
            }
            return str;
        }

        public static string MD5(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str2 = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str2 = str2 + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str2;
        }

        public static string MD5_16(string str)
        {
            return MD5(str).Substring(8, 0x10);
        }
    }
}
