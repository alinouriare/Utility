
namespace Fhs.Bulletin.E_Utility.BankUtility.Attributes
{
    public class EnumShebaFormat : System.Attribute
    {
        private string[] _text;

        public string[] Text
        {
            get
            {
                if(_text == null)
                    return new string[] {};
                return _text;
            }
        }

        public EnumShebaFormat(params string[] text)
        {
            this._text = text;
        }
       
    }
}
