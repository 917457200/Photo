using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace iFlyTek.Common.Ftp
{
    public class FtpConfig
    {
        //// 远程FTP相关配置
        //private string _strServer = "";
        //private string _strUserName = "";
        //private string _strPassword = "";
        //private string _strPort = "21";

        //// 远程FTP文件存放路径
        //private string _strRemoteFilePath = "";
        //private string _strRemoteAbsFile = "";
        //private string _strRemoteSuccessPath = ".";

        //private string _strAreaName = "";

        private string _configType = "";

        /// <summary>
        /// 构造一个ftp传输配置类
        /// </summary>
        /// <param name="type">配置类型【读取配置信息时有用】</param>
        public FtpConfig(string type)
        {
            this._configType = type;
        }

        #region 属性

        public string strRemoteSuccessPath
        {
            get
            {
                return " ";
            }
        }
        public string strAreaName
        {
            get
            {
                return " ";
            }
        }

     
        public string strRemoteFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpSubPath_" + _configType];
            }
        }
        public string strRemoteAbsFile
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpPath_" + _configType];
            }
        }



        public string strServer
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpIP_" + _configType];
            }
        }


        public string strUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUser_" + _configType];
            }
        }


        public string strPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpPassword_" + _configType];
            }
        }

        public string strPort
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpPort_" + _configType];
            }
        }

        // 本地FTP文件存放路径
        public string strLocalFilePath
        {
            get
            {
                string LocalFTPFilePath = ConfigurationManager.AppSettings["FtpLocalFilePath_" + _configType];
                if (!Directory.Exists(LocalFTPFilePath))
                {
                    Directory.CreateDirectory(LocalFTPFilePath);
                }
                return LocalFTPFilePath;
            }
        }

        #endregion
        
    }
}
