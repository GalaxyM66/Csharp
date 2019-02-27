using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

namespace PriceManager
{
    class StringUtils
    {
        public static SortableBindingList<T> TableToEntity<T>(DataTable dt) where T : class, new()
        {
            SortableBindingList<T> list = null;
            Type type = null;
            PropertyInfo[] pArray = null;
            T entity = null;
            try
            {
                list = new SortableBindingList<T>();
                type = typeof(T);
                pArray = type.GetProperties();

                foreach (DataRow row in dt.Rows)
                {
                    entity = Activator.CreateInstance<T>();
                    SetEntity(entity, pArray, row);
                    list.Add(entity);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                type = null;
                pArray = null;
                entity = null;
            }

            return list;
        }

        private static void SetEntity<T>(T entity, PropertyInfo[] pArray, DataRow row) where T : class, new()
        {
            foreach (PropertyInfo property in pArray)
            {
                if (property.CanWrite == false) { continue; }
                if ("System.String".Equals(property.PropertyType.FullName))
                {
                    if (row.Table.Columns.Contains(property.Name))
                    {
                        if (row[property.Name] is DBNull)
                        {
                            property.SetValue(entity, "", null);
                        }
                        else
                        {
                            property.SetValue(entity, row[property.Name].ToString(), null);
                        }
                    }
                    else
                    {
                        property.SetValue(entity, "", null);
                    }
                }
                //else
                //{
                //    object obj = Assembly.GetExecutingAssembly().CreateInstance(property.PropertyType.FullName, true);
                //    SetEntity(obj, property.PropertyType.GetProperties(), row);
                //    property.SetValue(entity, obj, null);
                //}
            }
        }

        public static string ToTimeStamp(string date)
        {
            DateTime time = Convert.ToDateTime(date);
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (time - startTime).TotalSeconds.ToString();
        }

        public static bool IsNumberic(string value)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsNull(Object obj)
        {
            if (obj == null)
            {
                return true;
            }
            else
            {
                if ("".Equals(obj.ToString().Trim()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsNotNull(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                if ("".Equals(obj.ToString().Trim()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static Decimal ToDecimal(Object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                if ("".Equals(obj.ToString()))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(obj.ToString());
                }
            }
        }
        // 2018-11-5---获取下拉框的对应的Key值 author:yangying
        public static string GetKey(string value,Dictionary<string, string> Dic)
        {
            string key = "";
            foreach (string keys in Dic.Keys)
            {
                if (Dic[keys].Equals(value))
                {
                    key = keys;
                    break;
                }
            }
            return key;
        }

        // 2018-11-5---获取下拉框的Value值集合 author:yangying
        public static List<string> GetValue(Dictionary<string, string> Dic)
        {
            List<string> list = new List<string>();
            list.Clear();
            foreach (var item in Dic.Values)
            {
                list.Add(item);
            }
            return list;
        }

    }
}
