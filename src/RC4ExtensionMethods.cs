using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace RC4.NET
{
    public static class RC4ExtensionMethods
    {
        public static String RC4ize(this String s, byte[] key, byte[] iv, Encoding enc)
        {
            byte[] sBuf = enc.GetBytes(s);

            MemoryStream ms = new MemoryStream();
            ms.Write(sBuf, 0, sBuf.Length);
            ms.Position = 0;

            CryptoStream cs = new CryptoStream(ms,
                new RC4().CreateEncryptor(key, iv),
                CryptoStreamMode.Read);

            byte[] rBuf = new byte[sBuf.Length];
            cs.Read(rBuf, 0, sBuf.Length);

            return enc.GetString(rBuf);
        }
    }
}
