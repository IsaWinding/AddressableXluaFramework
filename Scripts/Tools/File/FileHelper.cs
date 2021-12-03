using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class FileHelper
{
    /// <summary>
    /// ��ʽ���ļ�Ŀ¼Ϊͳһ��ʽ
    /// </summary>
    /// <param name="filePath">�ļ�·��</param>
    /// <returns></returns>
    public static string FormatFilePath(string filePath)
    {
        var path = filePath.Replace('\\', '/');
        path = path.Replace("//", "/");
        return path;
    }
    public static void CopyDirectoryAndSuffix(string srcDir, string tgtDir, string pSuffix, string pNewSuffix = null, string pNeedSpecPath = null,
           string pDontNeedSpecPath = null, string pDontNeedSpecPathEx = null)
    {
        DirectoryInfo source = new DirectoryInfo(srcDir);
        DirectoryInfo target = new DirectoryInfo(tgtDir);
        if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase)) { throw new Exception("��Ŀ¼���ܿ�������Ŀ¼��"); }
        if (!source.Exists) { return; }

        FileInfo[] files = source.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            var fixFullName = FormatFilePath(files[i].FullName);
            if (pNeedSpecPath != null && !fixFullName.Contains(pNeedSpecPath))
            {
                continue;
            }
            if (pDontNeedSpecPath != null && fixFullName.Contains(pDontNeedSpecPath))
            {
                continue;
            }
            if (pDontNeedSpecPathEx != null && fixFullName.Contains(pDontNeedSpecPathEx))
            {
                continue;
            }
            if (files[i].FullName.EndsWith(pSuffix))
            {
                if (!target.Exists) { target.Create(); }
                string newFilePath = FormatFilePath(target.FullName + @"\" + files[i].Name);
                if (pNewSuffix != null)
                {
                    if (!newFilePath.EndsWith(pNewSuffix))
                    {
                        int lastIndex = newFilePath.LastIndexOf('.');
                        newFilePath = newFilePath.Substring(0, lastIndex);
                        newFilePath += pNewSuffix;
                    }
                }
                File.Copy(files[i].FullName, newFilePath, true);
            }
        }

        DirectoryInfo[] dirs = source.GetDirectories();

        for (int j = 0; j < dirs.Length; j++)
        {
            CopyDirectoryAndSuffix(dirs[j].FullName, FormatFilePath(target.FullName + @"\" + dirs[j].Name), pSuffix, pNewSuffix, pNeedSpecPath, pDontNeedSpecPath, pDontNeedSpecPathEx);
        }
    }

    /// <summary>
    /// ɾ���������µ�Ŀ¼
    /// </summary>
    /// <param name="dir"></param>
    public static void DeleteCreateNewDirectory(string dir)
    {
        if (Directory.Exists(dir))
            Directory.Delete(dir, true);
        Directory.CreateDirectory(dir);
    }
    /// <summary>
    /// Deletes the files except.
    /// </summary>
    /// <param name="directory">The directory.</param>
    /// <param name="exceptFileSuffix">The except file suffix.</param>
    public static void DeleteFilesExcept(string directory, string exceptFileSuffix)
    {
        if (!Directory.Exists(directory))
            return;
        DirectoryInfo di = new DirectoryInfo(directory);
        var fileInfos = di.GetFiles();
        if (fileInfos != null){
            foreach (var temp in fileInfos){
                if (!temp.FullName.Contains(exceptFileSuffix)){
                    temp.Delete();
                }
            }
        }     
    }
    /// <summary>
    /// ��ȡ�ļ������ֽ�
    /// </summary>
    /// <param name="filepath">�ļ�·��</param>
    /// <returns></returns>
    public static byte[] FileReadAllBytes(string filepath)
    {
        if (File.Exists(filepath))
            return File.ReadAllBytes(filepath);
        else
            return null;
    }
    /// <summary>
    /// �ļ��洢�����ֽ�
    /// </summary>
    /// <param name="filepath">�ļ�·��</param>
    /// <param name="bytes">�ֽ���</param>
    public static void FileWriteAllBytes(string filepath, byte[] bytes)
    {
        CreateDirectoryByFile(filepath);
        File.WriteAllBytes(filepath, bytes);
    }
    /// <summary>
    /// �����ļ�������Ŀ¼
    /// </summary>
    /// <param name="filepath"></param>
    public static void CreateDirectoryByFile(string filepath)
    {
        CreateDirectory(Path.GetDirectoryName(filepath));
    }
    /// <summary>
    /// ����Ŀ¼
    /// </summary>
    /// <param name="dir">Ŀ¼·��</param>
    public static void CreateDirectory(string dir)
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }
    public static bool CopyFile( string sourcePath,  string targetPath)
    {
        var bytes = FileReadAllBytes(sourcePath);
        if (bytes == null)
            return false;
        else
        {
            FileWriteAllBytes(targetPath, bytes);
            return true;
        }
    }
    public static void CopyDirectory(string srcDir,string tgtDir)
    {
        DirectoryInfo source = new DirectoryInfo(srcDir);
        DirectoryInfo target = new DirectoryInfo(tgtDir);
        if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("��Ŀ¼���ܿ�������Ŀ¼��");
        }
        if (!source.Exists)
        {
            return;
        }
        if (!target.Exists)
        {
            target.Create();
        }
        FileInfo[] files = source.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            File.Copy(files[i].FullName, target.FullName + @"\" + files[i].Name, true);
        }
        DirectoryInfo[] dirs = source.GetDirectories();
        for (int j = 0; j < dirs.Length; j++)
        {
            CopyDirectory(dirs[j].FullName, target.FullName + @"\" + dirs[j].Name);
        }
    }
}
