using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Test
{
    public class LegacyPasswordHelper
    {
        public static string EncodePassword(string pass, string salt)
        {
            string result = null;
            if (!string.IsNullOrEmpty(pass) && !string.IsNullOrEmpty(salt))
            {
                byte[] bytes = Encoding.Unicode.GetBytes(pass);
                byte[] src = Convert.FromBase64String(salt);
                byte[] dst = new byte[src.Length + bytes.Length];
                Buffer.BlockCopy(src, 0, dst, 0, src.Length);
                Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
                var algorithm = HashAlgorithm.Create("SHA1");
                result = Convert.ToBase64String(algorithm.ComputeHash(dst));
            }
            return result;
        }

        public static string GenerateSalt()
        {
            byte[] data = new byte[0x10];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            return Convert.ToBase64String(data);
        }
    }
}
