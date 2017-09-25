/*+---------------------------------------------------------------------------+
* 文件名称：WaveFile.cs
* 文件功能：wave文件处理，功能包括：打开、创建、追加格式相同的wave文件
* 文件作者：刘金友
* 创建时间：2010.6.16   11:10
* 项目名称：my project
 
* 历史记录：
* 编号 日期      作者    备注
* 1.0  2010.6.16 jyliu  创建
* 1.0  2010.6.16 jyliu  实现 打开文件功能、创建文件、追加文件
*+--------------------------------------------------------------------------*/

using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace iFlyTek.Common.Wav
{

    public class WaveFile
    {
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static WaveInfo Open(string fileName)
        {
            WaveInfo waveInfo = new WaveInfo();
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                #region 1.riff块
                //获得文件头中riff
                fileStream.Read(waveInfo.RiffBlock.RiffID, 0, 4);
                if (waveInfo.RiffBlock.RiffID[0] != waveInfo.RiffBlock.RiffTag[0]
                    || waveInfo.RiffBlock.RiffID[1] != waveInfo.RiffBlock.RiffTag[1]
                    || waveInfo.RiffBlock.RiffID[2] != waveInfo.RiffBlock.RiffTag[2]
                    || waveInfo.RiffBlock.RiffID[3] != waveInfo.RiffBlock.RiffTag[3])
                {
                    return null;
                }
                byte[] tempBytes = new byte[4];
                //数据大小
                fileStream.Read(tempBytes, 0, 4);
                waveInfo.RiffBlock.Size = BitConverter.ToUInt32(tempBytes, 0);
                //文件大小
                waveInfo.Size = waveInfo.RiffBlock.Size + 8;
                //wave标示
                fileStream.Read(waveInfo.RiffBlock.WaveID, 0, 4);
                if (waveInfo.RiffBlock.WaveID[0] != waveInfo.RiffBlock.WaveTag[0]
                    || waveInfo.RiffBlock.WaveID[1] != waveInfo.RiffBlock.WaveTag[1]
                    || waveInfo.RiffBlock.WaveID[2] != waveInfo.RiffBlock.WaveTag[2]
                    || waveInfo.RiffBlock.WaveID[3] != waveInfo.RiffBlock.WaveTag[3])
                {
                    return null;
                }
                #endregion

                #region 2.format块
                //fmt标示
                fileStream.Read(waveInfo.FormatBlock.ID, 0, 4);
                if (waveInfo.FormatBlock.ID[0] != waveInfo.FormatBlock.FmtTag[0]
                    || waveInfo.FormatBlock.ID[1] != waveInfo.FormatBlock.FmtTag[1]
                    || waveInfo.FormatBlock.ID[2] != waveInfo.FormatBlock.FmtTag[2]
                    || waveInfo.FormatBlock.ID[3] != waveInfo.FormatBlock.FmtTag[3])
                {
                    return null;
                }
                //format大小
                fileStream.Read(tempBytes, 0, 4);
                waveInfo.FormatBlock.Size = BitConverter.ToUInt32(tempBytes, 0);
                //格式类别
                fileStream.Read(tempBytes, 0, 2);
                waveInfo.FormatBlock.FormatType = BitConverter.ToUInt16(tempBytes, 0);
                //声道数目
                fileStream.Read(tempBytes, 0, 2);
                waveInfo.FormatBlock.ChannelCount = BitConverter.ToUInt16(tempBytes, 0);
                //采样率
                fileStream.Read(tempBytes, 0, 4);
                waveInfo.FormatBlock.SamlesPerSec = BitConverter.ToUInt32(tempBytes, 0);
                //波形音频数据传送速率
                fileStream.Read(tempBytes, 0, 4);
                waveInfo.FormatBlock.AvgBytesPerSec = BitConverter.ToUInt32(tempBytes, 0);
                waveInfo.PlayTime = (double)waveInfo.RiffBlock.Size / (double)waveInfo.FormatBlock.AvgBytesPerSec;
                //数据块的调整数
                fileStream.Read(tempBytes, 0, 2);
                waveInfo.FormatBlock.BlockAlign = BitConverter.ToUInt16(tempBytes, 0);
                //每样本的数据位数
                fileStream.Read(tempBytes, 0, 2);
                waveInfo.FormatBlock.BitsPerSample = BitConverter.ToUInt16(tempBytes, 0);

                if (waveInfo.FormatBlock.Size == 18)
                {
                    //保留
                    fileStream.Read(waveInfo.FormatBlock.Temp, 0, 2);
                }
                #endregion

                #region 3.Fact块
                //fact标示
                fileStream.Read(waveInfo.FackBlock.ID, 0, 4);
                if (waveInfo.FackBlock.ID[0] == waveInfo.FackBlock.FactTag[0]
                    && waveInfo.FackBlock.ID[1] == waveInfo.FackBlock.FactTag[1]
                    && waveInfo.FackBlock.ID[2] == waveInfo.FackBlock.FactTag[2]
                    && waveInfo.FackBlock.ID[3] == waveInfo.FackBlock.FactTag[3])
                {
                    //大小
                    fileStream.Read(tempBytes, 0, 4);
                    waveInfo.FackBlock.Size = BitConverter.ToUInt32(tempBytes, 0);
                }
                #endregion

                #region 4.data块
                waveInfo.DataBlock.ID[0] = waveInfo.FackBlock.ID[0];
                waveInfo.DataBlock.ID[1] = waveInfo.FackBlock.ID[1];
                waveInfo.DataBlock.ID[2] = waveInfo.FackBlock.ID[2];
                waveInfo.DataBlock.ID[3] = waveInfo.FackBlock.ID[3];
                if (waveInfo.DataBlock.ID[0] == waveInfo.DataBlock.DataTag[0]
                    && waveInfo.DataBlock.ID[1] == waveInfo.DataBlock.DataTag[1]
                    && waveInfo.DataBlock.ID[2] == waveInfo.DataBlock.DataTag[2]
                    && waveInfo.DataBlock.ID[3] == waveInfo.DataBlock.DataTag[3])
                {
                    //大小
                    fileStream.Read(tempBytes, 0, 4);
                    waveInfo.DataBlock.Size = BitConverter.ToUInt32(tempBytes, 0);
                    waveInfo.DataBlock.FileBeginIndex = fileStream.Position;
                    waveInfo.DataBlock.FileOverIndex = fileStream.Position + waveInfo.DataBlock.Size;
                }
                //wave文件数据
                long dataSize = waveInfo.Size - fileStream.Position;
                waveInfo.Data = new byte[(int)dataSize];
                fileStream.Read(waveInfo.Data, 0, (int)dataSize);
                #endregion

                return waveInfo;
            }
        }


        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="waveInfo"></param>
        /// <returns></returns>
        public static bool Create(WaveInfo waveInfo, ref string errorInfo)
        {
            using (FileStream fileStream = new FileStream(waveInfo.FileName, FileMode.Create))
            {
                #region 1.riff块
                //获得文件头中riff                
                if (waveInfo.RiffBlock.RiffID[0] != waveInfo.RiffBlock.RiffTag[0]
                    || waveInfo.RiffBlock.RiffID[1] != waveInfo.RiffBlock.RiffTag[1]
                    || waveInfo.RiffBlock.RiffID[2] != waveInfo.RiffBlock.RiffTag[2]
                    || waveInfo.RiffBlock.RiffID[3] != waveInfo.RiffBlock.RiffTag[3])
                {
                    errorInfo = "文件头Riff块格式不正确";
                    return false;
                }
                fileStream.Write(waveInfo.RiffBlock.RiffID, 0, 4);

                //数据大小
                byte[] tempBytes;
                tempBytes = BitConverter.GetBytes(waveInfo.RiffBlock.Size);
                fileStream.Write(tempBytes, 0, 4);

                //wave标示                
                if (waveInfo.RiffBlock.WaveID[0] != waveInfo.RiffBlock.WaveTag[0]
                    || waveInfo.RiffBlock.WaveID[1] != waveInfo.RiffBlock.WaveTag[1]
                    || waveInfo.RiffBlock.WaveID[2] != waveInfo.RiffBlock.WaveTag[2]
                    || waveInfo.RiffBlock.WaveID[3] != waveInfo.RiffBlock.WaveTag[3])
                {
                    errorInfo = "文件头Riff块格式不正确";
                    return false;
                }
                fileStream.Write(waveInfo.RiffBlock.WaveID, 0, 4);
                #endregion

                #region 2.format块
                //fmt标示
                if (waveInfo.FormatBlock.ID[0] != waveInfo.FormatBlock.FmtTag[0]
                    || waveInfo.FormatBlock.ID[1] != waveInfo.FormatBlock.FmtTag[1]
                    || waveInfo.FormatBlock.ID[2] != waveInfo.FormatBlock.FmtTag[2])
                {
                    errorInfo = "文件头format块格式不正确";
                    return false;
                }
                fileStream.Write(waveInfo.FormatBlock.ID, 0, 4);

                //format大小
                tempBytes = BitConverter.GetBytes(waveInfo.FormatBlock.Size);
                fileStream.Write(tempBytes, 0, 4);
                //格式类别
                tempBytes = BitConverter.GetBytes(waveInfo.FormatBlock.FormatType);
                fileStream.Write(tempBytes, 0, 2);
                //声道数目
                tempBytes = BitConverter.GetBytes(waveInfo.FormatBlock.ChannelCount);
                fileStream.Write(tempBytes, 0, 2);
                //采样率
                tempBytes = BitConverter.GetBytes(waveInfo.FormatBlock.SamlesPerSec);
                fileStream.Write(tempBytes, 0, 4);
                //波形音频数据传送速率
                tempBytes = BitConverter.GetBytes(waveInfo.FormatBlock.AvgBytesPerSec);
                fileStream.Write(tempBytes, 0, 4);
                //数据块的调整数
                tempBytes = BitConverter.GetBytes(waveInfo.FormatBlock.BlockAlign);
                fileStream.Write(tempBytes, 0, 2);
                //每样本的数据位数
                tempBytes = BitConverter.GetBytes(waveInfo.FormatBlock.BitsPerSample);
                fileStream.Write(tempBytes, 0, 2);

                if (waveInfo.FormatBlock.Size == 18)
                {
                    //保留
                    fileStream.Write(waveInfo.FormatBlock.Temp, 0, 2);
                }
                #endregion

                #region 3.Fact块
                //fact标示                
                if (waveInfo.FackBlock.ID[0] == waveInfo.FackBlock.FactTag[0]
                    && waveInfo.FackBlock.ID[1] == waveInfo.FackBlock.FactTag[1]
                    && waveInfo.FackBlock.ID[2] == waveInfo.FackBlock.FactTag[2]
                    && waveInfo.FackBlock.ID[3] == waveInfo.FackBlock.FactTag[3])
                {
                    fileStream.Write(waveInfo.FackBlock.ID, 0, 4);
                    //大小
                    tempBytes = BitConverter.GetBytes(waveInfo.FackBlock.Size);
                    fileStream.Write(tempBytes, 0, 4);
                }
                #endregion

                #region 4.data块
                if (waveInfo.DataBlock.ID[0] == waveInfo.DataBlock.DataTag[0]
                    && waveInfo.DataBlock.ID[1] == waveInfo.DataBlock.DataTag[1]
                    && waveInfo.DataBlock.ID[2] == waveInfo.DataBlock.DataTag[2]
                    && waveInfo.DataBlock.ID[3] == waveInfo.DataBlock.DataTag[3])
                {
                    fileStream.Write(waveInfo.DataBlock.ID, 0, 4);
                    //大小
                    tempBytes = BitConverter.GetBytes(waveInfo.DataBlock.Size);
                    fileStream.Write(tempBytes, 0, 4);
                }
                //wave文件数据
                fileStream.Write(waveInfo.Data, 0, waveInfo.Data.Length);
                #endregion
            }
            return true;
        }

        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="destWaveInfo">目标文件</param>
        /// <param name="sourceWaveInfo">源文件</param>
        /// <param name="errorInfo">返回出错信息</param>
        /// <returns></returns>
        public static bool Append(WaveInfo destWaveInfo, WaveInfo sourceWaveInfo, ref string errorInfo)
        {
            //1.判断文件格式是否正确
            if (destWaveInfo.FormatBlock.FormatType != sourceWaveInfo.FormatBlock.FormatType
                || destWaveInfo.FormatBlock.ChannelCount != sourceWaveInfo.FormatBlock.ChannelCount
                || destWaveInfo.FormatBlock.SamlesPerSec != sourceWaveInfo.FormatBlock.SamlesPerSec)
            {
                errorInfo = "源文件与目标文件格式不正确";
                return false;
            }

            //2.根据文件数据块的类型，将源数据追加到目的数据
            if (sourceWaveInfo.FackBlock.ID[0] == sourceWaveInfo.FackBlock.FactTag[0]
                && sourceWaveInfo.FackBlock.ID[1] == sourceWaveInfo.FackBlock.FactTag[1]
                && sourceWaveInfo.FackBlock.ID[2] == sourceWaveInfo.FackBlock.FactTag[2]
                && sourceWaveInfo.FackBlock.ID[3] == sourceWaveInfo.FackBlock.FactTag[3])
            {
                destWaveInfo.RiffBlock.Size += sourceWaveInfo.FackBlock.Size;
                destWaveInfo.FackBlock.Size += sourceWaveInfo.FackBlock.Size;
            }

            if (sourceWaveInfo.DataBlock.ID[0] == sourceWaveInfo.DataBlock.DataTag[0]
                && sourceWaveInfo.DataBlock.ID[1] == sourceWaveInfo.DataBlock.DataTag[1]
                && sourceWaveInfo.DataBlock.ID[2] == sourceWaveInfo.DataBlock.DataTag[2]
                && sourceWaveInfo.DataBlock.ID[3] == sourceWaveInfo.DataBlock.DataTag[3])
            {
                destWaveInfo.RiffBlock.Size += sourceWaveInfo.DataBlock.Size;
                destWaveInfo.DataBlock.Size += sourceWaveInfo.DataBlock.Size;
            }
            byte[] tatolData = new byte[destWaveInfo.Data.Length + sourceWaveInfo.Data.Length];
            destWaveInfo.Data.CopyTo(tatolData, 0);
            sourceWaveInfo.Data.CopyTo(tatolData, destWaveInfo.Data.Length);
            destWaveInfo.Data = tatolData;

            //3.创建追加后的文件
            return true;
        }

        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="destWaveFileName"></param>
        /// <param name="sourceWaveFileName"></param>
        /// <returns></returns>
        public static bool Append(string destWaveFileName, string sourceWaveFileName, ref string errorInfo)
        {
            //1.将文件读入到内存中
            WaveInfo destWaveInfo = Open(destWaveFileName);
            if (destWaveInfo == null)
            {
                errorInfo = destWaveFileName + "文件不能打开";
                return false;
            }
            WaveInfo sourceWaveInfo = Open(sourceWaveFileName);
            if (sourceWaveInfo == null)
            {
                errorInfo = sourceWaveFileName + "文件不能打开";
                return false;
            }
            //2.追加文件
            return Append(destWaveInfo, sourceWaveInfo, ref errorInfo);
        }
    }

}