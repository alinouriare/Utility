using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.BankUtility.Enums;
using Fhs.Bulletin.E_Utility.PublicUtility;
using Newtonsoft.Json;

namespace Fhs.Bulletin.E_Utility.BankUtility
{
    public class SaderatBillModel
    {
        #region Variables

        public String BillNumber
        {
            get { return _billNumber; }
        }
        public String BillController01
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(BillNumber) ||
                        String.IsNullOrEmpty(BillNumber))
                        return "";

                    return BillNumber.Substring(0, 1);
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }
        public String CompanyCode
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(BillNumber) ||
                        String.IsNullOrEmpty(BillNumber))
                        return "";
                    if (BillNumber.Length >= 5)
                        return BillNumber.Substring(1, 4);
                    return "";
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }
        public String BillController02
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(BillNumber) ||
                        String.IsNullOrEmpty(BillNumber))
                        return "";
                    if (BillNumber.Length >= 6)
                        return BillNumber.Substring(5, 1);

                    return "";
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }
        public String FileId
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(BillNumber) ||
                        String.IsNullOrEmpty(BillNumber))
                        return "";

                    var endofNumber = BillNumber.Length <= 18 ? BillNumber.Length : 18;

                    if (BillNumber.Length > 6)
                        return BillNumber.Substring(6, endofNumber - 6);

                    return "";
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }

        public String PaymentNumber
        {
            get { return _paymentNumber; }
        }
        public String PaymentController01
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(PaymentNumber) ||
                        String.IsNullOrEmpty(PaymentNumber))
                        return "";

                    return PaymentNumber.Substring(0, 1);
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }
        public String PaymentController02
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(PaymentNumber) ||
                        String.IsNullOrEmpty(PaymentNumber))
                        return "";
                    if (PaymentNumber.Length >= 2)
                        return PaymentNumber.Substring(1, 1);
                    return "";
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }
        public String PaymentOneDigit
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(PaymentNumber) ||
                        String.IsNullOrEmpty(PaymentNumber))
                        return "";
                    if (PaymentNumber.Length >= 3)
                        return PaymentNumber.Substring(2, 1);
                    return "";
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }

        public String AdditionalInfo4DigiStr
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(PaymentNumber) ||
                        String.IsNullOrEmpty(PaymentNumber))
                        return "";
                    if (PaymentNumber.Length >= 7)
                        return PaymentNumber.Substring(3, 4);

                    return "";
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }
        public int AdditionalInfo4Digi
        {
            get
            {
                try
                {
                    var numberStr = AdditionalInfo4DigiStr;
                    var number = 0;
                    if (String.IsNullOrWhiteSpace(numberStr) ||
                        String.IsNullOrEmpty(numberStr) ||
                        !int.TryParse(numberStr,out number))
                        return 0;

                    return number;
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return 0;
                }
            }
        }
        public String BillAmountStr
        {
            get
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(PaymentNumber) ||
                        String.IsNullOrEmpty(PaymentNumber))
                        return "";

                    var endofNumber = PaymentNumber.Length <= 18 ? PaymentNumber.Length : 18;

                    if (PaymentNumber.Length >= 8)
                        return PaymentNumber.Substring(7, endofNumber - 7);

                    return "";
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return "";
                }
            }
        }
        public decimal BillAmount
        {
            get
            {
                try
                {
                    var numberStr = BillAmountStr;
                    decimal number = 0;
                    if (String.IsNullOrWhiteSpace(numberStr) ||
                        String.IsNullOrEmpty(numberStr) ||
                        !decimal.TryParse(numberStr, out number))
                        return 0;

                    return number;
                }
                catch (Exception ex)
                {
                    AddExceptionData(ex);
                    return 0;
                }
            }
        }
        
        private List<Exception> _exceptionDataList = new List<Exception>();
        private String _billNumber = "";
        private String _paymentNumber = "";
        private BillGetInformationTypeEnum _modelGetType = BillGetInformationTypeEnum.Null;

        #endregion

        public SaderatBillModel(BillGetInformationTypeEnum type, string number)
        {
            MakeDefault();
            if (type == BillGetInformationTypeEnum.Null) return;

            _modelGetType = type;
            switch (type)
            {
                case BillGetInformationTypeEnum.BillNumberOnly:
                    _billNumber = number;
                    break;
                case BillGetInformationTypeEnum.PaymentNumberOnly:
                    _paymentNumber = number;
                    break;
                case BillGetInformationTypeEnum.BillAndPaymentNumbe:
                    
                    if (number.Length >= 18)
                        _billNumber = number.Substring(0, 18);

                    _paymentNumber = number.Replace(_billNumber, "");
                    break;
            }
        }
        public SaderatBillModel(string billNumber, string paymentNumber)
        {
            MakeDefault();

            _modelGetType = BillGetInformationTypeEnum.BillAndPaymentNumbe;
            _billNumber = billNumber ?? "";
            _paymentNumber = paymentNumber ?? "";
        }
        
        #region Methods

        public bool BillController01IsCorrect()
        {
            try
                {
                var stdModel = new SaderatBillModel(BillNumber, PaymentNumber);

                if (stdModel.BillController01 != BillController01)
                    return false;
                return true;
            }
                catch (Exception ex)
                {
                AddExceptionData(ex);
                return false;
            }
        }
        public bool BillController02IsCorrect()
        {
            try
                {
                var stdModel = new SaderatBillModel(BillNumber, PaymentNumber);

                if (stdModel.BillController02 != BillController02)
                    return false;
                return true;
            }
                catch (Exception ex)
                {
                AddExceptionData(ex);
                return false;
            }
        }
        public bool PaymentController01IsCorrect()
        {
            try
                {
                var stdModel = new SaderatBillModel(BillNumber, PaymentNumber);

                if (stdModel.PaymentController01 != PaymentController01)
                    return false;
                return true;
            }
                catch (Exception ex)
                {
                AddExceptionData(ex);
                return false;
            }
        }
        public bool PaymentController02IsCorrect()
        {
            try
            {
                var stdModel = new SaderatBillModel(BillNumber, PaymentNumber);

                if (stdModel.PaymentController02 != PaymentController02)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                AddExceptionData(ex);
                return false;
            }
        }

        public static SaderatBillModel GenerateSaderatBillModel(string companyId, string fileId, decimal amount,int fourDigitController, out Exception exData)
        {
            exData = null;
            try
            {
                var billNumber = GenerateBillNumber(companyId, fileId, out exData);
                if (exData != null)
                    return null;

                var paymentNumber = GeneratePaymentNumber(billNumber, amount, fourDigitController, out exData);
                if (exData != null)
                    return null;

                return new SaderatBillModel(billNumber,paymentNumber);
            }
            catch (Exception ex)
            {
                exData = ex;
                return null;
            }
        }
        public static SaderatBillModel GenerateSaderatBillModel(string companyId, string fileId, decimal amount, DateTime paymentDate, out Exception exData)
        {
            exData = null;
            try
            {
                var billNumber = GenerateBillNumber(companyId, fileId, out exData);
                if (exData != null)
                    return null;

                var paymentNumber = GeneratePaymentNumber(billNumber,amount,paymentDate, out exData);
                if (exData != null)
                    return null;

                return new SaderatBillModel(billNumber, paymentNumber);
            }
            catch (Exception ex)
            {
                exData = ex;
                return null;
            }
        }

        public static string GenerateBillNumber(string companyId, string fileId, out Exception exData)
        {
            exData = null;
            var billNumber = "";
            try
            {
                #region Check Basics

                if (String.IsNullOrWhiteSpace(fileId) ||
                    String.IsNullOrEmpty(fileId) ||
                    fileId.Length > 12)
                    throw new Exception("طول پرونده نمی تواند خالی یا بیش از 12 باشد");

                if (String.IsNullOrWhiteSpace(companyId) ||
                    String.IsNullOrEmpty(companyId) ||
                    companyId.Length > 4)
                    throw new Exception("کد شرکت نمی تواند خالی یا بیش از 4 باشد");

                #endregion

                //Step 1 Make FileId
                billNumber = fileId.PadLeft(12, '0');

                //Step 2 Make Controller2
                Exception exData_Calc = null;
                var billController2 = CalculateControlDigit(billNumber, SaderatBillControlDigitModeEnum.BillController02,
                    out exData_Calc);
                if (exData_Calc != null)
                    throw exData_Calc;

                //Step 3 Add CompanyId 
                billNumber = companyId.PadLeft(4, '0') + billController2 + billNumber;

                //Step 4 Make Controller1
                exData_Calc = null;
                var billController1 = CalculateControlDigit(billNumber, SaderatBillControlDigitModeEnum.BillController01,
                    out exData_Calc);
                if (exData_Calc != null)
                    throw exData_Calc;

                //Step 5 return Code
                billNumber = billController1 + billNumber;
                return billNumber;
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }
        public static String GeneratePaymentNumber(string companyId, string fileId, DateTime billPaymentTime, decimal amount, out Exception exData)
        {
            exData = null;
            try
            {
                var startBillTime = ConvertJalaliDateToDateTime("1387/01/01");

                #region Check Basics

                if (String.IsNullOrWhiteSpace(fileId) ||
                    String.IsNullOrEmpty(fileId) ||
                    fileId.Length > 12)
                    throw new Exception("طول پرونده نمی تواند خالی یا بیش از 12 باشد");

                if (String.IsNullOrWhiteSpace(companyId) ||
                    String.IsNullOrEmpty(companyId) ||
                    companyId.Length > 4)
                    throw new Exception("کد شرکت نمی تواند خالی یا بیش از 4 باشد");

                if (startBillTime == null)
                    throw new Exception("مشکل در عملکرد محاسبه تاریخ مبداً ساخت قبض");

                if (amount <= 0)
                    throw new Exception("مبلغ قرارداد باید بزرگتر از صفر باشد");

                if (billPaymentTime < startBillTime.Value)
                    throw new Exception("تاریخ پرداخت قبض باید بزرگتر از اول فرودین 1387 باشد (1387/01/01)");

                if (amount != (decimal)Decimal.Truncate(amount))
                    throw new Exception("مبلغ قبض نمی تواند شامل اعشار باشد");

                #endregion

                //Step 01 Make BillNumber
                var billNumber = GenerateBillNumber(companyId, fileId, out exData);
                if (exData != null)
                    throw exData;


                return GeneratePaymentNumber(billNumber, amount, ((int)(billPaymentTime - startBillTime.Value).TotalDays), out exData);
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }
        public static String GeneratePaymentNumber(string companyId, string fileId, int fourAdditionalDateInPaymentNumber, decimal amount, out Exception exData)
        {
            exData = null;
            try
            {

                #region Check Basics

                if (String.IsNullOrWhiteSpace(fileId) ||
                    String.IsNullOrEmpty(fileId) ||
                    fileId.Length > 12)
                    throw new Exception("طول پرونده نمی تواند خالی یا بیش از 12 باشد");

                if (String.IsNullOrWhiteSpace(companyId) ||
                    String.IsNullOrEmpty(companyId) ||
                    companyId.Length > 4)
                    throw new Exception("کد شرکت نمی تواند خالی یا بیش از 4 باشد");

                if (amount <= 0)
                    throw new Exception("مبلغ قرارداد باید بزرگتر از صفر باشد");

                if (amount != (decimal)Decimal.Truncate(amount))
                    throw new Exception("مبلغ قبض نمی تواند شامل اعشار باشد");

                #endregion

                //Step 01 Make BillNumber
                var billNumber = GenerateBillNumber(companyId, fileId, out exData);
                if (exData != null)
                    throw exData;


                return GeneratePaymentNumber(billNumber, amount, fourAdditionalDateInPaymentNumber, out exData);
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }
        public static String GeneratePaymentNumber(string billNumber, decimal amount, DateTime billPaymentTime, out Exception exData)
        {
            exData = null;
            try
            {
                var startBillTime = ConvertJalaliDateToDateTime("1387/01/01");

                #region Check Basics
                
                if (startBillTime == null)
                    throw new Exception("مشکل در عملکرد محاسبه تاریخ مبداً ساخت قبض");

                if (amount <= 0)
                    throw new Exception("مبلغ قرارداد باید بزرگتر از صفر باشد");

                if (billPaymentTime < startBillTime.Value)
                    throw new Exception("تاریخ پرداخت قبض باید بزرگتر از اول فرودین 1387 باشد (1387/01/01)");

                if (amount != (decimal)Decimal.Truncate(amount))
                    throw new Exception("مبلغ قبض نمی تواند شامل اعشار باشد");

                #endregion
                


                return GeneratePaymentNumber(billNumber, amount, ((int)(billPaymentTime - startBillTime.Value).TotalDays), out exData);
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }
        public static String GeneratePaymentNumber(string billNumber, decimal amount, int fourAdditionalDateInPaymentNumber, out Exception exData)
        {
            exData = null;
            try
            {
                #region Check Basics

                if (String.IsNullOrWhiteSpace(billNumber) ||
                    String.IsNullOrEmpty(billNumber) ||
                    billNumber.Length > 18)
                    throw new Exception("شناسه قبض نمی تواند خالی یا بیش از 18 کاراکتر باشد");


                if (fourAdditionalDateInPaymentNumber > 9999)
                    throw new Exception("مقدار اضافه قبض پرداخت نمی تواند بیش از 9999 باشد");

                if (amount <= 0)
                    throw new Exception("مبلغ قرارداد باید بزرگتر از صفر باشد");


                if (amount != (decimal)Decimal.Truncate(amount))
                    throw new Exception("مبلغ قبض نمی تواند شامل اعشار باشد");

                #endregion

                //Step 02 Add BillPayement
                var payementNumber = "1" + fourAdditionalDateInPaymentNumber.ToString("0000") + amount.ToString();

                //Step 03 Calculate PaymentController02
                var paymentController02 = CalculateControlDigit(payementNumber, SaderatBillControlDigitModeEnum.PaymentController02, out exData);
                if (exData != null)
                    throw exData;

                //Step 04 Add Payment Controller02 to PaymentNumber
                payementNumber = paymentController02 + payementNumber;

                //Calculate PaymentController01
                var paymentController01 = CalculateControlDigit(billNumber + payementNumber,
                    SaderatBillControlDigitModeEnum.PaymentController01, out exData);
                if (exData != null)
                    throw exData;

                //Add PaymentController01 to PaymentNumber
                payementNumber = paymentController01 + payementNumber;
                return payementNumber;
            }
            catch (Exception ex)
            {
                exData = ex;
                return "";
            }
        }
        
        
        public static decimal CalculateControlDigit(decimal number, SaderatBillControlDigitModeEnum mode, out Exception exData)
        {
            return CalculateControlDigit(number.ToString(),mode,out exData);
        }
        public static decimal CalculateControlDigit(string numberStr, SaderatBillControlDigitModeEnum mode, out Exception exData)
        {
            exData = null;
            try
            {
                if (String.IsNullOrWhiteSpace(numberStr) ||
                    String.IsNullOrEmpty(numberStr)) throw new Exception("عددی برای کنترل یافت نشد!");

                decimal sum = 0;
                var counterMultiple = 2;
                var currentStr = numberStr;
                while (!String.IsNullOrEmpty(currentStr))
                {
                    var digit = 0;
                    int.TryParse(currentStr.Substring(currentStr.Length - 1, 1), out digit);
                    
                    sum += (digit * counterMultiple);

                    counterMultiple++;
                    if (counterMultiple >= 8)
                        counterMultiple = 2;

                    currentStr = currentStr.Substring(0, currentStr.Length-1);
                }

                var finalMode = sum % 11;

                if ((mode == SaderatBillControlDigitModeEnum.BillController02 ||
                     mode == SaderatBillControlDigitModeEnum.PaymentController02) &&
                    (finalMode == 1 || finalMode == 0))
                    finalMode = 0;
                else if ((mode == SaderatBillControlDigitModeEnum.BillController01 ||
                          mode == SaderatBillControlDigitModeEnum.PaymentController01)
                         && (finalMode == 1 || finalMode == 0))
                    finalMode = 9;
                else
                    finalMode = 11 - finalMode;

                return finalMode;
            }
            catch (Exception ex)
            {
                exData = ex;
                return -1;
            }
        }
        #endregion

        #region Helper Methods

        private void MakeDefault()
        {
            _exceptionDataList = new List<Exception>();
            _billNumber = "";
            _paymentNumber = "";
            _modelGetType = BillGetInformationTypeEnum.Null;
        }
        protected void AddExceptionData(Exception exData)
        {
            try
            {
                if (_exceptionDataList == null)
                    _exceptionDataList = new List<Exception>();

                if (exData == null) return;

                _exceptionDataList.Add(exData);
            }
            catch (Exception ex)
            {

            }
        }
        public List<Exception> GetExceptionDataList()
        {
            try
            {
                if (_exceptionDataList == null ||
                    _exceptionDataList.Count == 0) return new List<Exception>();

                var result = new List<Exception>();

                result.AddRange(_exceptionDataList);

                return result;
            }
            catch (Exception ex)
            {
                return new List<Exception>();
            }

        }

        public override string ToString()
        {
            return BillNumber + " - " + PaymentNumber;
        }

        private static DateTime? ConvertJalaliDateToDateTime(string dateTimeStr)
        {
            try
            {
                var persianCalander = new PersianCalendar();
                var array = dateTimeStr.Split(new string[] { "/", "\\" }, StringSplitOptions.None);

                if (array.Length < 3) return null;

                var year = persianCalander.GetYear(DateTime.Now);
                var month = persianCalander.GetMonth(DateTime.Now);
                var day = persianCalander.GetDayOfMonth(DateTime.Now);
                if (!int.TryParse(array[0], out year) || !int.TryParse(array[1], out month) ||
                    !int.TryParse(array[2], out day))
                    return null;
                

                return persianCalander.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}