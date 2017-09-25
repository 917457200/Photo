using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Web;

namespace iFlyTek.Common.Ftp
{
    /// <summary>
    /// Dispose 的摘要说明。
    /// </summary>
    public class FtpClient
    {
        public FtpClient(){}


        /// <summary>
        /// 向FTP上传文件
        /// </summary>
        /// <param name="localPath">本地目录</param>
        /// <param name="fileName">文件名</param>
        /// <param name="errorinfo">报错信息</param>
        /// <param name="type">操作类型</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static bool UploadFile(string localPath, string fileName,ref string errorinfo,string type,ref string filename)
        {
            FtpConfig ftpInfo = new FtpConfig(type);

            FtpLib ftp = new FtpLib();

            localPath = @"" + localPath + fileName;

            string remotePath =  @"" + ftpInfo.strRemoteFilePath + fileName;

            int perc, old_perc = 0;

            if (!File.Exists(localPath))
            {
                errorinfo=string.Format("批开操作类型:{0},无法找到本地文件:{1}",type,localPath);

                return false;
            }

            try
            {
                ftp.Connect(ftpInfo.strServer, ftpInfo.strUserName, ftpInfo.strPassword, int.Parse(ftpInfo.strPort));
                ftp.OpenUpload(localPath, remotePath, false);
                filename = ftpInfo.strRemoteAbsFile + "\\" + fileName;
                while (true)
                {
                    perc = ftp.DoUpload();
                    // No need to report progress everytime we get some bytes
                    // because it causes a flickery effect on the screen in most cases.
                    if (perc > old_perc)
                    {
                        Console.Write("\rFTP Complete: {0}%", perc);
                        Console.Out.Flush();
                    }
                    // is the download done?
                    if (perc == 100)
                    {
                        break;
                    }
                    old_perc = perc;
                }


                return true;
            }
            catch (Exception ex)
            {
                errorinfo=string.Format("批开操作类型:{0},上传文件:{1},出现异常：{2}",type,localPath,ex.Message.ToString());
                
                return false;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        /// <summary>
        /// 上载本地文件到远程目录
        /// </summary>
        /// <param name="strFileName">文件名</param>
        /// <param name="oFTPConfig">配置类</param>
        /// <returns>布尔型</returns>
        public bool UploadSingleLocalFile(string strFileName, FtpConfig oFTPConfig)
        {
            string strLocalFileName = @"" + oFTPConfig.strLocalFilePath + strFileName;
            string strRemoteFileName = @"" + oFTPConfig.strRemoteFilePath + strFileName;

            FtpLib ftp = new FtpLib();

            Console.WriteLine("【上载文件】 {0} 开始上载本地文件到远程目录 {1}", DateTime.Now.ToString(), strFileName);

            int perc, old_perc = 0;

            if (!File.Exists(strLocalFileName))
            {
                return false;
            }

            try
            {
                ftp.Connect(oFTPConfig.strServer, oFTPConfig.strUserName, oFTPConfig.strPassword, int.Parse(oFTPConfig.strPort));

                ftp.OpenUpload(strLocalFileName, strRemoteFileName, false);

                while (true)
                {
                    perc = ftp.DoUpload();

                    // No need to report progress everytime we get some bytes
                    // because it causes a flickery effect on the screen in most cases.
                    if (perc > old_perc)
                    {
                        Console.Write("\r【上载进度】 FTP Complete: {0}%", perc);
                        Console.Out.Flush();
                    }
                    // is the download done?
                    if (perc == 100)
                    {
                        Console.WriteLine(" ");
                        break;
                    }
                    old_perc = perc;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        /// <summary>
        /// 下载远程指定目录下的所有文件
        /// </summary>
        /// <param name="alFielList"></param>
        /// <param name="oFTPConfig"></param>
        /// <returns></returns>
        public bool DownloadAllRemoteFiles(ArrayList alFielList, FtpConfig oFTPConfig)
        {
            if (alFielList.Count < 1) return true;
                
            try
            {
                for (int i = 0; i < alFielList.Count; i++)
                {
                    bool result = DownloadSingleRemoteFile((string)alFielList[i], oFTPConfig);
                    if (!result) return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 下载远程的单个文件
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="oFTPConfig"></param>
        /// <returns></returns>
        public bool DownloadSingleRemoteFile(string strFileName, FtpConfig oFTPConfig)
        {
            string strLocalFileName = @"" + oFTPConfig.strLocalFilePath + strFileName;
            string strRemoteFileName = @"" + oFTPConfig.strRemoteFilePath + strFileName;

            int perc, old_perc = 0;

            FtpLib ftp = new FtpLib();

            Console.WriteLine("【下拉文件】 {0} 开始从远程目录下载文件到本地 {1}", DateTime.Now.ToString(), strFileName);

            try
            {
                ftp.Connect(oFTPConfig.strServer, oFTPConfig.strUserName, oFTPConfig.strPassword, int.Parse(oFTPConfig.strPort));
                ftp.ChangeDir(oFTPConfig.strRemoteFilePath);
                ftp.OpenDownload(strFileName, false);

                while (true)
                {
                    perc = ftp.DoDownload();

                    // No need to report progress everytime we get some bytes
                    // because it causes a flickery effect on the screen in most    cases.
                    if (perc > old_perc)
                    {
                        Console.Write("\r【下拉进度】 Ftp Complete: {0}%", perc);
                        Console.Out.Flush();
                    }

                    // is the download done?
                    if (perc == 100)
                    {
                        Console.WriteLine(" ");
                        File.Copy(@"./" + strFileName, strLocalFileName, true);
                        File.Delete(@"./" + strFileName);
                        break;
                    }

                    old_perc = perc;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        /// <summary>
        /// 获取目录下的文件列表
        /// </summary>
        /// <param name="oFTPConfig"></param>
        /// <param name="alFileList"></param>
        /// <returns></returns>
        public bool GetRemoteFileList(FtpConfig oFTPConfig, ref ArrayList alFileList)
        {
            FtpLib ftp = new FtpLib();
            string strLocalFilePath = @"" + oFTPConfig.strLocalFilePath;
            string strRemoteFilePath = @"" + oFTPConfig.strRemoteFilePath;
            ArrayList myArraylist = new ArrayList();

            try
            {
                ftp.port = int.Parse(oFTPConfig.strPort);
                ftp.Connect(oFTPConfig.strServer, oFTPConfig.strUserName, oFTPConfig.strPassword, int.Parse(oFTPConfig.strPort));
                ftp.ChangeDir(strRemoteFilePath);
                myArraylist = ftp.ListFiles();
                if (myArraylist.Count > 0)
                {
                    alFileList = GetFileName(myArraylist);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        public ArrayList GetFileName(ArrayList alFielList)
        {
            ArrayList myList = new ArrayList();
            for (int i = 0; i < alFielList.Count; i++)
            {
                // 文件完全限定名
                string strFileName = (string)alFielList[i];

                // 匹配字符串
                string characterFirst = "";
                string characterSecond = "";

                // 匹配字符串的索引
                int indexFirst = 0;
                int indexSecond = 0;

                characterFirst = " ";
                indexFirst = strFileName.LastIndexOf(characterFirst);
                if (indexFirst < 0) continue;

                characterSecond = ".n";
                indexSecond = strFileName.LastIndexOf(characterSecond);
                if (indexSecond < 0) continue;

                if (indexFirst > indexSecond) continue;

                // 去掉匹配字符串
                indexFirst = indexFirst + characterFirst.Length;

                // 包括匹配字符串
                indexSecond = indexSecond + characterSecond.Length - indexFirst;

                strFileName =
                    strFileName.Substring(indexFirst, indexSecond);
                myList.Add(strFileName);

                Console.WriteLine("【文件列表】 {0} 在FTP目录发现文件 {1}", DateTime.Now.ToString(), strFileName);
            }

            return myList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oFTPConfig"></param>
        /// <param name="strTaskFileName"></param>
        /// <returns></returns>
        public bool ResponseSuccessToFtp(FtpConfig oFTPConfig, String strTaskFileName)
        {
            FtpLib ftp = new FtpLib();
            string strLocalFilePath = @"" + oFTPConfig.strLocalFilePath;
            strTaskFileName = strTaskFileName.Replace(".xml", "-" + oFTPConfig.strAreaName + ".ok");
            string strRemoteFilePath = @"" + oFTPConfig.strRemoteSuccessPath + strTaskFileName;
            strLocalFilePath += strTaskFileName;

            int perc, old_perc = 0;

            try
            {
                if (!File.Exists(strLocalFilePath))
                {
                    using (FileStream fs = (File.Create(strLocalFilePath))){}
                }

                ftp.Connect(oFTPConfig.strServer, oFTPConfig.strUserName, oFTPConfig.strPassword, int.Parse(oFTPConfig.strPort));
                ftp.OpenUpload(strLocalFilePath, strRemoteFilePath, false);
                
                while (true)
                {
                    perc = ftp.DoUpload();

                    // No need to report progress everytime we get some bytes
                    // because it causes a flickery effect on the screen in most cases.
                    if (perc > old_perc)
                    {
                        Console.Write("\rFTP Complete: {0}%", perc);
                        Console.Out.Flush();
                    }
                    // is the download done?
                    if (perc == 100)
                        break;

                    old_perc = perc;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alFielList"></param>
        /// <returns></returns>
        public ArrayList GetTaskXmlFileList(ArrayList alFielList)
        {
            ArrayList myList = new ArrayList();
            for (int i = 0; i < alFielList.Count; i++)
            {
                string strFileName = (string)alFielList[i];
                int nIndex = strFileName.IndexOf(".xml", 1);
                if (nIndex > 0)
                {
                    myList.Add(strFileName);
                }
            }

            return myList;
        }

        /// <summary>
        /// 从FTP上取得SQL文件
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="strSql"></param>
        /// <param name="oFTPConfig"></param>
        /// <returns></returns>
        public bool GetSQLFromTaskFile(string strFile, ref string strSql, FtpConfig oFTPConfig)
        {

            string path;
            String strpath = oFTPConfig.strLocalFilePath;
            string strFileName = strFile.Replace(".xml", "-EasyHouseDB.sql");

            try
            {
                DownloadSingleRemoteFile(strFileName, oFTPConfig);
                path = @"" + strpath + strFileName;
                if (!File.Exists(path))
                {
                    return false;
                }
                StreamReader Reader = new StreamReader(path, System.Text.Encoding.Default);
                strSql += Reader.ReadToEnd();
                Reader.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}

