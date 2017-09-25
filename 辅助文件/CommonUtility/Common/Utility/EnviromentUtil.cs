using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common.Utility
{
    public class EnviromentUtil
    {
        #region font

        private static Dictionary<string, bool> _IsHasFontDic = new Dictionary<string, bool>();

        /// <summary>
        /// 检查机器上是否安装了字体[增加缓存功能]
        /// </summary>
        /// <param name="family">字体名称，比如："宋体"</param>
        public static bool HasFontInstalled(string family)
        {
            if (_IsHasFontDic == null)
            {
                _IsHasFontDic = new Dictionary<string, bool>();
            }

            if (_IsHasFontDic.ContainsKey(family))
            {
                return _IsHasFontDic[family];
            }

            System.Drawing.Text.InstalledFontCollection ifc = new System.Drawing.Text.InstalledFontCollection();

            for (int i = 0; i < ifc.Families.Length; i++)
            {
                FontFamily ff = ifc.Families[i];
                string name = ff.ToString();

                if (name.IndexOf(family) > -1)
                {
                    _IsHasFontDic.Add(family, true);
                    return true;
                }
            }

            _IsHasFontDic.Add(family, false);
            return false;

        }

        #endregion
    }
}
