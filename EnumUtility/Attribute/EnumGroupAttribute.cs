using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhs.Bulletin.E_Utility.EnumUtility.Attribute
{
    public class EnumGroupAttribute : System.Attribute
    {
        #region Variables

        public int GroupCode { get; set; }
        public String GroupName { get; set; }
        public String GroupTitle { get; set; }

        public Exception ExceptionData { get { return _exceptionData; } }

        private Exception _exceptionData = null;
        #endregion

        public EnumGroupAttribute(Object data)
        {
            try
            {
                MakeDefaults();

                if (data == null) return;

                if (data.GetType().IsEnum)
                {
                    GroupCode = (int)data.GetHashCode();
                    GroupName = data.ToString();
                    GroupTitle = EnumUtilities.GetEnumDescription((Enum)data);
                }
            }
            catch (Exception ex)
            {
                _exceptionData = ex;
            }
            
        }

        public EnumGroupAttribute(int code, string name, string title = "")
        {
            MakeDefaults();
            GroupCode = code;
            GroupName = name;
            GroupTitle = title;

        }

        #region Helper Methods

        private void MakeDefaults()
        {
            GroupCode = 0;
            GroupName = "";
            GroupTitle = "";
        }

        #endregion

    }
}
