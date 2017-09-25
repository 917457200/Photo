using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Common.Utility.Winform
{
    public class EnviromentUtil
    {

        #region Process

        /// <summary>
        /// 检查进程是否存在
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static bool IsProcessExitst(string Name)
        {
            try
            {
                Process[] psList = Process.GetProcessesByName(Name);
                if (psList.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region enviromentCheck

        /// <summary>
        /// 检查客户机上能否使用WebBrowser控件
        /// </summary>
        /// <returns></returns>
        public static bool IsWebBrowserCanRun()
        {
            try
            {

                WebBrowser wb = new WebBrowser();
                wb.Url = new Uri("http://www.baidu.com");
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
