using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Entities
{
    public enum UserPasswordFormats
    {
        Clear = 0,
        HashedFromMembership = 1,
        EncryptedFromMembership = 2,
        EncryptedFromSpaceBuilder = 3,
        MD5 = 6
    }
}
