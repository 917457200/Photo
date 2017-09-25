/*+---------------------------------------------------------------------------+
* 文件名称：FtpHelper.cs
* 文件功能：ftp处理器
* 文件作者：jyliu
* 创建时间：2010.9.29   11:10
* 项目名称：CEP10
 
* 历史记录：
* 编号 日期      作者    备注
* 1.0  2010.9.29 jyliu  创建，并实现上传功能
*+--------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Net;
using System.Configuration;
using System.Collections.Generic;
using iFlyTek.Common.Utility;

namespace iFlyTek.Common.Helper
{
    /// <summary>
    /// FTP信息
    /// </summary>
    [Serializable]
    public class FtpInfo
    {
        private string _serverIP;           //FTP服务器IP
        private string _user;               //用户名
        private string _password;           //密码
        private int _port = 21;             //端口号
        private bool _fireMode = true;     //防火墙模式:false,主动模式,true,被动模式
        private string _remoteDir;         //FTP路径
        private bool _isUseBinary;         //服务器要传输的是二进制数据
        private int _timeOut = 30000;
        private string _localPath;

        public FtpInfo()
        {
            this._serverIP = ConfigurationManager.AppSettings["FtpIP"];
            this._user = ConfigurationManager.AppSettings["FtpUser"];
            this._password = ConfigurationManager.AppSettings["FtpPassword"];
            this._port = int.Parse(ConfigurationManager.AppSettings["FtpPort"]);
            this._remoteDir = ConfigurationManager.AppSettings["FtpSubPath"];

            string localPath = ConfigurationManager.AppSettings["FtpLocalFilePath"];
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            this._localPath = localPath;
        }
        /// <summary>
        /// 服务器要传输的是二进制数据
        /// </summary>
        public bool IsUseBinary
        {
            get { return _isUseBinary; }
            set { _isUseBinary = value; }
        }

        public string ServerIP
        {
            get { return _serverIP; }
            set { _serverIP = value; }
        }
        public string User
        {
            get { return _user; }
            set { _user = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }
        public bool FireMode
        {
            get { return _fireMode; }
            set { _fireMode = value; }
        }
        public string RemoteDir
        {
            get { return _remoteDir; }
            set { _remoteDir = value; }
        }
        public int TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }
        public string LocalPath
        {
            get { return _localPath; }
            set { _localPath = value; }
        }

        public FtpInfo Clone()
        {
            FtpInfo ftpInfo = new FtpInfo();
            ftpInfo.User = User;
            ftpInfo.Password = Password;
            ftpInfo.FireMode = FireMode;
            ftpInfo.IsUseBinary = IsUseBinary;
            ftpInfo.Port = Port;
            ftpInfo.RemoteDir = RemoteDir;
            ftpInfo.ServerIP = ServerIP;
            ftpInfo.TimeOut = TimeOut;
            return ftpInfo;
        }

        public override string ToString()
        {
            return string.Format("ftp://{0}:{1}/{2}"
                    , ServerIP
                    , Port
                    , RemoteDir);
        }
    } ;

    /// <summary>
    /// FTP处理
    /// </summary>
    public partial class FtpHelper
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="ftpInfo"></param>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool Download(FtpInfo ftpInfo, string fileName, string path, ref string error)
        {
            Stream ftpStream = null;
            FtpWebRequest reqFTP;
            FileStream outputStream = null;
            FtpWebResponse response = null;
            string uri = string.Empty;

            try
            {
                uri = string.Format("ftp://{0}:{1}{2}/{3}"
                    , ftpInfo.ServerIP
                    , ftpInfo.Port
                    , PathUtil.DeleteSeparator(ftpInfo.RemoteDir)
                    , fileName);

                outputStream = new FileStream(path, FileMode.Create);
                reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = ftpInfo.IsUseBinary;
                reqFTP.Credentials = new NetworkCredential(ftpInfo.User, ftpInfo.Password);
                //被动模式(将 UsePassive 属性设置为 true 可向服务器发送“PASV”命令。此命令请求服务器侦听数据端口并等待连接，而不是在收到传输命令时启动连接。)
                reqFTP.UsePassive = ftpInfo.FireMode;
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                const int bufferSize = 2048;

                byte[] buffer = new byte[bufferSize];
                int readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                return true;
            }
            catch (Exception ex)
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                error = string.Format("下载文件FTP上文件[{0}]到[{1}]出现异常：{2}", uri, path, ex.Message);
                return false;
            }
            finally
            {
                if (ftpStream != null) ftpStream.Close();
                if (outputStream != null) outputStream.Close();
                if (response != null) response.Close();
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="ftpInfo">上传FTP信息</param>
        /// <param name="fileName">上传文件名</param>
        /// <param name="error">返回出错信息</param>
        /// <returns></returns>
        public static bool Upload(FtpInfo ftpInfo, string fileName, ref string error)
        {
            Stream requestStream = null;
            FtpWebRequest request;
            string uri = string.Empty;
            fileName = ftpInfo.LocalPath + fileName;

            try
            {
                if (string.IsNullOrEmpty(ftpInfo.RemoteDir))
                {
                    uri = string.Format("ftp://{0}:{1}/{2}"
                                , ftpInfo.ServerIP
                                , ftpInfo.Port
                                , Path.GetFileName(fileName)
                                );
                }
                else
                {
                    uri = string.Format("ftp://{0}:{1}{2}/{3}"
                        , ftpInfo.ServerIP
                        , ftpInfo.Port
                        , PathUtil.DeleteSeparator(ftpInfo.RemoteDir)
                        , Path.GetFileName(fileName)
                        );
                }

                request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpInfo.User, ftpInfo.Password);
                request.KeepAlive = false;
                request.UseBinary = ftpInfo.IsUseBinary;
                request.Timeout = ftpInfo.TimeOut;
                request.UsePassive = ftpInfo.FireMode;//被动方式连接到FTP服务器
               
                const int bufferLength = 2048;
                requestStream = request.GetRequestStream();
                // Get the event to wait on.
                byte[] buffer = new byte[bufferLength];
                using (FileStream stream = File.OpenRead(fileName))
                {
                    int count = 0;
                    int readBytes;
                    do
                    {
                        readBytes = stream.Read(buffer, 0, bufferLength);
                        requestStream.Write(buffer, 0, readBytes);
                        count += readBytes;
                    }
                    while (readBytes != 0);
                }
                return true;
            }
            catch (Exception ex)
            {
                error = string.Format("上传文件[{0}]到[{1}]出现异常：{2}", fileName , uri , ex.Message);
                return false;
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="ftpInfo">上传FTP信息</param>
        /// <param name="fileName">上传文件名</param>
        /// <param name="data">文件内容</param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public static bool Upload(FtpInfo ftpInfo, string fileName, byte[] data, ref string errorInfo)
        {
            Stream requestStream = null;
            FtpWebRequest request;
            string uri = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(ftpInfo.RemoteDir))
                {
                    uri = string.Format("ftp://{0}:{1}/{2}"
                                , ftpInfo.ServerIP
                                , ftpInfo.Port
                                , Path.GetFileName(fileName)
                                );
                }
                else
                {
                    uri = string.Format("ftp://{0}:{1}{2}/{3}"
                        , ftpInfo.ServerIP
                        , ftpInfo.Port
                        , PathUtil.DeleteSeparator(ftpInfo.RemoteDir)
                        , Path.GetFileName(fileName)
                        );
                }
                request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpInfo.User, ftpInfo.Password);
                request.KeepAlive = false;
                request.UseBinary = ftpInfo.IsUseBinary;
                request.Timeout = ftpInfo.TimeOut;
                request.UsePassive = ftpInfo.FireMode;//被动方式连接到FTP服务器            

                requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                return true;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                return false;
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
        }

        /// <summary>
        /// 判断remotePath是否是远程FTP目录
        /// </summary>
        /// <param name="ftpInfo">远程FTP信息</param>
        /// <param name="remotePath">目录名</param>
        /// <returns></returns>
        public static bool CheckDirectoryExists(FtpInfo ftpInfo, string remotePath)
        {
            
            try
            {
                //1.删除最后面的目录分隔符
                string path1 = string.Format("{0}/{1}",ftpInfo.RemoteDir, remotePath);
                string uri = string.Format("ftp://{0}:{1}{2}/"
                                          , ftpInfo.ServerIP
                                          , ftpInfo.Port
                                          , PathUtil.DeleteSeparator(path1));

                //2.设置访问FTP参数
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                reqFTP.UseBinary = ftpInfo.IsUseBinary;
                reqFTP.UsePassive = ftpInfo.FireMode;
                reqFTP.Credentials = new NetworkCredential(ftpInfo.User, ftpInfo.Password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                //3.读取FTP信息
                using (WebResponse response = reqFTP.GetResponse())
                {
                    response.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查远程目录是否存在，如果不存在则创建远程FTP目录
        /// </summary>
        /// <param name="ftpInfo">远程FTP信息</param>
        /// <param name="remotePath">目录名</param>
        /// <returns></returns>
        public static bool CheckMakeDirectory(FtpInfo ftpInfo, string remotePath)
        {
            string remoteDir = string.Format("{0}/{1}"
                                          , ftpInfo.RemoteDir
                                          , remotePath);
            try
            {

                //1.获得目录名称
                List<string> remoteDirNames = PathUtil.GetDirectoryName(remoteDir);

                //2.判断远程FTP是否存在目录，如果不存在则创建此目录
                FtpInfo tempFtpInfo = ftpInfo.Clone();
                tempFtpInfo.RemoteDir = "";
                foreach (string dirName in remoteDirNames)
                {
                    //2.1 判断远程FTP是否存在目录
                    tempFtpInfo.RemoteDir = tempFtpInfo.RemoteDir + "/" + dirName;
                    if (CheckDirectoryExists(tempFtpInfo, string.Empty))
                    {
                        continue;
                    }

                    //2.2 如果不存在则创建此目录
                    if (!MakeDirectory(tempFtpInfo, string.Empty))
                    {
                        return false;
                    }                    
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// 判断remotePath是否是远程FTP目录
        /// </summary>
        /// <param name="ftpInfo">远程FTP信息</param>
        /// <param name="remotePath">目录名</param>
        /// <returns></returns>
        public static bool MakeDirectory(FtpInfo ftpInfo, string remotePath)
        {
            try
            {
                //1.删除最后面的目录分隔符
                string path1 = string.Format("{0}/{1}", ftpInfo.RemoteDir, remotePath);
                string uri = string.Format("ftp://{0}:{1}{2}"
                                           , ftpInfo.ServerIP
                                           , ftpInfo.Port
                                           , PathUtil.DeleteSeparator(path1));

                //2.设置访问FTP参数
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uri));
                reqFTP.UseBinary = ftpInfo.IsUseBinary;
                reqFTP.UsePassive = ftpInfo.FireMode;                
                reqFTP.Credentials = new NetworkCredential(ftpInfo.User, ftpInfo.Password);
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.KeepAlive = false;

                //3.读取FTP信息
                using (WebResponse response = reqFTP.GetResponse())
                {
                   response.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
