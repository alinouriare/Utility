using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fhs.Bulletin.E_Utility.BankUtility.Attributes;
using Fhs.Bulletin.E_Utility.EnumUtility.Attribute;

namespace Fhs.Bulletin.E_Utility.BankUtility.HelperClass
{
    public static class BankEnumUtilities
    {
        public static EnumBankCollection ListBankEnum(Type type)
        {
            EnumBankCollection result = new EnumBankCollection();
            try
            {
                if (type.IsEnum)
                {
                    Array enValues = Enum.GetValues(type);

                    MemberInfo[] mis = type.GetMembers();
                    foreach (MemberInfo mi in mis)
                    {
                        object[] attribs = mi.GetCustomAttributes(typeof(EnumDescription), false);
                        object[] attribsParent = mi.GetCustomAttributes(typeof(EnumParentValue), false);
                        object[] attribsCard = mi.GetCustomAttributes(typeof(EnumCardFormat), false);
                        object[] attribsSheba = mi.GetCustomAttributes(typeof(EnumShebaFormat), false);
                        if (attribs.Length != 0)
                        {
                            BankEnumItem item = new BankEnumItem
                            {
                                Name = mi.Name,
                                Value = (int)Enum.Parse(type, mi.Name),
                                Description = ((EnumDescription)attribs[0]).Text,
                                ParentValue = attribsParent.Length != 0 ? ((EnumParentValue)attribsParent[0]).Value : 0,
                                CardFormat = attribsCard.Length > 0 ? ((EnumCardFormat)attribsCard[0]).Text : new string[] { },
                                ShebaFormat = attribsSheba.Length > 0 ? ((EnumShebaFormat)attribsSheba[0]).Text : new string[] { },

                            };
                            result.Add(item);
                        }

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return new EnumBankCollection();
            }
        }
        public static string[] GetEnumCardFormat(System.Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo.Length != 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumCardFormat), false);
                if (attrs.Length != 0)
                {
                    return ((EnumCardFormat)attrs[0]).Text;
                }
            }
            return new[] { en.ToString() };
        }
        public static string[] GetEnumShebaFormat(System.Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo.Length != 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumShebaFormat), false);
                if (attrs.Length != 0)
                {
                    return ((EnumShebaFormat)attrs[0]).Text;
                }
            }
            return new[] { en.ToString() };
        }
    }
}
