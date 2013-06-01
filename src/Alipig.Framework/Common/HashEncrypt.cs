using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Alipig.Framework.Common
{
    public class HashEncrypt
    {
        // Fields
        private bool mboolUseSalt;
        private HashEncryptType mbytHashType;
        private HashAlgorithm mhash;
        private short msrtSaltLength;
        private string mstrHashString;
        private string mstrOriginalString;
        private string mstrSaltValue;

        // Methods
        public HashEncrypt()
        {
            this.mstrSaltValue = string.Empty;
            this.msrtSaltLength = 8;
            this.mbytHashType = HashEncryptType.MD5;
        }

        public HashEncrypt(HashEncryptType HashType)
        {
            this.mstrSaltValue = string.Empty;
            this.msrtSaltLength = 8;
            this.mbytHashType = HashType;
        }

        public HashEncrypt(HashEncryptType HashType, string OriginalString)
        {
            this.mstrSaltValue = string.Empty;
            this.msrtSaltLength = 8;
            this.mbytHashType = HashType;
            this.mstrOriginalString = OriginalString;
        }

        public HashEncrypt(HashEncryptType HashType, string OriginalString, bool UseSalt, string SaltValue)
        {
            this.mstrSaltValue = string.Empty;
            this.msrtSaltLength = 8;
            this.mbytHashType = HashType;
            this.mstrOriginalString = OriginalString;
            this.mboolUseSalt = UseSalt;
            this.mstrSaltValue = SaltValue;
        }

        public string CreateHash()
        {
            this.SetEncryptor();
            if (this.mboolUseSalt && (this.mstrSaltValue.Length == 0))
            {
                this.mstrSaltValue = this.CreateSalt();
            }
            byte[] bytes = Encoding.UTF8.GetBytes(this.mstrSaltValue + this.mstrOriginalString);
            return Convert.ToBase64String(this.mhash.ComputeHash(bytes));
        }

        public string CreateHash(string OriginalString)
        {
            this.mstrOriginalString = OriginalString;
            return this.CreateHash();
        }

        public string CreateHash(string OriginalString, HashEncryptType HashType)
        {
            this.mstrOriginalString = OriginalString;
            this.mbytHashType = HashType;
            return this.CreateHash();
        }

        public string CreateHash(string OriginalString, bool UseSalt)
        {
            this.mstrOriginalString = OriginalString;
            this.mboolUseSalt = UseSalt;
            return this.CreateHash();
        }

        public string CreateHash(string OriginalString, string SaltValue)
        {
            this.mstrOriginalString = OriginalString;
            this.mstrSaltValue = SaltValue;
            return this.CreateHash();
        }

        public string CreateHash(string OriginalString, HashEncryptType HashType, string SaltValue)
        {
            this.mstrOriginalString = OriginalString;
            this.mbytHashType = HashType;
            this.mstrSaltValue = SaltValue;
            return this.CreateHash();
        }

        public string CreateSalt()
        {
            byte[] data = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }

        public void Reset()
        {
            this.mstrSaltValue = string.Empty;
            this.mstrOriginalString = string.Empty;
            this.mstrHashString = string.Empty;
            this.mboolUseSalt = false;
            this.mbytHashType = HashEncryptType.MD5;
            this.mhash = null;
        }

        private void SetEncryptor()
        {
            switch (this.mbytHashType)
            {
                case HashEncryptType.MD5:
                    this.mhash = new MD5CryptoServiceProvider();
                    break;

                case HashEncryptType.SHA1:
                    this.mhash = new SHA1CryptoServiceProvider();
                    break;

                case HashEncryptType.SHA256:
                    this.mhash = new SHA256Managed();
                    break;

                case HashEncryptType.SHA384:
                    this.mhash = new SHA384Managed();
                    break;

                case HashEncryptType.SHA512:
                    this.mhash = new SHA512Managed();
                    break;
            }
        }

        // Properties
        public string HashString
        {
            get
            {
                return this.mstrHashString;
            }
            set
            {
                this.mstrHashString = value;
            }
        }

        public HashEncryptType HashType
        {
            get
            {
                return this.mbytHashType;
            }
            set
            {
                if (this.mbytHashType != value)
                {
                    this.mbytHashType = value;
                    this.mstrOriginalString = string.Empty;
                    this.mstrHashString = string.Empty;
                    this.SetEncryptor();
                }
            }
        }

        public string OriginalString
        {
            get
            {
                return this.mstrOriginalString;
            }
            set
            {
                this.mstrOriginalString = value;
            }
        }

        public short SaltLength
        {
            get
            {
                return this.msrtSaltLength;
            }
            set
            {
                this.msrtSaltLength = value;
            }
        }

        public string SaltValue
        {
            get
            {
                return this.mstrSaltValue;
            }
            set
            {
                this.mstrSaltValue = value;
            }
        }

        public bool UseSalt
        {
            get
            {
                return this.mboolUseSalt;
            }
            set
            {
                this.mboolUseSalt = value;
            }
        }

        // Nested Types
        public enum HashEncryptType : byte
        {
            MD5 = 0,
            SHA1 = 1,
            SHA256 = 2,
            SHA384 = 3,
            SHA512 = 4
        }
    }
}
