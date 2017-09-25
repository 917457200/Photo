using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iFlyTek.Common.Utility
{
    public class PathUtil
    {
        /// <summary>
        /// 删除多余分隔符
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static string DeleteSeparator(string path)
        {
            //1.获得目录名称
            List<string> remoteDirNames = GetDirectoryName(path);

            //2.拼接目录名
            StringBuilder combinePath = new StringBuilder();
            foreach (string dirName in remoteDirNames)
            {
                combinePath.Append("/");
                combinePath.Append(dirName);
            }
            return combinePath.ToString();
        }

        /// <summary>
        /// 获得目录名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetDirectoryName(string path)
        {
            //1.获得目录名称
            List<string> remoteDirNames = new List<string>();
            int j = 0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != '/' && path[i] != '\\')
                {
                    continue;
                }
                if ((i + 1) == j) //过滤// \\这种情况
                {
                    j = i + 1;
                    continue;
                }
                string temp = path.Substring(j, i - j).Trim();
                if(!string.IsNullOrEmpty(temp)&&temp.Length > 0)
                {
                    remoteDirNames.Add(temp);
                }
                j = i + 1;
            }
            if (j < path.Length)
            {
                string temp = path.Substring(j, path.Length - j).Trim();
                if (!string.IsNullOrEmpty(temp) && temp.Length > 0)
                {
                    remoteDirNames.Add(temp);
                }
            }
            return remoteDirNames;
        }
    }
}
