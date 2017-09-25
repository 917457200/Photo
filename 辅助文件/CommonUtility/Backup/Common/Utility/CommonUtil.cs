using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Common.Utility
{
    public static class CommonUtil
    {


        #region  数据转换

        /// <summary>
        /// 检查字符是否可以转换成DateTime
        /// </summary>
        /// <param name="text">要转换的字符</param>
        /// <returns></returns>
        public static bool CanToDateTime(this string text)
        {
            try
            {
                DateTime.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 检查字符是否可以转换成int
        /// </summary>
        /// <param name="text">要转换的字符</param>
        /// <returns></returns>
        public static bool CanToInt(this string text)
        {
            try
            {
                int.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 检查字符是否可以转换成long
        /// </summary>
        /// <param name="text">要转换的字符</param>
        /// <returns></returns>
        public static bool CanToLong(this string text)
        {
            try
            {
                long.Parse(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region 通用函数

        /// <summary>
        /// 把秒转化为分钟和秒数
        /// </summary>
        /// <param name="second">要转化的秒数</param>
        /// <param name="mm">返回分钟</param>
        /// <param name="ss">返回秒数</param>
        public static void ConvertSecondsToMinute(int second, out int mm, out int ss)
        {
            mm = second / 60;
            ss = second - mm * 60;
        }


        /// <summary>
        /// 从传入的string数组中获取访问db的字符串，用,隔开
        /// </summary>
        /// <param name="input">传入字符数组</param>
        /// <param name="isString">是否字符串</param>
        /// <returns></returns>
        public static string GetInStringsAccessDB(string[] input, bool isString)
        {
            string output = "";
            if (isString)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    string text = input[i];

                    output += "'" + text + "'";
                    if (i != input.Length - 1)
                    {
                        output += ",";
                    }
                }
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    string text = input[i];

                    output += text;
                    if (i != input.Length - 1)
                    {
                        output += ",";
                    }
                }
            }

            return output;
        }

      
        #endregion

        #region 扩展方法

        #region [string[]]

        /// <summary>
        /// 从传入的string数组中获取访问db的字符串，根据标识判断是否用,隔开
        /// </summary>
        /// <param name="input">传入字符数组</param>
        /// <param name="isString">是否是字符串('号隔开)</param>
        /// <returns></returns>
        public static string GetStringsAccessDB(this string[] input, bool isString)
        {
            string output = "";
            if (isString)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    string text = input[i];

                    output += "'" + text + "'";
                    if (i != input.Length - 1)
                    {
                        output += ",";
                    }
                }
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    string text = input[i];

                    output += text;
                    if (i != input.Length - 1)
                    {
                        output += ",";
                    }
                }
            }

            return output;
        }

        #endregion

        #region List<string>


        /// <summary>
        /// 把list的每个string,用指定的符号括起来，拼接成一个字符串输出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteForm"></param>
        /// <param name="QuoteTo"></param>
        /// <returns></returns>
        public static string ResolveToStringWithQuote(this List<string> list, string QuoteForm, string QuoteTo)
        {
            string result = "";
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    result += string.Format("{0}{1}{2}", QuoteForm, list[i], QuoteTo);
                }

                return result;
            }
            catch
            {
                return result;
            }
        }


        /// <summary>
        /// 把list的每个string,用指定的符号括起来，拼接成一个字符串输出,输出成日期格式
        /// </summary>
        /// <param name="list"></param>
        /// <param name="QuoteForm"></param>
        /// <param name="QuoteTo"></param>
        /// <param name="formatFrom"></param>
        /// <param name="formatTo"></param>
        /// <returns></returns>
        public static string ResolveToDAteStringWithQuote(this List<string> list, string QuoteForm, string QuoteTo, string formatFrom, string formatTo)
        {
            string result = "";
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    result += string.Format("{0}{1}{2}", QuoteForm, list[i].ParseToDateTime(formatFrom).ToString(formatTo), QuoteTo);
                }

                return result;
            }
            catch
            {
                return result;
            }
        }
        #endregion


        #region list<int>

        /// <summary>
        /// 把List的每个元素之间用split符号隔开，返回string
        /// </summary>
        /// <param name="list"></param>
        /// <param name="split">分隔符</param>
        /// <returns>返回string</returns>
        public static string AddSplitString(this List<int> list, string split)
        {
            try
            {
                StringBuilder result = new StringBuilder("");
                for (int i = 0; i < list.Count; i++)
                {
                    int item = list[i];
                    if (i < list.Count - 1)
                    {
                        result.AppendFormat("{0}{1}", item.ToString(), split);
                    }
                    else
                    {
                        result.AppendFormat("{0}", item.ToString());
                    }
                }
                return result.ToString();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region double

        /// <summary>
        /// 把double类型转化为int32,失败返回0
        /// </summary>
        /// <param name="dbl"></param>
        /// <returns></returns>
        public static Int32 ParseToInt32(this double dbl)
        {
            try
            {
                int result = Convert.ToInt32(dbl);
                return result;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region string

        /// <summary>
        /// 尝试转化为HtmlEncode的文本，失败则返回""
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TryHtmlEncode(this string str)
        {
            try
            {
                return HttpUtility.HtmlEncode(str);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 尝试转化为HtmlDecode的文本，失败则返回""
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TryHtmlDecode(this string str)
        {
            try
            {
                return HttpUtility.HtmlDecode(str);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 把string转化为int32，失败则返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int32 ParseToInt32(this string str)
        {
            try
            {
                int result = Int32.Parse(str);
                return result;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取字符串的第一位，返回它的char形式，否则返回'-'
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static char ParseToChar(this string str)
        {
            try
            {
                char result = str.ToCharArray()[0];
                return result;
            }
            catch
            {
                return '-';
            }
        }

        /// <summary>
        /// 转化为datetime,失败返回1801-1-1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="format">格式字符</param>
        /// <returns></returns>
        public static DateTime ParseToDateTime(this string str, string format)
        {
            DateTime result = new DateTime(1801, 1, 1);
            try
            {

                result = DateTime.ParseExact(str, format, null);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        /// <summary>
        /// 转化为datetime,失败返回1801-1-1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ParseToDateTime(this string str)
        {
            DateTime result = new DateTime(1801, 1, 1);
            try
            {

                result = DateTime.Parse(str);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }


        #endregion

        #region DataSet

        /// <summary>
        /// 判断ds中是否有数据（引用不为空，table数大于0，table中rows数大于0）
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool IsDataSetHasData(this DataSet ds)
        {
            try
            {

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将数据集强类型化
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="dataSet">数据源</param>
        /// <param name="tableIndex">需要转换表的索引</param>
        /// <returns>泛型集合</returns>
        public static IList<T> ToList<T>(this DataSet dataSet, int tableIndex)
        {
            //确认参数有效
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
                return null;

            DataTable dt = dataSet.Tables[tableIndex];


            IList<T> list = new List<T>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j].ConvertDataTo(info.PropertyType), null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }




        #endregion

        #region Datetime

        /// <summary>
        /// 将日期转化为string输出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="format">日期格式</param>
        /// <returns></returns>
        public static List<string> ConvertToListString(this List<DateTime> list, string format)
        {
            List<string> result = new List<string>();
            try
            {
                foreach (var item in list)
                {
                    string date = item.ToString(format);
                    result.Add(date);
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        /// <summary>
        /// 获取本日期到目标日期所有的日期(按天)
        /// </summary>
        /// <param name="DateFrom"></param>
        /// <param name="DateTo">目标日期</param>
        public static List<DateTime> GetDateListTo(this DateTime DateFrom, DateTime DateTo)
        {
            List<DateTime> list = new List<DateTime>();
            try
            {
                DateTime from = DateFrom.Date;
                while (from <= DateTo.Date)
                {
                    list.Add(from);
                    from = from.AddDays(1);
                }
                return list;

            }
            catch (Exception)
            {
                return list;
            }
        }

        #endregion
        #endregion

        #region 字符过滤和替换

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterSpecial(this string str)
        {
            if (str == string.Empty) //如果字符串为空，直接返回。
            {
                return str;
            }
            else
            {
                str = str.Replace("'", string.Empty);
                str = str.Replace("<", string.Empty);
                str = str.Replace(">", string.Empty);
                //str = str.Replace("%", string.Empty);
                str = str.Replace("'delete", string.Empty);
                str = str.Replace("''", string.Empty);
                str = str.Replace(",", string.Empty);
                str = str.Replace(".", string.Empty);
                str = str.Replace(">=", string.Empty);
                str = str.Replace("=<", string.Empty);
                str = str.Replace("-", string.Empty);
                str = str.Replace("_", string.Empty);
                str = str.Replace(";", string.Empty);
                str = str.Replace("||", string.Empty);
                str = str.Replace("[", string.Empty);
                str = str.Replace("]", string.Empty);
                str = str.Replace("&", string.Empty);
                str = str.Replace("/", string.Empty);
                str = str.Replace("-", string.Empty);
                str = str.Replace("|", string.Empty);
                str = str.Replace("?", string.Empty);
                str = str.Replace(">?", string.Empty);
                str = str.Replace("?<", string.Empty);
                str = str.Replace(" ", string.Empty);
                return str;
            }
        }

        /// <summary>
        /// 对字符串进行检查和替换其中的特殊字符
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
                        @"<script[^>]*?>.*?</script>",
                        @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([string.Empty'])(\\[string.Empty'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                        @"([\r\n])[\s]+",
                        @"&(quot|#34);",
                        @"&(amp|#38);",
                        @"&(lt|#60);",
                        @"&(gt|#62);", 
                        @"&(nbsp|#160);", 
                        @"&(iexcl|#161);",
                        @"&(cent|#162);",
                        @"&(pound|#163);",
                        @"&(copy|#169);",
                        @"&#(\d+);",
                        @"-->",
                        @"<!--.*\n"
                        };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", string.Empty);
            strOutput.Replace(">", string.Empty);
            strOutput.Replace("\r\n", string.Empty);


            return strOutput;
        }


        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string HtmlEncode(this string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace(" ", "&nbsp;");
            theString = theString.Replace("\'", "'");
            theString = theString.Replace("\n", "<br/> ");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDecode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace("'", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;
        }

        //add by kaicui 2010-12-14 10:25:10 
        public static string CEPHtmlTransQuote(this string str)
        {
            if (str == null)
            {
                return "";
            }
            str = str.Replace("\"", "&quot;");
            str = str.Replace("\\", "/");
            str = str.Replace("'", "\\'");
            str = str.Replace(">", "&gt;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(" ", "&nbsp;");
            return str;
        }

        #endregion

        #region 全半角转化
        /// <summary>全角半角的相互转换
        /// 
        /// </summary>
        public class ConvertDBCAndSBC
        {

            /// <summary>半角转成全角
            /// 半角空格32,全角空格12288
            /// 其他字符半角33~126,其他字符全角65281~65374,相差65248
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public static string DBCToSBC(string input)
            {
                char[] cc = input.ToCharArray();
                for (int i = 0; i < cc.Length; i++)
                {
                    if (cc[i] == 32)
                    {
                        // 表示空格
                        cc[i] = (char)12288;
                        continue;
                    }
                    if (cc[i] < 127 && cc[i] > 32)
                    {
                        cc[i] = (char)(cc[i] + 65248);
                    }
                }
                return new string(cc);
            }

            /// <summary>全角转半角
            /// 半角空格32,全角空格12288
            /// 其他字符半角33~126,其他字符全角65281~65374,相差65248
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public static string SBCToDBC(string input)
            {
                char[] cc = input.ToCharArray();
                for (int i = 0; i < cc.Length; i++)
                {
                    if (cc[i] == 12288)
                    {
                        // 表示空格
                        cc[i] = (char)32;
                        continue;
                    }
                    if (cc[i] > 65280 && cc[i] < 65375)
                    {
                        cc[i] = (char)(cc[i] - 65248);
                    }

                }
                return new string(cc);
            }
        }

        #endregion

        #region 类型转换
       
        public static object ConvertDataTo(this object obj,Type type) 
        {
            if (obj.GetType().Equals(type))
            {
                return obj;
            }
            else
            {
                try
                {
                    if (type == typeof(string)) { return obj.ToString(); }
                    MethodInfo parseMethod = null;
                    foreach (MethodInfo mi in type.GetMethods(BindingFlags.Static | BindingFlags.Public))
                    {
                        if (mi.Name == "Parse" && mi.GetParameters().Length == 1)
                        { parseMethod = mi; break; }
                    }
                    if (parseMethod == null)
                    {
                        return null;
                    }
                    return parseMethod.Invoke(null, new object[] { obj }); 
                }
                catch 
                {
                    return null;
                    
                    throw;
                }
            }
        }

        #endregion
    }
}
