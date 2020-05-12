using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhs.Bulletin.E_Utility.EnumUtility.Attribute
{
    public class EnumPropertyTypeAttribute:System.Attribute
    {
        #region Variables
        public Type PropertyType;
        public Object DefaultValue;
        #endregion
        
        public EnumPropertyTypeAttribute(Object defaultValue)
        {
            if (defaultValue == null) return;

            PropertyType = defaultValue.GetType();
            DefaultValue = defaultValue;
            
        }

        #region Helper Methods

        object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }

        #endregion
    }
}
