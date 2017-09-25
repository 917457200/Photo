/*+---------------------------------------------------------------------------+
* 文件名称：WaveHead.cs
* 文件功能：定义wave文件riff块、format块、Fact块、data块的数据模型
* 文件作者：刘金友
* 创建时间：2010.6.16   11:10
* 项目名称：my project
 
* 历史记录：
* 编号 日期      作者    备注
* 1.0  2010.6.16 jyliu  创建
* 1.0  2010.6.16 jyliu  实现定义
*+--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
namespace iFlyTek.Common.Wav
{


    /// <summary>
    /// riff块
    /// </summary>
    public class RIFFChunk
    {
        /// <summary>
        /// wave标示
        /// </summary>
        public readonly byte[] RiffTag = new byte[] { 0x52, 0x49, 0x46, 0x46 };   // 'R','I','F','F' 
        /// <summary>
        /// wave标示
        /// </summary>
        public readonly byte[] WaveTag = new byte[] { 0x57, 0x41, 0x56, 0x45 }; // 'W','A','V','E' 
        /// <summary> 
        /// 文件前四个字节 为RIFF 
        /// </summary> 
        public byte[] RiffID = new byte[4];
        /// <summary> 
        /// 数据大小 文件大小=这个数字等于+8 
        /// </summary> 
        public uint Size = 0;
        /// <summary> 
        ///WAVE文件定义 为WAVE 
        /// </summary> 
        public byte[] WaveID = new byte[4];
    };

    /// <summary>
    /// format块
    /// </summary>
    public class FormatChunk
    {
        /// <summary>
        /// format标示
        /// </summary>
        public readonly byte[] FmtTag = new byte[] { 0x66, 0x6D, 0x74, 0x20 };//'f','m','t',' '
        /// <summary> 
        /// 固定为  是"fmt "字后一位为0x20 
        /// 以'fmt '作为标示
        /// </summary> 
        public byte[] ID = new byte[4];
        /// <summary> 
        /// 一般情况下Size为16，此时最后附加信息没有；如果为18
        /// 则最后多了2个字节的附加信息
        /// </summary> 
        public uint Size = 0;
        /// <summary> 
        /// 格式类别（10H为PCM形式的声音数据)
        /// 例如：1-WAVE_FORMAT_PCM， 2-WAVE_F0RAM_ADPCM等等。  
        /// </summary> 
        public ushort FormatType = 1;
        /// <summary> 
        /// 声道数目，1--单声道；2--双声道 
        /// </summary> 
        public ushort ChannelCount = 2;
        /// <summary> 
        /// 采样率（每秒样本数），表示每个通道的播放速度，
        /// 采样频率  一般有11025Hz（11kHz）、22050Hz（22kHz）和44100Hz（44kHz）三种 
        /// </summary> 
        public uint SamlesPerSec = 0;
        /// <summary> 
        /// 波形音频数据传送速率，其值为通道数*每秒数据位数*每样
        /// 本的数据位数／8。播放软件利用此值可以估计缓冲区的大小。 
        /// </summary> 
        public uint AvgBytesPerSec = 0;
        /// <summary> 
        /// 数据块的调整数（按字节算的），其值为通道数*每样本的数据位
        /// 值／8。播放软件需要一次处理多个该值大小的字节数据，以便将其
        /// 值用于缓冲区的调整。
        /// </summary> 
        public ushort BlockAlign = 0;
        /// <summary> 
        ///  每样本的数据位数，表示每个声道中各个样本的数据位数。如果有多
        /// 个声道，对每个声道而言，样本大小都一样。  
        /// </summary> 
        public ushort BitsPerSample = 0;
        /// <summary> 
        /// 保留 2
        /// </summary> 
        public byte[] Temp = new byte[2];
    };

    /// <summary>
    /// Fact块
    /// </summary>
    public class FactChunk
    {
        /// <summary>
        /// Fact 标示
        /// </summary>
        public readonly byte[] FactTag = new byte[] { 0x66, 0x61, 0x63, 0x74 };   // 'f','a','c','t' 
        /// <summary> 
        /// 文件前四个字节 为fact 
        /// </summary> 
        public byte[] ID = new byte[4];
        /// <summary> 
        /// 数据大小
        /// </summary> 
        public uint Size = 0;
        /// <summary> 
        /// 临时数据 4
        /// </summary> 
        public byte[] Temp = new byte[4];
    };

    /// <summary>
    /// data块
    /// </summary>
    public class DataChunk
    {
        public readonly byte[] DataTag = new byte[] { 0x64, 0x61, 0x74, 0x61 };   // 'd','a','t','a' 
        /// <summary> 
        /// 文件前四个字节 为RIFF 
        /// </summary> 
        public byte[] ID = new byte[4];
        /// <summary> 
        /// 大小 
        /// </summary> 
        public uint Size = 0;
        /// <summary> 
        /// 开始播放的位置 
        /// </summary> 
        public long FileBeginIndex = 0;
        /// <summary> 
        /// 结束播放的位置 
        /// </summary> 
        public long FileOverIndex = 0;
    };

}