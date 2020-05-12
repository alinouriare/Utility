using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhs.Bulletin.E_Utility.EnumUtility.Attribute
{
    public class EnumDescription : System.Attribute
    {
        #region Variables
        public string Text;
        public string ShortName;
        #endregion

        #region Constructors
        public EnumDescription(string text)
        {
            MakeDefaults();

            Text = text;
        }

        public EnumDescription(string text, string shortName)
        {
            MakeDefaults();

            Text = text;
            ShortName = shortName;
        }
        #endregion

        #region Helper Methods
        private void MakeDefaults()
        {
            Text = "";
            ShortName = "";
        }
        #endregion

    }
}