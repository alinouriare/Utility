using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fhs.Bulletin.E_Utility.EnumUtility.Attribute;

namespace Fhs.Bulletin.E_Utility.EnumUtility
{
    public static class EnumUtilities
    {
        public static EnumCollection List(Type type)
        {
            EnumCollection result = new EnumCollection();

            if (type.IsEnum)
            {
                Array enValues = Enum.GetValues(type);

                MemberInfo[] mis = type.GetMembers();
                foreach (MemberInfo mi in mis)
                {
                    object[] attribs = mi.GetCustomAttributes(typeof(EnumDescription), false);
                    if (attribs != null && attribs.Length != 0)
                    {
                        EnumItem item = new EnumItem();
                        item.Value = (int) Enum.Parse(type, mi.Name);
                        item.Description = ((EnumDescription) attribs[0]).Text;
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public static string GetEnumDescription(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length != 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescription), false);
                if (attrs != null && attrs.Length != 0)
                {
                    return ((EnumDescription) attrs[0]).Text;
                }
            }
            return en.ToString();
        }
        public static string GetShortName(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length != 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescription), false);
                if (attrs != null && attrs.Length != 0)
                    return ((EnumDescription)attrs[0]).ShortName;
                
            }
            return en.ToString();
        }
        public static EnumDescription GetEnumDescriptionModel(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length != 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescription), false);
                if (attrs != null && attrs.Length != 0)
                {
                    return ((EnumDescription) attrs[0]);
                }
            }
            return null;
        }

        public static T ConvertStringToEnum<T>(string inputStr, T defaultValue)
        {
            try
            {
                var type = typeof(T);

                if (!type.IsEnum ||
                    String.IsNullOrEmpty(inputStr) ||
                    String.IsNullOrWhiteSpace(inputStr))
                    return defaultValue;

                var valueList = Enum.GetValues(type);
                var nameList = Enum.GetNames(type);

                for (int i = 0; i < nameList.Length; i++)
                {
                    var name = nameList[i];
                    var value = valueList.GetValue(i);
                    if (inputStr.ToLower() == name.ToLower())
                        return (T) Enum.Parse(type, name);
                }

                return defaultValue;
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }
        public static T ConvertShortNameToEnum<T>(string inputStr, T defaultValue)
        {
            try
            {
                var type = typeof(T);



                if (!type.IsEnum ||
                    String.IsNullOrEmpty(inputStr) ||
                    String.IsNullOrWhiteSpace(inputStr))
                    return defaultValue;

                var valueList = Enum.GetValues(type);
                var nameList = Enum.GetNames(type);

                foreach (T item in valueList)
                {
                    Type currentType = item.GetType();

                    MemberInfo[] memInfo = currentType.GetMember(item.ToString());

                    if (memInfo != null && memInfo.Length != 0)
                    {
                        object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescription), false);
                        if (attrs != null && attrs.Length != 0 &&
                            ((EnumDescription)attrs[0]).ShortName.ToLower() == inputStr.ToLower())
                            return item;
                    }


                }

                return defaultValue;
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }
        public static bool TryConvertStringToEnum<T>(string inputStr, T defaultValue, out T currentValue)
        {
            currentValue = defaultValue;
            try
            {
                var type = typeof(T);

                if (!type.IsEnum ||
                    String.IsNullOrEmpty(inputStr) ||
                    String.IsNullOrWhiteSpace(inputStr))
                    return false;

                var valueList = Enum.GetValues(type);
                var nameList = Enum.GetNames(type);

                for (int i = 0; i < nameList.Length; i++)
                {
                    var name = nameList[i];
                    var value = valueList.GetValue(i);
                    if (inputStr.ToLower() == name.ToLower())
                    {
                        currentValue = (T)Enum.Parse(type, name);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool TryConvertStringToEnum<T>(string inputStr, out T currentValue)
        {
            currentValue = default(T);
            try
            {
                var type = typeof(T);

                if (!type.IsEnum ||
                    String.IsNullOrEmpty(inputStr) ||
                    String.IsNullOrWhiteSpace(inputStr))
                    return false;

                var valueList = Enum.GetValues(type);
                var nameList = Enum.GetNames(type);

                for (int i = 0; i < nameList.Length; i++)
                {
                    var name = nameList[i];
                    var value = valueList.GetValue(i);
                    if (inputStr.ToLower() == name.ToLower())
                    {
                        currentValue = (T)Enum.Parse(type, name);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static EnumGroupAttribute GetEnumGroupAttribute(Object data)
        {
            try
            {
                Type type = data.GetType();

                if (!type.IsEnum) return null;

                MemberInfo[] memInfo = type.GetMember(data.ToString());

                if (memInfo != null && memInfo.Length != 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(EnumGroupAttribute), false);
                    if (attrs != null && attrs.Length != 0)
                    {
                        return ((EnumGroupAttribute) attrs[0]);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static T GetEnumGroup<T>(Enum data, T defaultValue)
        {
            try
            {
                return ConvertStringToEnum(GetGroupName(data), defaultValue);
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }

        public static List<T> GetEnumListByGroup<T>(Object group)
        {
            try
            {
                var type = typeof(T);
                var groupAttribute = new EnumGroupAttribute(group);

                if (!type.IsEnum || !group.GetType().IsEnum) return new List<T>();

                var enumValues = Enum.GetValues(type);
                var result = new List<T>();

                foreach (T valueT in enumValues)
                {
                    var parentGroup = GetEnumGroupAttribute(valueT);

                    if (parentGroup == null) continue;

                    if (parentGroup.GroupCode == groupAttribute.GroupCode)
                        result.Add(valueT);

                }

                return result;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public static int GetGroupCode(Enum data, int defaultValue = -1000)
        {
            try
            {
                Type type = data.GetType();
                MemberInfo[] memInfo = type.GetMember(data.ToString());

                if (memInfo != null && memInfo.Length != 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(EnumGroupAttribute), false);
                    if (attrs != null && attrs.Length != 0)
                    {
                        return ((EnumGroupAttribute) attrs[0]).GroupCode;
                    }
                }
                return defaultValue;
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }

        public static string GetGroupName(Enum data)
        {
            try
            {
                Type type = data.GetType();
                MemberInfo[] memInfo = type.GetMember(data.ToString());

                if (memInfo != null && memInfo.Length != 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(EnumGroupAttribute), false);
                    if (attrs != null && attrs.Length != 0)
                    {
                        return ((EnumGroupAttribute) attrs[0]).GroupName;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetGroupTitle(Enum data)
        {
            try
            {
                Type type = data.GetType();
                MemberInfo[] memInfo = type.GetMember(data.ToString());

                if (memInfo != null && memInfo.Length != 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(EnumGroupAttribute), false);
                    if (attrs != null && attrs.Length != 0)
                    {
                        return ((EnumGroupAttribute) attrs[0]).GroupTitle;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static EnumPropertyTypeAttribute GetEnumPropertyType(Enum data)
        {
            try
            {
                Type type = data.GetType();

                if (!type.IsEnum) return null;

                MemberInfo[] memInfo = type.GetMember(data.ToString());

                if (memInfo != null && memInfo.Length != 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(EnumPropertyTypeAttribute), false);
                    if (attrs != null && attrs.Length != 0)
                    {
                        return ((EnumPropertyTypeAttribute)attrs[0]);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static Type GetTypeOfEnumOverEnumProperty(Enum data)
        {
            try
            {
                var type = data.GetType();
                var memInfo = type.GetMember(data.ToString());

                if (memInfo != null && memInfo.Length != 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(EnumPropertyTypeAttribute), false);
                    if (attrs != null && attrs.Length != 0)
                    {
                        return ((EnumPropertyTypeAttribute)attrs[0]).PropertyType;
                    }
                }
                return typeof(string);
            }
            catch (Exception ex)
            {
                return typeof(string);
            }
        }
        public static Object GetDefaultValueByEnumProperty(Enum data)
        {
            try
            {
                var type = data.GetType();
                var memInfo = type.GetMember(data.ToString());

                if (memInfo != null && memInfo.Length != 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(EnumPropertyTypeAttribute), false);
                    if (attrs != null && attrs.Length != 0)
                    {
                        return ((EnumPropertyTypeAttribute)attrs[0]).DefaultValue;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static T GetDefaultValueInEnumProperty<T>(Enum data)
        {
            try
            {
                var type = data.GetType();
                var memInfo = type.GetMember(data.ToString());

                if (memInfo != null && memInfo.Length != 0)
                {
                    var attrs = memInfo[0].GetCustomAttributes(typeof(EnumPropertyTypeAttribute), false);
                    if (attrs != null && attrs.Length != 0)
                    {
                        if(((EnumPropertyTypeAttribute)attrs[0]).DefaultValue is T)
                        return (T) ((EnumPropertyTypeAttribute) attrs[0]).DefaultValue;
                    }
                }
                return default(T);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}