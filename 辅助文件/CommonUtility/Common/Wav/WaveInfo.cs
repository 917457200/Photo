/*+---------------------------------------------------------------------------+
* 文件名称：WaveInfo.cs
* 文件功能：定义wave文件信息
* 文件作者：刘金友
* 创建时间：2010.6.16   11:10
* 项目名称：my project
 
* 历史记录：
* 编号 日期      作者    备注
* 1.0  2010.6.16 jyliu  创建
* 1.0  2010.6.16 jyliu  实现定义
*+--------------------------------------------------------------------------*/

using System;
namespace iFlyTek.Common.Wav
{


    public class WaveInfo
    {
        private string fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private double playTime;
        /// <summary>
        /// 播放时间 单位：s (秒)
        /// </summary>
        public double PlayTime
        {
            get { return playTime; }
            set { playTime = value; }
        }

        private uint size;
        /// <summary>
        /// 文件大小 单位：Byte(字节)
        /// </summary>
        public uint Size
        {
            get { return size; }
            set { size = value; }
        }
        /// <summary>
        /// wave 文件Rifff
        /// </summary>
        public RIFFChunk RiffBlock = new RIFFChunk();
        /// <summary>
        /// format 块
        /// </summary>
        public FormatChunk FormatBlock = new FormatChunk();
        /// <summary>
        /// fact 块
        /// </summary>
        public FactChunk FackBlock = new FactChunk();
        /// <summary>
        /// data 块
        /// </summary>
        public DataChunk DataBlock = new DataChunk();
        /// <summary>
        /// wave文件数据区
        /// </summary>
        public byte[] Data;

        public WaveInfo Clone()
        {
            WaveInfo waveInfo = new WaveInfo();
            waveInfo.FileName = this.FileName;
            waveInfo.PlayTime = this.PlayTime;
            waveInfo.Size = this.Size;
            //riff块
            this.RiffBlock.RiffID.CopyTo(waveInfo.RiffBlock.RiffID, 0);
            waveInfo.RiffBlock.Size = this.RiffBlock.Size;
            this.RiffBlock.WaveID.CopyTo(waveInfo.RiffBlock.WaveID, 0);

            //format块
            this.FormatBlock.ID.CopyTo(waveInfo.FormatBlock.ID, 0);
            waveInfo.FormatBlock.Size = this.FormatBlock.Size;
            waveInfo.FormatBlock.AvgBytesPerSec = this.FormatBlock.AvgBytesPerSec;
            waveInfo.FormatBlock.BitsPerSample = this.FormatBlock.BitsPerSample;
            waveInfo.FormatBlock.BlockAlign = this.FormatBlock.BlockAlign;
            waveInfo.FormatBlock.ChannelCount = this.FormatBlock.ChannelCount;
            waveInfo.FormatBlock.FormatType = this.FormatBlock.FormatType;
            waveInfo.FormatBlock.SamlesPerSec = this.FormatBlock.SamlesPerSec;
            this.FormatBlock.Temp.CopyTo(waveInfo.FormatBlock.Temp, 0);

            //fact块
            this.FackBlock.ID.CopyTo(waveInfo.FackBlock.ID, 0);
            waveInfo.FackBlock.Size = this.FackBlock.Size;
            this.FackBlock.Temp.CopyTo(waveInfo.FackBlock.Temp, 0);

            //data块
            this.DataBlock.ID.CopyTo(waveInfo.DataBlock.ID, 0);
            waveInfo.DataBlock.Size = this.DataBlock.Size;
            waveInfo.DataBlock.FileBeginIndex = this.DataBlock.FileBeginIndex;
            waveInfo.DataBlock.FileOverIndex = this.DataBlock.FileOverIndex;
            waveInfo.Data = new byte[this.Data.Length];
            this.Data.CopyTo(waveInfo.Data, 0);
            return waveInfo;
        }
    }

}