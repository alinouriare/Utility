using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhs.Bulletin.E_Utility.EnumUtility.Attribute
{
    public class EnumParentValue: System.Attribute
    {
        public int Value;

        public EnumParentValue(int Value)
        {
            this.Value = Value;
        }
    }
}
