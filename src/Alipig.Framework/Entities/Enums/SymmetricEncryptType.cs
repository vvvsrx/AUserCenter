using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Entities
{
    public enum SymmetricEncryptType : byte
    {
        DES = 0,
        RC2 = 1,
        Rijndael = 2,
        TripleDES = 3
    }
}
