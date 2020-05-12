using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.EncryptDecryptUtility.Models;

namespace Fhs.Bulletin.E_Utility.EncryptDecryptUtility
{
    public static class EncryptDecryptUtilities
    {
        #region Variables

        public static List<Exception> InnerExceptionList = new List<Exception>();

        #endregion

        #region Methods

        public static string CalculateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        public static byte[] EncryptPassword(string password)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes = encoding.GetBytes(password);

            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] cryptPassword = sha1.ComputeHash(hashBytes);

            return cryptPassword;
        }

        /// <summary>
        /// Encrypt clear text to cipher text
        /// </summary>
        /// <param name="clearText">clear text</param>
        /// <param name="encryptionKey1">key 1</param>
        /// <param name="encryptionKey2">8 characters key 2</param>
        /// <param name="exData">Exception Data</param>
        /// <returns></returns>
        public static string Encrypt(string clearText, string encryptionKey1, string encryptionKey2,
            out Exception exData)
        {
            exData = null;
            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (var encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey1, AsciiToHex(encryptionKey2));
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (
                            CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;
            }
            catch (Exception ex)
            {
                if (InnerExceptionList == null) InnerExceptionList = new List<Exception>();

                InnerExceptionList.Add(ex);
                exData = ex;
                return "NULL";
            }
        }

        public static string Encrypt2(string value, string password, string salt, out Exception exData)
        {
            exData = null;
            try
            {
                DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));
                SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
                byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
                byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);
                ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);
                using (MemoryStream buffer = new MemoryStream())
                {
                    using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                        {
                            writer.Write(value);
                        }
                    }
                    return Convert.ToBase64String(buffer.ToArray());
                }
            }
            catch (Exception ex)
            {
                exData = ex;
                return "NULL";
            }

        }

        public static string Encrypt3(string clearText, string encryptionKey1, string encryptionKey2,
            out Exception exData)
        {
            exData = null;
            try
            {
                var mainKey = encryptionKey1 + encryptionKey2;
                var blowFish = new Blowfish(mainKey);

                return blowFish.Encipher(clearText);
            }
            catch (Exception ex)
            {
                if (InnerExceptionList == null) InnerExceptionList = new List<Exception>();

                exData = ex;
                InnerExceptionList.Add(ex);
                return "NULL";
            }
        }

        public static string Encrypt4(string clearText, string encryptionKey1, string encryptionKey2,out Exception exData)
        {
            exData = null;
            try
            {
                string key = encryptionKey1;
                string encryptedText = "";

                MD5 md5 = new MD5CryptoServiceProvider();
                TripleDES des = new TripleDESCryptoServiceProvider();
                des.KeySize = 128;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;

                var hashByte = Encoding.UTF8.GetBytes(key);
                var md5ByteArray = md5.ComputeHash(hashByte);
                

                byte[] md5Bytes = md5ByteArray;
                byte[] ivBytes = new byte[des.BlockSize/8];
                des.Key = md5Bytes;
                des.IV = ivBytes;

                byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);

                ICryptoTransform ct = des.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptedText = Convert.ToBase64String(ms.ToArray());
                }
                return encryptedText;
            }
            catch (Exception ex)
            {
                exData = null;
                return "";
            }
        }

        public static string Encrypt5(string clearText, string encryptionKey1, string encryptionKey2, out Exception exData)
        {
            exData = null;
            try
            {
                string key = encryptionKey1;
                string encryptedText = "";

                MD5 md5 = new MD5CryptoServiceProvider();
                TripleDES des = new TripleDESCryptoServiceProvider();
                des.KeySize = 128;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;

                var hashByte = Encoding.UTF8.GetBytes(key);
                //var md5ByteArray = md5.ComputeHash(hashByte);


                //byte[] md5Bytes = md5ByteArray;
                byte[] ivBytes = new byte[des.BlockSize / 8];
                des.Key = hashByte;
                des.IV = ivBytes;

                byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);

                ICryptoTransform ct = des.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptedText = Convert.ToBase64String(ms.ToArray());
                }
                return encryptedText;
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }


        public static string Decrypt(string cipherText, string encryptionKey1, string encryptionKey2,
            out Exception exData)
        {
            exData = null;
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey1, AsciiToHex(encryptionKey2));
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (
                            CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;

            }
            catch (Exception ex)
            {
                if (InnerExceptionList == null) InnerExceptionList = new List<Exception>();

                InnerExceptionList.Add(ex);
                exData = ex;
                return "NULL";
            }
        }
        public static string Decrypt2(string text, string password, string salt, out Exception exData)
        {
            exData = null;
            try
            {
                DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));
                SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
                byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
                byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);
                ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);
                using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(text)))
                {
                    using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exData = ex;
                return "NULL";
            }

        }
        public static string Decrypt3(string cipherText, string encryptionKey1, string encryptionKey2,out Exception exData)
        {
            exData = null;
            try
            {
                var mainKey = encryptionKey1 + encryptionKey2;

                var blowFish = new Blowfish(mainKey);

                return blowFish.Decipher(cipherText);
            }
            catch (Exception ex)
            {
                if (InnerExceptionList == null) InnerExceptionList = new List<Exception>();

                InnerExceptionList.Add(ex);
                exData = ex;
                return "NULL";
            }
        }
        public static string Decrypt4(string encryptedString, string encryptionKey1, string encryptionKey2,out Exception exData)
        {
            exData = null;
            try
            {
                var plaintext = "";
                string key = encryptionKey1;

                MD5 md5 = new MD5CryptoServiceProvider();
                TripleDES des = new TripleDESCryptoServiceProvider();
                des.KeySize = 128;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;


                byte[] md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                byte[] ivBytes = new byte[des.BlockSize / 8];
                des.Key = md5Bytes;
                des.IV = ivBytes;
                
                byte[] clearBytes = Convert.FromBase64String(encryptedString);

                ICryptoTransform ct = des.CreateDecryptor();
                byte[] output = ct.TransformFinalBlock(clearBytes, 0, clearBytes.Length);

                return Encoding.UTF8.GetString(output);
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }
        public static string Decrypt5(string encryptedString, string encryptionKey1, string encryptionKey2, out Exception exData)
        {
            exData = null;
            try
            {
                var plaintext = "";
                string key = encryptionKey1;

                MD5 md5 = new MD5CryptoServiceProvider();
                TripleDES des = new TripleDESCryptoServiceProvider();
                des.KeySize = 128;
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;

                var keyBytes = Encoding.UTF8.GetBytes(key);
                //byte[] md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                byte[] ivBytes = new byte[des.BlockSize / 8];
                des.Key = keyBytes;
                des.IV = ivBytes;

                byte[] clearBytes = Convert.FromBase64String(encryptedString);

                ICryptoTransform ct = des.CreateDecryptor();
                byte[] output = ct.TransformFinalBlock(clearBytes, 0, clearBytes.Length);

                return Encoding.UTF8.GetString(output);
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }
        #endregion

        #region Helper Methods
        private static byte[] AsciiToHex(string asciiText)
        {
            try
            {
                char[] charValues = asciiText.ToCharArray();
                byte[] hexOutput = new byte[asciiText.Length];
                int index = 0;
                foreach (char _eachChar in charValues)
                {
                    int value = Convert.ToInt32(_eachChar);
                    hexOutput[index] = Convert.ToByte(String.Format("{0:X}", value));
                    index++;
                }
                return hexOutput;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static String PrintByteArray(byte[] hash)
        {
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }

        #endregion

    }
}
