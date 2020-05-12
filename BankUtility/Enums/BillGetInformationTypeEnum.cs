using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.EnumUtility.Attribute;

namespace Fhs.Bulletin.E_Utility.BankUtility.Enums
{
    public enum BillGetInformationTypeEnum
    {
        Null = 0,
        [EnumDescription("فقط دارای شناسه قبض")]
        BillNumberOnly = 10,
        [EnumDescription("فقط دارای شناسه پرداخت")]
        PaymentNumberOnly = 20,
        [EnumDescription("دارای شناسه قبض و شناسه پرداخت")]
        BillAndPaymentNumbe = 30
    }
}
