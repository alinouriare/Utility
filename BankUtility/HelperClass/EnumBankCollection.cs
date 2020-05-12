using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhs.Bulletin.E_Utility.BankUtility.HelperClass
{
    public class EnumBankCollection: System.Collections.ObjectModel.Collection<BankEnumItem>
    {
        public void RemoveByValue(int val)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Value == val)
                {
                    this.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
