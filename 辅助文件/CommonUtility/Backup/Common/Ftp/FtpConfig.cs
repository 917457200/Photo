using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace iFlyTek.Common.Ftp
{
    public class FtpConfig
    {
        //// Զ��FTP�������
        //private string _strServer = "";
        //private string _strUserName = "";
        //private string _strPassword = "";
        //private string _strPort = "21";

        //// Զ��FTP�ļ����·��
        //private string _strRemoteFilePath = "";
        //private string _strRemoteAbsFile = "";
        //private string _strRemoteSuccessPath = ".";

        //private string _strAreaName = "";

        private string _configType = "";

        /// <summary>
        /// ����һ��ftp����������
        /// </summary>
        /// <param name="type">�������͡���ȡ������Ϣʱ���á�</param>
        public FtpConfig(string type)
        {
            this._configType = type;
        }

        #region ����

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

        // ����FTP�ļ����·��
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
