using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Web;

namespace iFlyTek.Common.Ftp
{
    /// <summary>
    /// Dispose ��ժҪ˵����
    /// </summary>
    public class FtpClient
    {
        public FtpClient(){}


        /// <summary>
        /// ��FTP�ϴ��ļ�
        /// </summary>
        /// <param name="localPath">����Ŀ¼</param>
        /// <param name="fileName">�ļ���</param>
        /// <param name="errorinfo">������Ϣ</param>
        /// <param name="type">��������</param>
        /// <param name="filename">�ļ���</param>
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
                errorinfo=string.Format("������������:{0},�޷��ҵ������ļ�:{1}",type,localPath);

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
                errorinfo=string.Format("������������:{0},�ϴ��ļ�:{1},�����쳣��{2}",type,localPath,ex.Message.ToString());
                
                return false;
            }
            finally
            {
                ftp.Disconnect();
            }
        }

        /// <summary>
        /// ���ر����ļ���Զ��Ŀ¼
        /// </summary>
        /// <param name="strFileName">�ļ���</param>
        /// <param name="oFTPConfig">������</param>
        /// <returns>������</returns>
        public bool UploadSingleLocalFile(string strFileName, FtpConfig oFTPConfig)
        {
            string strLocalFileName = @"" + oFTPConfig.strLocalFilePath + strFileName;
            string strRemoteFileName = @"" + oFTPConfig.strRemoteFilePath + strFileName;

            FtpLib ftp = new FtpLib();

            Console.WriteLine("�������ļ��� {0} ��ʼ���ر����ļ���Զ��Ŀ¼ {1}", DateTime.Now.ToString(), strFileName);

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
                        Console.Write("\r�����ؽ��ȡ� FTP Complete: {0}%", perc);
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
        /// ����Զ��ָ��Ŀ¼�µ������ļ�
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
        /// ����Զ�̵ĵ����ļ�
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

            Console.WriteLine("�������ļ��� {0} ��ʼ��Զ��Ŀ¼�����ļ������� {1}", DateTime.Now.ToString(), strFileName);

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
                        Console.Write("\r���������ȡ� Ftp Complete: {0}%", perc);
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
        /// ��ȡĿ¼�µ��ļ��б�
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
                // �ļ���ȫ�޶���
                string strFileName = (string)alFielList[i];

                // ƥ���ַ���
                string characterFirst = "";
                string characterSecond = "";

                // ƥ���ַ���������
                int indexFirst = 0;
                int indexSecond = 0;

                characterFirst = " ";
                indexFirst = strFileName.LastIndexOf(characterFirst);
                if (indexFirst < 0) continue;

                characterSecond = ".n";
                indexSecond = strFileName.LastIndexOf(characterSecond);
                if (indexSecond < 0) continue;

                if (indexFirst > indexSecond) continue;

                // ȥ��ƥ���ַ���
                indexFirst = indexFirst + characterFirst.Length;

                // ����ƥ���ַ���
                indexSecond = indexSecond + characterSecond.Length - indexFirst;

                strFileName =
                    strFileName.Substring(indexFirst, indexSecond);
                myList.Add(strFileName);

                Console.WriteLine("���ļ��б� {0} ��FTPĿ¼�����ļ� {1}", DateTime.Now.ToString(), strFileName);
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
        /// ��FTP��ȡ��SQL�ļ�
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

