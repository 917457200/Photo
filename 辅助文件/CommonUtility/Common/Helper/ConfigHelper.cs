using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Configuration;

namespace iFlyTek.Common.Helper
{
    /// <summary>
    ///得到AppSettings中配置的信息
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// 获得Web.config里的配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            return ConfigurationSettings.AppSettings[key];
        }
        /// <summary>
        /// 将配置信息转化为字符串
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static string GetString(string key, string defaultValue)
        {
            string keyValue=ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(keyValue))
            {
                return keyValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将配置信息转化为整型
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static int GetInt(string key, int defaultValue)
        {
            string keyValue = ConfigurationManager.AppSettings[key];
            int tempValue;
            if (int.TryParse(keyValue, out tempValue))
            {
                return tempValue;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将配置信息转化为布尔型
        /// </summary>
        /// <param name="key">AppSettings中的key</param>
        /// <param name="defaultValue">默认返回值</param
        /// <returns>找到与key相应的值，则返回该值，否则返回默认值</returns>
        public static bool GetBool(string key, bool defaultValue)
        {
            string keyValue=ConfigurationManager.AppSettings[key];
            bool tempValue;
            if (bool.TryParse(keyValue,out tempValue))
            {
                return tempValue;
            }
            return defaultValue;
        }
    }
}
