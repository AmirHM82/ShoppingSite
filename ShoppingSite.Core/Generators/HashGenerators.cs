using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ShoppingSite.Core.Generators
{
    public static class HashGenerators
    {
        public static string MD5Encoding(this string Password)
        {
            Byte[] mainByte;
            Byte[] encodeByte;

            mainByte = ASCIIEncoding.Default.GetBytes(Password);
            encodeByte = MD5.Create().ComputeHash(mainByte);
            return BitConverter.ToString(encodeByte);
        }
    }
}
