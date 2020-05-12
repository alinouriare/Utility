
namespace Fhs.Bulletin.E_Utility.BankUtility.Attributes
{
    public class EnumCardFormat : System.Attribute
    {
        private string[] _text;

        public string[] Text
        {
            get
            {
                if (_text == null)
                    return new string[] { };
                return _text;
            }
        }

        public EnumCardFormat(params string[] text)
        {
            this._text = text;
        }

    }
}
