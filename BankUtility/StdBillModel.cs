using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.BankUtility.Enums;
using Fhs.Bulletin.E_Utility.EnumUtility;

namespace Fhs.Bulletin.E_Utility.BankUtility
{
    public class StdBillModel
    {
        #region Variables

        public BillGetInformationTypeEnum ModelGetType
        {
            get { return _modelGetType; }
        }

        public String BillNumber
        {
            get
            {
                if (_billNumber.Length <= 6)
                    return new string('0', 6 - _billNumber.Length) + _billNumber;


                if (_billNumber.Length <= 13)
                    return new string('0', 13 - _billNumber.Length) + _billNumber;

                return _billNumber.Substring(0, 13);
            }
        }
        public String PaymentNumber
        {
            get
            {
                if (_paymentNumber.Length <= 6)
                    return new string('0', 6 - _paymentNumber.Length) + _paymentNumber;


                if (_paymentNumber.Length <= 13)
                    return new string('0', 13 - _paymentNumber.Length) + _paymentNumber;

                return _paymentNumber.Substring(0, 13);
            }
        }
        public BillEnum BillType { get { return _findBillType(); } }
        public Decimal Amount
        {
            get
            {
                return PaymentNumber_AmountNumber * 1000;
            }
        }
        public String PersianBillType
        {
            get
            {
                try
                {
                    return EnumUtilities.GetEnumDescription(BillType);
                }
                catch (Exception e)
                {
                    return "نامشخص";
                }
            }
        }

        public int BillNumber_ControlCode
        {
            get
            {
                var billNumberStr = _billNumber;
                billNumberStr = _reverseString(billNumberStr);
                if (String.IsNullOrEmpty(billNumberStr) ||
                    String.IsNullOrWhiteSpace(billNumberStr) ||
                    billNumberStr.Length == 0)
                    return -1;

                var codeStr = billNumberStr.Substring(0, 1);
                var code = -1;

                int.TryParse(codeStr, out code);
                return code;
            }
        }
        public int BillNumber_BillType
        {
            get
            {
                var billNumberStr = _billNumber;
                billNumberStr = _reverseString(billNumberStr);
                if (String.IsNullOrEmpty(billNumberStr) ||
                    String.IsNullOrWhiteSpace(billNumberStr) ||
                    billNumberStr.Length < 2)
                    return -1;

                var codeStr = billNumberStr.Substring(1, 1);
                var code = -1;

                int.TryParse(codeStr, out code);
                return code;
            }
        }
        public int BillNumber_SubsidiaryCode
        {
            get
            {
                var billNumberStr = _billNumber;
                billNumberStr = _reverseString(billNumberStr);
                if (String.IsNullOrEmpty(billNumberStr) ||
                    String.IsNullOrWhiteSpace(billNumberStr) ||
                    billNumberStr.Length < 5)
                    return -1;

                var codeStr = billNumberStr.Substring(2, 3);
                var code = -1;

                codeStr = _reverseString(codeStr);
                int.TryParse(codeStr, out code);
                return code;
            }
        }
        public String BillNumber_CaseNumber
        {
            get
            {
                var billNumberStr = _billNumber;
                billNumberStr = _reverseString(billNumberStr);
                if (String.IsNullOrEmpty(billNumberStr) ||
                    String.IsNullOrWhiteSpace(billNumberStr) ||
                    billNumberStr.Length < 6)
                    return "";

                var codeStr = billNumberStr.Substring(5, billNumberStr.Length - 5);
                codeStr = _reverseString(codeStr);
                return codeStr;
            }
        }

        public int PaymentNumber_ControlCode01
        {
            get
            {
                var paymentNumberStr = _paymentNumber;
                paymentNumberStr = _reverseString(paymentNumberStr);
                if (String.IsNullOrEmpty(paymentNumberStr) ||
                    String.IsNullOrWhiteSpace(paymentNumberStr) ||
                    paymentNumberStr.Length == 0)
                    return -1;

                var codeStr = paymentNumberStr.Substring(0, 1);
                var code = -1;

                int.TryParse(codeStr, out code);
                return code;
            }
        }
        public int PaymentNumber_ControlCode02
        {
            get
            {
                var paymentNumberStr = _paymentNumber;
                paymentNumberStr = _reverseString(paymentNumberStr);
                if (String.IsNullOrEmpty(paymentNumberStr) ||
                    String.IsNullOrWhiteSpace(paymentNumberStr) ||
                    paymentNumberStr.Length < 2)
                    return -1;

                var codeStr = paymentNumberStr.Substring(1, 1);
                var code = -1;

                int.TryParse(codeStr, out code);
                return code;
            }
        }
        public int PaymentNumber_PeriodeNumber
        {
            get
            {
                var paymentNumberStr = _paymentNumber;
                paymentNumberStr = _reverseString(paymentNumberStr);
                if (String.IsNullOrEmpty(paymentNumberStr) ||
                    String.IsNullOrWhiteSpace(paymentNumberStr) ||
                    paymentNumberStr.Length < 4)
                    return -1;

                var codeStr = paymentNumberStr.Substring(2, 2);
                codeStr = _reverseString(codeStr);
                var code = -1;

                int.TryParse(codeStr, out code);
                return code;
            }
        }
        public int PaymentNumber_YearNumber
        {
            get
            {
                var paymentNumberStr = _paymentNumber;
                paymentNumberStr = _reverseString(paymentNumberStr);
                if (String.IsNullOrEmpty(paymentNumberStr) ||
                    String.IsNullOrWhiteSpace(paymentNumberStr) ||
                    paymentNumberStr.Length < 4)
                    return -1;

                var codeStr = paymentNumberStr.Substring(4, 1);
                var code = -1;

                int.TryParse(codeStr, out code);
                return code;
            }
        }
        public decimal PaymentNumber_AmountNumber
        {
            get
            {
                var paymentNumberStr = _paymentNumber;
                if (String.IsNullOrEmpty(paymentNumberStr) ||
                    String.IsNullOrWhiteSpace(paymentNumberStr) ||
                    paymentNumberStr.Length < 6)
                    return 0;

                var codeStr = paymentNumberStr.Substring(0, (paymentNumberStr.Length - 5));
                var code = (decimal)0;

                decimal.TryParse(codeStr, out code);
                return code;
            }
        }

        private String _billNumber = "";
        private String _paymentNumber = "";
        private BillGetInformationTypeEnum _modelGetType = BillGetInformationTypeEnum.Null;

        #endregion

        #region Constructors

        public StdBillModel()
        {
            MakeDefault();
        }
        public StdBillModel(BillGetInformationTypeEnum type, string number)
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
                    if (number.Length >= 13)
                        _billNumber = number.Substring(0, 13);

                    _paymentNumber = number.Replace(_billNumber,"");
                    break;
            }
        }
        public StdBillModel(string billNumber, string paymentNumber)
        {
            MakeDefault();

            _modelGetType = BillGetInformationTypeEnum.BillAndPaymentNumbe;
            _billNumber = billNumber ?? "";
            _paymentNumber = paymentNumber ?? "";
        }

        #endregion

        #region Helper Methods

        public override string ToString()
        {
            try
            {
                return String.Format("شناسه قبض: {0} - شناسه پرداخت : {1} - مبلغ: {2} ریال - سازمان: {3}.",
                    BillNumber, PaymentNumber, Amount.ToString("N0"), PersianBillType);
            }
            catch (Exception e)
            {
                return "نامشخص";
            }
        }

        private void MakeDefault()
        {
            _modelGetType = BillGetInformationTypeEnum.Null;
            _billNumber = "";
            _paymentNumber = "";
        }
        private BillEnum _findBillType()
        {
            try
            {
                if (String.IsNullOrEmpty(BillNumber) ||
                   String.IsNullOrWhiteSpace(BillNumber))
                    return BillEnum.NotSet;

                switch (BillNumber_BillType)
                {
                    case 0:
                        return BillEnum.None;
                    case 1:
                        return BillEnum.WaterDepartment;
                    case 2:
                        return BillEnum.PowerDepartment;
                    case 3:
                        return BillEnum.GasDepartment;
                    case 4:
                        return BillEnum.Telecommunications;
                    case 5:
                        return BillEnum.Mci;
                    case 6:
                        return BillEnum.TaxMunicipality;
                    case 7:
                        return BillEnum.None;
                    case 8:
                        return BillEnum.TaxOrganization;
                    case 9:
                        return BillEnum.DrivingFine;
                }
                return BillEnum.None;

            }
            catch (Exception e)
            {
                return BillEnum.None;
            }
        }
        private string _reverseString(string input)
        {
            if (input == null ||
                input == "")
                return input;

            var inputarray = input.ToCharArray();
            Array.Reverse(inputarray);
            return new string(inputarray);
        }
        #endregion
    }
}
