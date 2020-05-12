using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhs.Bulletin.E_Utility.EncryptDecryptUtility.Models
{
    public class TokenHelper
    {
        #region Variables
        public bool IsEncryptDataActive { get { return _isEncryptDataActive; } }
        public string PublicKey { get; set; }

        /// <summary>
        /// must be atleast 8 characters
        /// </summary>
        public string PrivateKey
        {
            get
            {
                return _privateKey;
            }
            set
            {
                if (value.Length < 8)
                    return;
                _privateKey = value;
            }
        }
        public DateTime ExpDate { get { return _expireData; } }
        public Exception ExceptionData { get { return _exceptionData; } }

        private Exception _exceptionData = null;
        private string _privateKey;
        private DateTime _expireData = DateTime.Now.AddHours(2);

        private static Dictionary<string, string> _replaceItem = new Dictionary<string, string>()
        {
            {"#","[|23]" },
            {"?","[|3f]" },
            {"&","[|26]" },
            {"/","[|2f]" },
            {@"\","[|5c]" },
            {"+","[|2b]" },
        };
        private char[] _upperAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private bool _isEncryptDataActive = true;
        #endregion

        #region Constructors

        public TokenHelper(bool isEncryptDataActive = true)
        {
            _isEncryptDataActive = isEncryptDataActive;
            try
            {
                if (string.IsNullOrEmpty(PublicKey) &&
                    string.IsNullOrWhiteSpace(PublicKey) &&
                    string.IsNullOrEmpty(PrivateKey) &&
                    string.IsNullOrWhiteSpace(PrivateKey))
                {
                    string guidTemp = Guid.NewGuid().ToString();
                    guidTemp = guidTemp.Replace("-", "");
                    PublicKey = guidTemp.Substring(0, guidTemp.Length / 2);
                    PrivateKey = guidTemp.Substring(guidTemp.Length / 2, guidTemp.Length / 2);
                    //PublicKey = guidTemp.Substring(0, 8);
                    //PrivateKey = guidTemp.Substring(guidTemp.Length / 2, 8);
                    _expireData = DateTime.Now.AddHours(2);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public TokenHelper(string publicKey, string privateKey, DateTime? expireData = null,bool isEncryptDataActive = true)
        {
            _isEncryptDataActive = isEncryptDataActive;
            try
            {
                if (!(string.IsNullOrEmpty(publicKey) &&
                    string.IsNullOrWhiteSpace(publicKey) &&
                    string.IsNullOrEmpty(privateKey) &&
                    string.IsNullOrWhiteSpace(privateKey)))
                {
                    PublicKey = publicKey;
                    PrivateKey = privateKey;
                    _expireData = expireData ?? DateTime.Now.AddHours(2);
                }
                else
                {
                    GenerateNewKeys();
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clearTex"></param>
        /// <returns></returns>
        public string Encrypt(int number)
        {
            return Encrypt(number.ToString());
        }
        public string Encrypt(Guid guid)
        {
            return Encrypt(guid.ToString());
        }
        public string Encrypt(string clearTex)
        {
            try
            {
                if (clearTex == null) clearTex = "";


                if (!IsEncryptDataActive)
                    return clearTex;

                _exceptionData = null;
                var result = EncryptDecryptUtilities.Encrypt3(clearTex, PublicKey, PrivateKey, out _exceptionData);

                foreach (var item in _replaceItem)
                    result = result.Replace(item.Key, item.Value);



                return MakeEncryptToLower(result);
            }
            catch (Exception ex)
            {
                _exceptionData = ex;
                return "NULL";
            }

        }
        public string Decrypt(string cipherText)
        {

            try
            {
                if (cipherText == null) cipherText = "";

                if (!IsEncryptDataActive)
                    return cipherText;

                cipherText = MakeDecryptToUpper(cipherText);
                foreach (var item in _replaceItem)
                    cipherText = cipherText.Replace(item.Value, item.Key);


                _exceptionData = null;
                var result = EncryptDecryptUtilities.Decrypt3(cipherText, PublicKey, PrivateKey, out _exceptionData);

                return result;
            }
            catch (Exception ex)
            {
                _exceptionData = ex;
                return "NULL";
            }
        }

        #endregion

        #region Helper Methods

        private void GenerateNewKeys()
        {
            string guidTemp = Guid.NewGuid().ToString().Replace("-", "");
            PublicKey = guidTemp.Substring(0, guidTemp.Length / 2);
            PrivateKey = guidTemp.Substring(guidTemp.Length / 2, guidTemp.Length / 2);
            _expireData = DateTime.Now.AddHours(2);
        }

        /// <summary>
        /// تمام رشته را به حروف کوچک کد می کند
        /// </summary>
        /// <param name="encryptText"></param>
        /// <returns></returns>
        private string MakeEncryptToLower(string encryptText)
        {
            var result = encryptText;

            foreach (var charData in _upperAlpha)
            {
                var toUpperChar = (new string(charData, 1)).ToUpper();
                var toLowerChar = "|" + (new string(charData, 1).ToLower());
                result = result.Replace(toUpperChar, toLowerChar);
            }

            return result;
        }

        /// <summary>
        /// حروف بزرگ که به صورت کوچک کد شده است به شکل قبلی باز می گرداند
        /// </summary>
        /// <param name="encryptText"></param>
        /// <returns></returns>
        private string MakeDecryptToUpper(string encryptText)
        {
            var result = encryptText;

            foreach (var charData in _upperAlpha)
            {
                var toUpperChar = (new string(charData, 1)).ToUpper();
                var toLowerChar = "|" + (new string(charData, 1).ToLower());
                result = result.Replace(toLowerChar, toUpperChar);
            }

            return result;
        }
        #endregion

    }
}
