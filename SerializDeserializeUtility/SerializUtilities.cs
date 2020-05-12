using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fhs.Bulletin.E_Utility.SerializDeserializeUtility
{
    public static class SerializUtilities
    {
        #region Variables
        #endregion

        #region Methods
        public static String JsonSerialize(Object data)
        {
            Exception e = null;
            return JsonSerialize(data, out e);
        }
        public static String JsonSerialize(Object data, out Exception exData)
        {
            exData = null;
            try
            {
                return JsonConvert.SerializeObject(data);
            }
            catch (Exception e)
            {
                exData = e;
                return "";
            }
        }

        public static Object JsonDeSerialize(string dataStr, Type type)
        {
            Exception e = null;
            return JsonDeSerialize(dataStr, type, out e);
        }
        public static Object JsonDeSerialize(string dataStr,Type type ,out Exception exData)
        {
            exData = null;
            try
            {
                return JsonConvert.DeserializeObject(dataStr, type);
            }
            catch (Exception e)
            {
                exData = e;
                return null;
            }
        }

        #endregion

        #region Helper Methods
        #endregion
    }
}
