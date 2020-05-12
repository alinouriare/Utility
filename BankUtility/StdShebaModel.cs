using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.BankUtility.Enums;
using Fhs.Bulletin.E_Utility.BankUtility.HelperClass;
using Fhs.Bulletin.E_Utility.EnumUtility;

namespace Fhs.Bulletin.E_Utility.BankUtility
{
    public class StdShebaModel
    {
        #region Variables


        public string ShebaNumer => _shebaNumber;
        public BankEnum BankEnum => GetEnumForSheba();
        public String AccountNumber
        {
            get
            {
                if (IsCorrect) return _shebaNumber.Substring(13, 13);
                return "";
            }
        }
        public bool IsCorrect => CheckShebaValidation();
        public String ErrorMessage
        {
            get
            {
                if (_errorMessage == null) return "";
                else return _errorMessage.Message;
            }
        }
        private Exception _errorMessage = null;
        public Exception ErrorData => _errorMessage;
        private string _shebaNumber = "";
        public string PrintFormat => ShebaPrint();
        #endregion

        #region Constructors
        public StdShebaModel()
        {

        }
        public StdShebaModel(string shebaNumber)
        {
            try
            {
                if (String.IsNullOrEmpty(shebaNumber) ||
                    String.IsNullOrWhiteSpace(shebaNumber))
                    return;

                _shebaNumber = shebaNumber.ToUpper().Replace("-", "").Replace(" ", "");
            }
            catch (Exception ex)
            {
                _errorMessage = ex;
                return;
            }


        }

        #endregion

        #region Methods
        public string ShebaPrint()
        {
            try
            {
                string print = "";
                int div = 3;
                int index = 0;
                if (_shebaNumber.Length > 1 && _shebaNumber.Substring(0, 2) == "IR")
                {
                    div = 1;
                    index = 2;
                    print += "IR-";
                }

                if (_shebaNumber.Length > 2)
                {
                    for (int i = index; i < _shebaNumber.Length; i++)
                    {
                        if (i % 4 == div)
                        {
                            if (i == _shebaNumber.Length - 1)
                                print += _shebaNumber[i];
                            else
                                print += (_shebaNumber[i] + "-");
                        }
                        else
                            print += _shebaNumber[i];
                    }
                    return print;
                }
                return _shebaNumber.ToUpper();

            }
            catch (Exception ex)
            {
                _errorMessage = ex;
                return null;

            }

        }
        public override string ToString()
        {
            return $"{ShebaNumer}:{EnumUtilities.GetEnumDescription(BankEnum)}";
        }
        public override bool Equals(object obj)
        {
            try
            {
                if (!(obj is StdShebaModel)) return false;

                var data = (StdShebaModel)obj;

                if (!string.Equals(data.ShebaNumer, _shebaNumber, StringComparison.CurrentCultureIgnoreCase))
                    return false;
                if (data.BankEnum != BankEnum)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = ex;
                return false;
            }

        }
        public bool CheckShebaValidation()
        {
            try
            {
                if (_shebaNumber == null)
                {
                    _errorMessage = new Exception("شماره شبا تکمیل نشده است");
                    return false;
                }
                if (_shebaNumber.Length != 26)
                {
                    _errorMessage = new Exception("شماره شبا باید 26 کاراکتری باشد");
                    return false;
                }
                if (_shebaNumber.Substring(0, 2).ToUpper() != "IR")
                {
                    _errorMessage = new Exception("شماره شبا باید با IR شروع گردد");
                    return false;
                }
                if (!CheckShebaFormat())
                {
                    _errorMessage = new Exception("شماره شبا اشتباه است و موجود نمی باشد");
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                _errorMessage = ex;
                return false;

            }

        }

        public BankEnum GetEnumForSheba()
        {
            try
            {
                var bankEnum = BankEnum.Unknwon;
                if (IsCorrect)
                {
                    EnumBankCollection bankCollection = BankEnumUtilities.ListBankEnum(typeof(BankEnum));
                    foreach (var item in bankCollection)
                    {
                        if (item.ShebaFormat.Any(t => t == _shebaNumber.Substring(4, 3)))
                        {

                            if (!Enum.TryParse(item.Name, out bankEnum))
                                return bankEnum;
                            return bankEnum;
                        }
                    }
                }
                return bankEnum;
            }
            catch (Exception ex)
            {
                _errorMessage = ex;
                return BankEnum.None;

            }

        }
        #endregion

        #region Helper Methods

        public void MakeDefault()
        {
            _shebaNumber = "";
            _errorMessage = null;
        }

        public bool CheckShebaFormat()
        {
            try
            {
                string sheba = ShebaNumer;
                sheba = sheba.ToUpper();
                sheba = sheba.Replace("A", "10");
                sheba = sheba.Replace("B", "11");
                sheba = sheba.Replace("C", "12");
                sheba = sheba.Replace("D", "13");
                sheba = sheba.Replace("E", "14");
                sheba = sheba.Replace("F", "15");
                sheba = sheba.Replace("G", "16");
                sheba = sheba.Replace("H", "17");
                sheba = sheba.Replace("I", "18");
                sheba = sheba.Replace("J", "19");
                sheba = sheba.Replace("K", "20");
                sheba = sheba.Replace("L", "21");
                sheba = sheba.Replace("M", "22");
                sheba = sheba.Replace("N", "23");
                sheba = sheba.Replace("O", "24");
                sheba = sheba.Replace("P", "25");
                sheba = sheba.Replace("Q", "26");
                sheba = sheba.Replace("R", "27");
                sheba = sheba.Replace("S", "28");
                sheba = sheba.Replace("T", "29");
                sheba = sheba.Replace("U", "30");
                sheba = sheba.Replace("V", "31");
                sheba = sheba.Replace("W", "32");
                sheba = sheba.Replace("X", "33");
                sheba = sheba.Replace("Y", "34");
                sheba = sheba.Replace("Z", "35");
                var res = sheba.Substring(0, 6);
                sheba = sheba + res;
                var finalSheba = sheba.Substring(6, sheba.Length - 6);
                decimal value = Convert.ToDecimal(finalSheba);
                decimal re = value % 97;
                if (re == 1)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                _errorMessage = ex;
                return false;
            }


        }

        #endregion

    }
}
