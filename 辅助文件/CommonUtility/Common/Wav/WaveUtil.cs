using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iFlyTek.Common;
using iFlyTek.Common.Wav;

namespace iFlyTek.Common.Wav
{
    public class WaveUtil
    {

        #region wav

        /// <summary>
        /// 获取wav文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static WaveInfo GetWavInfo(string path)
        {
            try
            {
                //获取播放时间
                WaveInfo wavinfo = WaveFile.Open(path);
                int result = Convert.ToInt32(wavinfo.PlayTime);
                return wavinfo;
            }
            catch
            {
                return null;
            }
        }

        #endregion

    }
}
