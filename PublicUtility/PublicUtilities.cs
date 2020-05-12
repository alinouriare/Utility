using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.PublicUtility.Enums;
using System.Collections;

namespace Fhs.Bulletin.E_Utility.PublicUtility
{
    public static class PublicUtilities
    {
        #region Variables

        private static Random _randomNumber = new Random((int)DateTime.Now.Ticks);

        #endregion

        #region Methods

        public static String GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_randomNumber.Next(s.Length)]).ToArray());
        }

        public static string GenerateRandomCode2(int totalAlha, int totalNumber)
        {

            var result = "";
            var alphaIndex = 0;
            var indexLoop = 0;
            #region Generate Character

            while (alphaIndex < totalAlha)
            {
                var alphCode = _randomNumber.Next(65, 90);
                var alphChar = Convert.ToChar(alphCode).ToString().ToUpper();
                if (alphChar == "O" ||
                    alphChar == "I" ||
                    alphChar == "L" ||
                    alphChar == "Q") continue;

                result = result + alphChar;
                alphaIndex++;
            }

            #endregion

            #region Generate Generate Number

            indexLoop = 0;
            while (indexLoop < totalNumber)
            {
                var ch = _randomNumber.Next(0, 9);

                if (totalAlha != 0 &&
                    (ch == 0 || ch == 1 || ch == 7 || ch == 8)) continue;

                result = result + ch.ToString();
                _randomNumber.NextDouble();
                _randomNumber.Next(100, 1999);
                indexLoop++;
            }

            #endregion

            return result;

        }

        public static string RandomInteger(int size)
        {
            try
            {
                var seed = Guid.NewGuid().ToString("N").GetHashCode();
                var randomNumber = new Random(seed);
                const string chars = "0123456789";
                return new string(Enumerable.Repeat(chars, size)
                    .Select(s => s[randomNumber.Next(s.Length)]).ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static double? MakeRandomDouble(double rangeLength)
        {
            try
            {
                var seed = Guid.NewGuid().ToString("N").GetHashCode();
                var randomNumber = new Random(seed);
                return randomNumber.NextDouble() * rangeLength;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string RandomInteger2(int size)
        {
            try
            {
                var provider = new RNGCryptoServiceProvider();
                var byteArray = new byte[4];
                provider.GetBytes(byteArray);

                //convert 4 bytes to an integer
                var randomInteger = BitConverter.ToUInt32(byteArray, 0);

                return randomInteger.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static String ConvertArabicToPersian(string src)
        {
            var result = src;
            try
            {
                if (String.IsNullOrEmpty(src) ||
                    String.IsNullOrWhiteSpace(src))
                    return src;

                result = result.Trim();
                result = result.Replace("ك", "ک").Replace("ي", "ی");
                result = result.Replace("٠", "0").Replace("۰", "0")
                    .Replace("١", "1").Replace("۱", "1")
                    .Replace("٢", "2").Replace("۲", "2")
                    .Replace("٣", "3").Replace("۳", "3")
                    .Replace("٤", "4").Replace("۴", "4")
                    .Replace("٥", "5").Replace("۵", "5")
                    .Replace("٦", "6").Replace("۶", "6")
                    .Replace("٧", "7").Replace("۷", "7")
                    .Replace("٨", "8").Replace("۸", "8")
                    .Replace("٩", "9").Replace("۹", "9");

                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public static int ConvertStringToInt(string strNumber, int defaultValue)
        {
            try
            {
                var number = 0;
                if (String.IsNullOrWhiteSpace(strNumber) || String.IsNullOrEmpty(strNumber) ||
                   !int.TryParse(strNumber, out number))
                    return defaultValue;

                return number;
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }

        public static String ConvertDateTimeToJalali(DateTime date)
        {
            try
            {
                var pCalendar = new PersianCalendar();

                var year = pCalendar.GetYear(date);
                var month = pCalendar.GetMonth(date);
                var day = pCalendar.GetDayOfMonth(date);


                return String.Format("{0}/{1}/{2}", year, month.ToString("00"), day.ToString("00"));

            }
            catch (Exception e)
            {
                return date.ToString();
            }
        }
        public static string ConvertDateTimeToJalali02(DateTime dateTime)
        {
            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();

                return string.Format("{0}/{1}/{2}",
                    persianCalendar.GetYear(dateTime),
                    persianCalendar.GetMonth(dateTime).ToString("00"),
                    persianCalendar.GetDayOfMonth(dateTime).ToString("00"),
                    dateTime.ToShortTimeString().Replace("AM", "صبح").Replace("PM", "عصر")
                );
            }
            catch (Exception ex)
            {
                return "نا مشخص";
            }
        }
        public static string ConvertDateTimeToJalali03(DateTime dateTime)
        {
            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();

                return string.Format("{0}  {1}  {2} " + " ساعت " + "{3}",
                    persianCalendar.GetDayOfMonth(dateTime),
                    GetMonthNameByMonthNumber(persianCalendar.GetMonth(dateTime)),
                    persianCalendar.GetYear(dateTime),
                    dateTime.ToShortTimeString().Replace("AM", "صبح").Replace("PM", "عصر")
                );
            }
            catch (Exception ex)
            {
                return "نا مشخص";
            }
        }
        public static DateTime? ConvertJalaliDateToDateTime(string dateTimeStr)
        {
            try
            {
                if (dateTimeStr != null &&
                    !dateTimeStr.Contains("/") &&
                    !dateTimeStr.Contains("\\") &&
                    dateTimeStr.Length == 8)
                    return ConvertJalaliDate8CharToDateTime(dateTimeStr);

                var persianCalander = new PersianCalendar();
                var array = dateTimeStr.Split(new string[] { "/", "\\" }, StringSplitOptions.None);

                if (array.Length < 3) return null;

                var year = persianCalander.GetYear(DateTime.Now);
                var month = persianCalander.GetMonth(DateTime.Now);
                var day = persianCalander.GetDayOfMonth(DateTime.Now);
                if (!int.TryParse(array[0], out year) || !int.TryParse(array[1], out month) ||
                    !int.TryParse(array[2], out day))
                    return null;

                var persianCalendar = new PersianCalendar();

                return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static DateTime? ConvertJalaliDate8CharToDateTime(string dateTimeStr)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(dateTimeStr) ||
                    String.IsNullOrEmpty(dateTimeStr) ||
                    dateTimeStr.Length != 8) return null;

                var persianCalander = new PersianCalendar();
                var yearStr = dateTimeStr.Substring(0, 4);
                var monthStr = dateTimeStr.Substring(4, 2);
                var dayStr = dateTimeStr.Substring(6, 2);



                var year = persianCalander.GetYear(DateTime.Now);
                var month = persianCalander.GetMonth(DateTime.Now);
                var day = persianCalander.GetDayOfMonth(DateTime.Now);
                if (!int.TryParse(yearStr, out year) ||
                    !int.TryParse(monthStr, out month) ||
                    !int.TryParse(dayStr, out day))
                    return null;

                var persianCalendar = new PersianCalendar();

                return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static DateTime? ConvertJalaliDate8CharToDateTime2(string dateTimeStr)
        {
            try
            {

                //if (dateTimeStr.StartsWith("0") &&)
                //{
                //    dateTimeStr = dateTimeStr.Remove(0, 1);
                //    //dateTimeStr = dateTimeStr.Insert(3, "0");
                //}
                if (dateTimeStr.Contains(" "))
                {
                   
                    dateTimeStr= dateTimeStr.Remove(8, 2);
             
                    dateTimeStr = 0 + dateTimeStr;
                    dateTimeStr= dateTimeStr.Insert(3,"0");
                }
                if (String.IsNullOrWhiteSpace(dateTimeStr) ||
                    String.IsNullOrEmpty(dateTimeStr) ||
                    dateTimeStr.Length !=10) return null;
                if (dateTimeStr.Contains("00"))
                {
                    dateTimeStr = dateTimeStr.Remove(4, 1);
                    dateTimeStr = 0 + dateTimeStr;
                }
                var persianCalander = new PersianCalendar();
                var yearStr = dateTimeStr.Substring(6, 4);
                var monthStr = dateTimeStr.Substring(3, 2);
                var dayStr = dateTimeStr.Substring(0, 2);



                var year = persianCalander.GetYear(DateTime.Now);
                var month = persianCalander.GetMonth(DateTime.Now);
                var day = persianCalander.GetDayOfMonth(DateTime.Now);
                if (!int.TryParse(yearStr, out year) ||
                    !int.TryParse(monthStr, out month) ||
                    !int.TryParse(dayStr, out day))
                    return null;

                var persianCalendar = new PersianCalendar();

                return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DateTime? ConvertJalaliDate14CharToDateTime(string dateTimeStr)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(dateTimeStr) ||
                    String.IsNullOrEmpty(dateTimeStr) ||
                    dateTimeStr.Length != 14) return null;

                var persianCalander = new PersianCalendar();
                var yearStr = dateTimeStr.Substring(0, 4);
                var monthStr = dateTimeStr.Substring(4, 2);
                var dayStr = dateTimeStr.Substring(6, 2);
                var hourStr = dateTimeStr.Substring(8, 2);
                var minStr = dateTimeStr.Substring(10, 2);
                var secStr = dateTimeStr.Substring(12, 2);

                var year = persianCalander.GetYear(DateTime.Now);
                var month = persianCalander.GetMonth(DateTime.Now);
                var day = persianCalander.GetDayOfMonth(DateTime.Now);
                var hour = 0;
                var min = 0;
                var sec = 0;
                if (!int.TryParse(yearStr, out year) ||
                    !int.TryParse(monthStr, out month) ||
                    !int.TryParse(dayStr, out day) ||
                    !int.TryParse(hourStr, out hour) ||
                    !int.TryParse(minStr, out min) ||
                    !int.TryParse(secStr, out sec))
                    return null;

                var persianCalendar = new PersianCalendar();

                return persianCalendar.ToDateTime(year, month, day, hour, min, sec, 0);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static String ConvertDateTimeTo6CharacterJalali(DateTime date)
        {
            try
            {
                var pCalendar = new PersianCalendar();

                var year = pCalendar.GetYear(date);
                var month = pCalendar.GetMonth(date);
                var day = pCalendar.GetDayOfMonth(date);

                var yearConvert = (year >= 1400 ? year - 1400 : year - 1300);

                return String.Format("{0}{1}{2}", yearConvert.ToString("00"), month.ToString("00"), day.ToString("00"));

            }
            catch (Exception e)
            {
                return date.ToString();
            }
        }

        public static String ConvertDateTimeTo8CharacterJalali(DateTime date)
        {
            try
            {
                var pCalendar = new PersianCalendar();

                var year = pCalendar.GetYear(date);
                var month = pCalendar.GetMonth(date);
                var day = pCalendar.GetDayOfMonth(date);


                return String.Format("{0}{1}{2}", year, month.ToString("00"), day.ToString("00"));

            }
            catch (Exception e)
            {
                return date.ToString();
            }
        }

        public static string ConvertDateTimeToInWeekJalaliDate(DateTime issueDateTime)
        {
            try
            {

                var pCalendar = new PersianCalendar();

                var dayInWeek = pCalendar.GetDayOfWeek(issueDateTime);

                return GetJalaliDayOfWeek(dayInWeek) + " " + issueDateTime.ToString("HH:mm");
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string ConvertDateTimeToInShortThisYearJalaliDate(DateTime issueDateTime)
        {
            try
            {
                var pCalendar = new PersianCalendar();

                var monthNumber = pCalendar.GetMonth(issueDateTime);

                return pCalendar.GetDayOfMonth(issueDateTime) + " " + GetMonthNameFromMonth(monthNumber.ToString()) +
                       " " + issueDateTime.ToString("HH:mm");
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetTime(DateTime datetime)
        {
            try
            {
                return datetime.ToLongTimeString();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
        public static string RemoveEscapeAndBreakingSpaceCharacters(string input)
        {
            foreach (var item in GetEscapeAndBreakingSpaceCharacters())
                input = input.Replace(item.Key, item.Value);
            return input;
        }

        public static String RemovePersianDigits(string text)
        {
            var result = "";
            try
            {
                if (String.IsNullOrWhiteSpace(text) ||
                    String.IsNullOrEmpty(text))
                    return "";

                var textStr = StripHTML(text);

                return textStr.Replace("٠", "0").Replace("۰", "0")
                    .Replace("١", "1").Replace("۱", "1")
                    .Replace("٢", "2").Replace("۲", "2")
                    .Replace("٣", "3").Replace("۳", "3")
                    .Replace("٤", "4").Replace("۴", "4")
                    .Replace("٥", "5").Replace("۵", "5")
                    .Replace("٦", "6").Replace("۶", "6")
                    .Replace("٧", "7").Replace("۷", "7")
                    .Replace("٨", "8").Replace("۸", "8")
                    .Replace("٩", "9").Replace("۹", "9");

            }
            catch (Exception ex)
            {
                return text;
            }
        }

        public static string OnlyNumberAccept(string str)
        {
            if (String.IsNullOrWhiteSpace(str) ||
                String.IsNullOrEmpty(str))
                return "";


            var numberCharArray = "0123456789".ToCharArray();
            var charArray = RemovePersianDigits(str).ToCharArray();
            var result = "";
            foreach (var charData in charArray)
                if (numberCharArray.Contains(charData))
                    result += charData;


            return result;
        }
        public static bool IsNumberOnly(string numberStr, bool emptyStringIsNumber = true)
        {
            if (String.IsNullOrWhiteSpace(numberStr) ||
                String.IsNullOrEmpty(numberStr))
                return emptyStringIsNumber;


            var numberCharArray = "0123456789".ToCharArray();
            var charArray = numberStr.Trim().ToCharArray();
            var result = "";
            foreach (var charData in charArray)
                if (!numberCharArray.Contains(charData))
                    return false;

            return true;
        }
        public static bool? IsOnlyHasEnglishAlphabet(string text)
        {
            try
            {
                var charList = "abcdefghijklmnopqrstwvuxyz".ToCharArray().ToList();
                var charArray = text.ToLower().ToCharArray();

                foreach (var charData in charArray)
                {
                    if (!charList.Any(x => x == charData))
                        return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool? IsOnlyHasEnglishAlphabetAndNumber(string text)
        {
            try
            {
                var charList = "abcdefghijklmnopqrstwvuxyz0123456789".ToCharArray().ToList();
                var charArray = text.ToLower().ToCharArray();

                foreach (var charData in charArray)
                {
                    if (!charList.Any(x => x == charData))
                        return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static string ConvertArabicAlphebet(string temp)
        {
            try
            {
                var result = temp.Replace("ی", "ي").Replace("ک", "ك");
                return result;
            }
            catch (Exception)
            {
                return temp;
            }
        }
        public static string ConvertArabicAlphebetAndRemovePersianDigits(string temp)
        {
            try
            {
                var result = RemovePersianDigits(temp.Trim());
                result.Replace("ی", "ي").Replace("ک", "ك");
                return result;
            }
            catch (Exception)
            {
                return temp;
            }
        }

        public static string RemovePersianDigitsAndArabicAlphebet(string temp)
        {
            try
            {
                var result = RemovePersianDigits(temp.Trim());


                result = result.Replace("ي", "ی").Replace("ك", "ک");

                return result;
            }
            catch (Exception)
            {
                return temp;
            }
        }

        public static string GetShortDate()
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(DateTime.Now);

            int month = persianCalendar.GetMonth(DateTime.Now);
            int day = persianCalendar.GetDayOfMonth(DateTime.Now);

            return year + "/" + month + "/" + day;
        }

        public static List<Exception> CheckPasswordPolicy(string password, int minLen, int maxLen, bool showMainResult = false)
        {
            var result = new List<Exception>();
            try
            {
                if (password.Length < minLen)
                    result.Add(new Exception("طول کلمه عبور می بایست حداقل" + minLen + "کاراکتر باشد!"));

                if (password.Length > maxLen)
                    result.Add(new Exception("طول کلمه عبور نمی تواند بیشتر" + maxLen + "کاراکتر باشد!"));

                if (!Regex.IsMatch(password, @"\d+", RegexOptions.ECMAScript))
                    result.Add(new Exception("کلمه عبور می بایست شامل حداقل یک عدد باشد"));

                if (!Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript) &&
                    !Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
                    result.Add(new Exception("کلمه عبور می بایست شامل حداقل یک حرف انگلیسی باشد"));

                /*if (!Regex.IsMatch(password, @".[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript))
                    result.Add(new Exception("کلمه عبور می بایست شامل یک کاراکتر غیر عددی و حرفی باشد"));/**/


                if (!showMainResult && result.Count > 0)
                {
                    result = new List<Exception>()
                    {
                        new Exception("کلمه عبور بايد حداقل  " + minLen +
                                      " و حداکثر " + maxLen +
                                      " كاراكتر و شامل حداقل یک حرف و عدد باشد")
                    };
                }

            }
            catch (Exception ex)
            {
                result.Add(ex);
            }
            return result;
        }

        public static string Base64Encode(string plainText, out Exception exData)
        {
            exData = null;
            try
            {
                if (String.IsNullOrWhiteSpace(plainText) ||
                    String.IsNullOrEmpty(plainText))
                    return "";

                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }

        public static string Base64Decode(string base64EncodedData, out Exception exData)
        {
            exData = null;
            try
            {
                if (String.IsNullOrWhiteSpace(base64EncodedData) ||
                    String.IsNullOrEmpty(base64EncodedData))
                    return "";

                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }

        public static String ConvertFileSizeToPersianTextSize(long fileSize)
        {
            try
            {
                if (fileSize <= 0) return "0";

                if (fileSize < 1024)
                    return fileSize + " " + "بایت (Byte)";
                else if (fileSize >= 1024 && fileSize < 1048576)
                    return (fileSize / 1024) + " " + "کیلو بایت (KB)";
                else if (fileSize >= 1048576 && fileSize < 1073741824)
                    return (fileSize / 1048576) + " " + "مگابایت (MB)";
                else
                    return (fileSize / 1073741824) + " " + "گیگابایت (GB)";
            }
            catch (Exception e)
            {
                return "N/A";
            }
        }

        public static String ConvertFileSizeToPersianTextSizeBaseOn1000BIs1KB(long fileSize)
        {
            try
            {
                if (fileSize <= 0) return "0";

                if (fileSize < 1000)
                    return fileSize + " " + "بایت (Byte)";
                else if (fileSize >= 1000 && fileSize < 1000000)
                    return (fileSize / 1000) + " " + "کیلو بایت (KB)";
                else if (fileSize >= 1000000 && fileSize < 1000000000)
                    return (fileSize / 1000000) + " " + "مگابایت (MB)";
                else
                    return (fileSize / 1000000000) + " " + "گیگابایت (GB)";
            }
            catch (Exception e)
            {
                return "N/A";
            }
        }

        public static String CleanIranMobilePhoneNumber(string mobileNumber)
        {
            var result = mobileNumber;
            try
            {
                result = RemovePersianDigitsAndArabicAlphebet(result.Trim().Replace(" ", "").Replace("-", ""));

                if (result.StartsWith("+98"))
                    result = result.Replace("+98", "0");

                if (result.StartsWith("0098"))
                    result = "+" + result.Replace("+0098", "0");

                if (result.StartsWith("98"))
                    result = "+00" + result.Replace("+0098", "0");

                if (result.StartsWith("00"))
                    result = "+" + result.Replace("+00", "0");

                if (!IsNumberOnly(result))
                    result = OnlyNumberAccept(result);

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static PhoneOperatorTypeEnum GetPhoneNumberOperator(string phoneNumber, bool isMobileNumber)
        {
            try
            {
                if (isMobileNumber)
                    phoneNumber = CleanIranMobilePhoneNumber(phoneNumber);

                var startStr = phoneNumber.Substring(0, 3);
                if (isMobileNumber)
                    startStr = phoneNumber.Substring(0, 4);

                var number = 0;
                var result = PhoneOperatorTypeEnum.Null;
                if (int.TryParse(startStr, out number))
                    result = (PhoneOperatorTypeEnum)number;

                if (result == PhoneOperatorTypeEnum.Null ||
                    !result.ToString().Contains("_"))
                    return PhoneOperatorTypeEnum.Null;

                return result;
            }
            catch (Exception ex)
            {
                return PhoneOperatorTypeEnum.Null;
            }
        }

        public static String MakeMaskPhoneNumber(string phoneNumber, char maskChar = '*')
        {
            try
            {
                if (String.IsNullOrWhiteSpace(phoneNumber) || String.IsNullOrEmpty(phoneNumber)) return "";

                if (phoneNumber.Length < 11) return "09" + new string(maskChar, 9);

                return phoneNumber.Substring(0, 3) + new string(maskChar, 5) + phoneNumber.Substring(8, 3);
            }
            catch (Exception ex)
            {
                return phoneNumber;
            }
        }
        public static String MakeMaskPhoneNumberV2(string phoneNumber, char maskChar = '*')
        {
            try
            {
                if (String.IsNullOrWhiteSpace(phoneNumber) || String.IsNullOrEmpty(phoneNumber)) return "";

                if (phoneNumber.Length < 11) return "09" + new string(maskChar, 9);

                return new string(maskChar, 3) + new string(maskChar, 4) + phoneNumber.Substring(7, 4);
            }
            catch (Exception ex)
            {
                return phoneNumber;
            }
        }
        public static String SubString(string str, int len)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(str) ||
                    String.IsNullOrEmpty(str))
                    return "";

                if (str.Length <= len)
                    return str;

                return str.Substring(0, len) + "...";
            }
            catch (Exception ex)
            {
                return str;
            }
        }
        public static String ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string GetMonthNameFromMonth(string monthStr)
        {
            try
            {
                if (string.IsNullOrEmpty(monthStr) || string.IsNullOrWhiteSpace(monthStr)) return "";

                var month = 0;
                int.TryParse(monthStr, out month);

                if (month <= 0 || month > 12) return "";

                string MonthName = "";
                switch (month)
                {
                    case 1:
                        MonthName = "فروردین";
                        break;
                    case 2:
                        MonthName = "اردیبهشت";
                        break;
                    case 3:
                        MonthName = "خرداد";
                        break;
                    case 4:
                        MonthName = "تیر";
                        break;
                    case 5:
                        MonthName = "مرداد";
                        break;
                    case 6:
                        MonthName = "شهرویور";
                        break;
                    case 7:
                        MonthName = "مهر";
                        break;
                    case 8:
                        MonthName = "آبان";
                        break;
                    case 9:
                        MonthName = "آذر";
                        break;
                    case 10:
                        MonthName = "دی";
                        break;
                    case 11:
                        MonthName = "بهمن";
                        break;
                    case 12:
                        MonthName = "اسفند";
                        break;
                    default:
                        break;
                }
                return MonthName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static bool IsDocumentFile(string extention)
        {
            try
            {
                var ext = extention.ToLower().Replace(".", "");

                if (ext == "doc" || ext == "docx" || ext == "pdf" ||
                    ext == "odt" || ext == "tex" || ext == "wpd" ||
                    ext == "txt" || ext == "rtf" || ext == "xml" ||
                    ext == "csv")
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsMovieFile(string extention)
        {
            try
            {
                var ext = extention.ToLower().Replace(".", "");

                if (ext == "avi" || ext == "mkv" || ext == "mov" ||
                    ext == "mp4" || ext == "qt" || ext == "rm" ||
                    ext == "wmv" || ext == "h264" || ext == "3gp" ||
                    ext == "swf" || ext == "vob" || ext == "wmv" ||
                    ext == "mpg" || ext == "flv" || ext == "mpeg")
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsAudioFile(string extention)
        {
            try
            {
                var ext = extention.ToLower().Replace(".", "");

                if (ext == "mp3" || ext == "wav" || ext == "wma" ||
                    ext == "ogg" || ext == "wpl" || ext == "mid" ||
                    ext == "midi" || ext == "cda" || ext == "aif")
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<String> GetDocumentFileExtention()
        {
            try
            {
                var result = new List<String>()
                {
                    ".doc",
                    ".docx",
                    ".pdf",
                    ".odt",
                    ".tex",
                    ".wpd",
                    ".txt",
                    ".rtf",
                    ".xml",
                    ".csv"
                };

                return result;
            }
            catch (Exception ex)
            {
                return new List<String>();
            }
        }

        public static List<String> GetMovieFileExtention()
        {
            try
            {
                var result = new List<String>()
                {
                    ".avi",
                    ".mkv",
                    ".mov",
                    ".mp4",
                    ".qt",
                    ".rm",
                    ".wmv",
                    ".h264",
                    ".3gp",
                    ".swf",
                    ".vob",
                    ".wmv",
                    ".mpg",
                    ".flv",
                    ".mpeg",
                };

                return result;
            }
            catch (Exception ex)
            {
                return new List<String>();
            }
        }

        public static List<String> GetAudioFileExtention()
        {
            try
            {
                var result = new List<String>()
                {
                    ".mp3",
                    ".wav",
                    ".wma",
                    ".ogg",
                    ".wpl",
                    ".mid",
                    ".midi",
                    ".cda",
                    ".aif"
                };

                return result;
            }
            catch (Exception ex)
            {
                return new List<String>();
            }
        }

        public static string MakeTitleUrl(string title)
        {
            try
            {
                string tmp = PublicUtilities.ConvertArabicToPersian(title);
                tmp = tmp.Replace(" ", "-")
                    .Replace("/", "")
                    .Replace("|", "")
                    .Replace("?", "")
                    .Replace("&", "")
                    .Replace(".", "")
                    .Replace("#", "")
                    .Replace("+", "")
                    .Replace(":", "")
                    .Replace("\\", "")
                    .Replace(",", "")
                    .Replace("=", "");
                tmp = tmp.Trim();
                return tmp;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        #endregion

        #region Helper Methods
        public static string GetMonthNameByMonthNumber(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    return string.Empty;
            }
        }
        private static String GetJalaliDayOfWeek(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return "یکشنبه";
                case DayOfWeek.Monday:
                    return "دوشنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهارشنبه";
                case DayOfWeek.Thursday:
                    return "پنجشنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                case DayOfWeek.Saturday:
                    return "شنبه";
                default:
                    return "نامشخص";
            }
        }
        private static Dictionary<string, string> GetEscapeAndBreakingSpaceCharacters()
        {
            Dictionary<string, string> specialList = new Dictionary<string, string>();
            specialList.Add("&nbsp;", " ");
            specialList.Add("\u200c", " ");
            specialList.Add("\r", " ");
            specialList.Add("\n", " ");

            return specialList;
        }
        #endregion
    }
}