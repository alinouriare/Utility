using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhs.Bulletin.E_Utility
{
    public class HexadecimalEncoding
    {
        public static string ToHexString(string str)
        {
            var sb = new StringBuilder();

            var bytes = System.Text.Encoding.UTF8.GetBytes(str);
            foreach (var t in bytes)
                sb.Append(t.ToString("X2"));
            return sb.ToString();
        }
        public static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return System.Text.Encoding.UTF8.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
    }
}
