using System;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace RC4.NET
{
	/// <summary>
	/// RC4 algorithm cryptography
	/// </summary>
    public class RC4 : SymmetricAlgorithm
	{
        public override ICryptoTransform CreateEncryptor()
        {
            throw new InvalidOperationException("You must give the cryptor an initialization vector and a key!");
        }

        public override ICryptoTransform CreateDecryptor()
        {
            throw new InvalidOperationException("You must give the cryptor an initialization vector and a key!");
        }

        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            return new RC4Transform(rgbKey, rgbIV);
        }

        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            return new RC4Transform(rgbKey, rgbIV);
        }

        public override void GenerateIV()
        {
            throw new InvalidOperationException("You must give the cryptor an initialization vector!");
        }

        public override void GenerateKey()
        {
            throw new InvalidOperationException("You must give the cryptor a key!");
        }
    }
}
