using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.EnumUtility;

namespace Fhs.Bulletin.E_Utility.BankUtility.HelperClass
{
    public class BankEnumItem: EnumItem
    {
        public string[] CardFormat { get; set; }
        public string[] ShebaFormat { get; set; }
    }
}
