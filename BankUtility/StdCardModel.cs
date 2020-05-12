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
    public class StdCardModel
    {
        #region Variables
        
        public string CardNumer => _cardNumber;
        public BankEnum BankEnum => GetEnumForCard();
        public String PersianBankName
        {
            get { return " بانک " + EnumUtilities.GetEnumDescription(BankEnum); }
        }
        public bool IsCorrect => CheckCardValidation();
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
        private string _cardNumber = "";
        public string PrintFormat => CardPrint();

        public bool IsCheckCardValidation { get; set; }
        #endregion

        #region Constructors
        public StdCardModel()
        {

        }
        public StdCardModel(string cardNumber)
        {
            try
            {
                if (String.IsNullOrEmpty(cardNumber) ||
                    String.IsNullOrWhiteSpace(cardNumber))
                    return;
                _cardNumber = cardNumber.Replace("-", "").Replace(" ", "");
            }
            catch (Exception ex)
            {
                _errorMessage = ex;
                return;
            }


        }
        #endregion

        #region Methods
        public string CardPrint()
        {
            try
            {
                string print = "";
                int div = 3;
                int index = 0;


                if (_cardNumber.Length > 2)
                {
                    for (int i = index; i < _cardNumber.Length; i++)
                    {
                        if (i % 4 == div)
                        {
                            if (i == _cardNumber.Length - 1)
                                print += _cardNumber[i];
                            else
                                print += (_cardNumber[i] + "-");
                        }
                        else
                            print += _cardNumber[i];
                    }
                    return print;
                }
                return _cardNumber;

            }
            catch (Exception ex)
            {
                _errorMessage = ex;
                return null;

            }

        }
        public override string ToString()
        {
            return $"{CardNumer}:{EnumUtilities.GetEnumDescription(BankEnum)}";
        }
        public override bool Equals(object obj)
        {
            try
            {
                if (!(obj is StdCardModel)) return false;

                var data = (StdCardModel)obj;

                if (!string.Equals(data.CardNumer, _cardNumber, StringComparison.CurrentCultureIgnoreCase))
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
        public bool CheckCardValidation()
        {
            try
            {
                if (_cardNumber == null)
                {
                    _errorMessage = new Exception("شماره کارت تکمیل نشده است");
                    return false;
                }
                if (_cardNumber.Length != 16 && _cardNumber.Length != 19)
                {
                    _errorMessage = new Exception("شماره کارت باید  16 یا 19 کاراکتری باشد");
                    return false;
                }

                if (!CheckCardFormat())
                {
                    _errorMessage = new Exception("شماره کارت اشتباه است و موجود نمی باشد");
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

        public BankEnum GetEnumForCard()
        {
            try
            {
                var bankEnum = BankEnum.Unknwon;
                if (IsCorrect || !IsCheckCardValidation)
                {
                    EnumBankCollection bankCollection = BankEnumUtilities.ListBankEnum(typeof(BankEnum));
                    foreach (var item in bankCollection)
                    {
                        if (item.CardFormat.Any(t => t == _cardNumber.Substring(0, 6)))
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
            _cardNumber = "";
            IsCheckCardValidation = true;
            _errorMessage = null;
        }

        public bool CheckCardFormat()
        {
            try
            {
                decimal num = Convert.ToDecimal(_cardNumber);
                if (num > 0)
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
