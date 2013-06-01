using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Framework.Common
{
    public class SymmetricEncrypt
    {
        // Fields
        private SymmetricEncryptType mbytEncryptionType;
        private SymmetricAlgorithm mCSP;
        private string mstrEncryptedString;
        private string mstrOriginalString;

        // Methods
        public SymmetricEncrypt()
        {
            this.mbytEncryptionType = SymmetricEncryptType.DES;
            this.SetEncryptor();
        }

        public SymmetricEncrypt(SymmetricEncryptType encryptionType)
        {
            this.mbytEncryptionType = encryptionType;
            this.SetEncryptor();
        }

        public SymmetricEncrypt(SymmetricEncryptType encryptionType, string originalString)
        {
            this.mbytEncryptionType = encryptionType;
            this.mstrOriginalString = originalString;
            this.SetEncryptor();
        }

        public string Decrypt()
        {
            ICryptoTransform transform = this.mCSP.CreateDecryptor(this.mCSP.Key, this.mCSP.IV);
            byte[] buffer = Convert.FromBase64String(this.mstrEncryptedString);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            stream2.Close();
            this.mstrOriginalString = Encoding.Unicode.GetString(stream.ToArray());
            return this.mstrOriginalString;
        }

        public string Decrypt(string encryptedString)
        {
            this.mstrEncryptedString = encryptedString;
            return this.Decrypt();
        }

        public string Decrypt(string encryptedString, SymmetricEncryptType encryptionType)
        {
            this.mstrEncryptedString = encryptedString;
            this.mbytEncryptionType = encryptionType;
            return this.Decrypt();
        }

        public string Encrypt()
        {
            ICryptoTransform transform = this.mCSP.CreateEncryptor(this.mCSP.Key, this.mCSP.IV);
            byte[] bytes = Encoding.Unicode.GetBytes(this.mstrOriginalString);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            stream2.Close();
            this.mstrEncryptedString = Convert.ToBase64String(stream.ToArray());
            return this.mstrEncryptedString;
        }

        public string Encrypt(string originalString)
        {
            this.mstrOriginalString = originalString;
            return this.Encrypt();
        }

        public string Encrypt(string originalString, SymmetricEncryptType encryptionType)
        {
            this.mstrOriginalString = originalString;
            this.mbytEncryptionType = encryptionType;
            return this.Encrypt();
        }

        public string GenerateIV()
        {
            this.mCSP.GenerateIV();
            return Convert.ToBase64String(this.mCSP.IV);
        }

        public string GenerateKey()
        {
            this.mCSP.GenerateKey();
            return Convert.ToBase64String(this.mCSP.Key);
        }

        private void SetEncryptor()
        {
            switch (this.mbytEncryptionType)
            {
                case SymmetricEncryptType.DES:
                    this.mCSP = new DESCryptoServiceProvider();
                    break;

                case SymmetricEncryptType.RC2:
                    this.mCSP = new RC2CryptoServiceProvider();
                    break;

                case SymmetricEncryptType.Rijndael:
                    this.mCSP = new RijndaelManaged();
                    break;

                case SymmetricEncryptType.TripleDES:
                    this.mCSP = new TripleDESCryptoServiceProvider();
                    break;
            }
            this.mCSP.GenerateKey();
            this.mCSP.GenerateIV();
        }

        // Properties
        public SymmetricAlgorithm CryptoProvider
        {
            get
            {
                return this.mCSP;
            }
            set
            {
                this.mCSP = value;
            }
        }

        public string EncryptedString
        {
            get
            {
                return this.mstrEncryptedString;
            }
            set
            {
                this.mstrEncryptedString = value;
            }
        }

        public SymmetricEncryptType EncryptionType
        {
            get
            {
                return this.mbytEncryptionType;
            }
            set
            {
                if (this.mbytEncryptionType != value)
                {
                    this.mbytEncryptionType = value;
                    this.mstrOriginalString = string.Empty;
                    this.mstrEncryptedString = string.Empty;
                    this.SetEncryptor();
                }
            }
        }

        public byte[] IV
        {
            get
            {
                return this.mCSP.IV;
            }
            set
            {
                this.mCSP.IV = value;
            }
        }

        public string IVString
        {
            get
            {
                return Convert.ToBase64String(this.mCSP.IV);
            }
            set
            {
                this.mCSP.IV = Convert.FromBase64String(value);
            }
        }

        public byte[] key
        {
            get
            {
                return this.mCSP.Key;
            }
            set
            {
                this.mCSP.Key = value;
            }
        }

        public string KeyString
        {
            get
            {
                return Convert.ToBase64String(this.mCSP.Key);
            }
            set
            {
                this.mCSP.Key = Convert.FromBase64String(value);
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
    }
}
