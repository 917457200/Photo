using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utility
{
    public class RandomUtil
    {

        #region Random

        public static Random rad = new Random();

        /// <summary>
        /// 获取非负随机整数
        /// </summary>
        /// <returns></returns>
        public static int GetRandomInt()
        {
            return rad.Next();
        }

        /// <summary>
        /// 获取小于指定数的非负随机数
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomInt(int max)
        {
            return rad.Next(max);
        }
        #endregion
    }
}
