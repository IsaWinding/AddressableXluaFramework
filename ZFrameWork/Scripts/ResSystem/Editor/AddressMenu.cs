using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressMenu 
{
    public static List<string> specPath = new List<string> { "LocalChange", "LocalDontChange", 
        "RemoteChange", "RemoteDontChange", "LuaTxt" };
    //public static string LuaPath = "_ABs/LuaTxt";
    public static string LocalServerWWWPath = "C:/wamp/www";
    public static string GetAbFileRootPath()
    {
        var path = Application.dataPath + "/_ABs";
        path = FormatFilePath(path);
        return path;
    }
    public static string GetServerDataPath()
    {
        var path = Application.dataPath.Replace("Assets", "ServerData");
        path = FormatFilePath(path);
        return path;
    }
    [MenuItem("AddressableMenu/将Xlua .lua文件转为.txt 文件", false, 1)]
    public static void OnKeyUpdateAllLuaToTxt()
    {
        var txtPath = GameSettings.GetLuaTxtPath();
        var luaPath = GameSettings.GetLuaResourcesPath();
        //FileHelper.DeleteFilesExcept(txtPath, ".txt");
        FileHelper.DeleteCreateNewDirectory(txtPath);
        FileHelper.CopyDirectoryAndSuffix(luaPath, txtPath, ".lua", ".txt");
        var xluaTxtPath = GameSettings.GetXLuaTxtPath();
        FileHelper.CopyDirectoryAndSuffix(xluaTxtPath, txtPath, ".txt", ".txt");
        AssetDatabase.Refresh();
    }

    [MenuItem("AddressableMenu/选择一个文件夹并将其下的资源进行分组", priority = 1)]
    public static void SetSelectDirectorToGroup()
    {
        var arr = Selection.GetFiltered(typeof(DefaultAsset), SelectionMode.Assets);
        string folder = AssetDatabase.GetAssetPath(arr[0]);
        if(folder != null)
            LoopSetAllDirectorToAddress(folder);
    }

    [MenuItem("AddressableMenu/清理上次打包的资源包括服务器热更新数据", priority = 2)]
    public static void ClearAllAddressBuild()
    {
        AddressableAssetSettings.CleanPlayerContent();
        var serverDataPath = GetServerDataPath();
        Debug.Log("clear serverdata " + serverDataPath);
        if (System.IO.Directory.Exists(serverDataPath))
        {
            System.IO.Directory.Delete(serverDataPath, true);
        }
    }
    /// <summary>
    /// 重新构建Address资源
    /// </summary>
    [MenuItem("AddressableMenu/清理上次构建的资源并且重新打包", priority = 3)]
    public static void ReBuildAddress()
    {
        ClearAllAddressBuild();
        //1.更新最新的lua 文件
        OnKeyUpdateAllLuaToTxt();
        //2.对所有资源进行重新分组打标签
        LoopSetAllDirectorToAddress(GameSettings.GetABRootPath());
        AddressableAssetSettings.BuildPlayerContent();
    }
    /// 对比更新列表
    [MenuItem("AddressableMenu/获取和上次发包的差异列表", priority = 4)]
    public static void CheckForUpdateContent()
    {
        //与上次打包做资源对比
        string buildPath = ContentUpdateScript.GetContentStateDataPath(false);
        var m_Settings = AddressableAssetSettingsDefaultObject.Settings;
        List<AddressableAssetEntry> entrys = ContentUpdateScript.GatherModifiedEntries(m_Settings, buildPath);
        if (entrys.Count == 0)
        {
            Debug.Log("没有资源更新");
            return;
        }
        StringBuilder sbuider = new StringBuilder();
        sbuider.AppendLine("Need Update Assets:");
        foreach (var _ in entrys){
            sbuider.AppendLine(_.address);
        }
        Debug.Log(sbuider.ToString());
        //将被修改过的资源单独分组
        var groupName = string.Format("UpdateGroup_{0}", DateTime.Now.ToString("yyyyMMdd"));
        ContentUpdateScript.CreateContentUpdateGroup(m_Settings, entrys, groupName);
    }
    //迭代打包
    [MenuItem("AddressableMenu/迭代构建资源", priority = 5)]
    public static void BuildUpdate()
    {
        //1.更新最新的lua 文件
        OnKeyUpdateAllLuaToTxt();
        //2.对Lua 文件进行重新分组打标签
        LoopSetAllDirectorToAddress(GameSettings.GetLuaTxtPath());
        //3.对比更新列表
        CheckForUpdateContent();

        var path = ContentUpdateScript.GetContentStateDataPath(false);
        var m_Settings = AddressableAssetSettingsDefaultObject.Settings;
        AddressablesPlayerBuildResult result = ContentUpdateScript.BuildContentUpdate(AddressableAssetSettingsDefaultObject.Settings, path);
        Debug.Log("BuildFinish path = " + m_Settings.RemoteCatalogBuildPath.GetValue(m_Settings));
    }
    //迭代打包
    [MenuItem("AddressableMenu/复制服务器热更新资源到本地服务器WWW路径", priority = 6)]
    public static void CopyServerDataToLocalServer()
    {
        FileHelper.CopyDirectory(GetServerDataPath(),LocalServerWWWPath);
        Debug.Log("复制服务器热更新资源到本地服务器完成 路径:" +LocalServerWWWPath);
    }
    [MenuItem("AddressableMenu/打印构建路径", priority = 7)]
    public static void Test()
    {
        Debug.Log("BuildPath = " + Addressables.BuildPath);
        Debug.Log("PlayerBuildDataPath = " + Addressables.PlayerBuildDataPath);
        Debug.Log("RemoteCatalogBuildPath = " + AddressableAssetSettingsDefaultObject.Settings.RemoteCatalogBuildPath.GetValue(AddressableAssetSettingsDefaultObject.Settings));
    }
    private static void LoopSetAllDirectorToAddress(string pFileDirectorRoot)
    {
        if (Directory.Exists(pFileDirectorRoot))
        {
            SetDirectorABNameNull(pFileDirectorRoot);
            var dirctory = new DirectoryInfo(pFileDirectorRoot);
            var direcs = dirctory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            if (direcs.Length > 0)
            {
                for (var i = 0; i < direcs.Length; i++)
                {
                    if (direcs[i].FullName != pFileDirectorRoot)
                    {
                        LoopSetAllDirectorToAddress(direcs[i].FullName);
                    }
                }
            }
        }
    }
    private static void SetDirectorABNameNull(string pFileDirectorRoot)
    {
        if (Directory.Exists(pFileDirectorRoot))
        {
            var dirctory = new DirectoryInfo(pFileDirectorRoot);
            var files = dirctory.GetFiles("*", SearchOption.TopDirectoryOnly);
            bool isAdd = false;
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if (file.Name.EndsWith(".meta"))
                    continue;
                string assetPath = file.FullName;
                assetPath = FormatFilePath(assetPath);
                var assetLength = UnityEngine.Application.dataPath.Length - 6;
                assetPath = assetPath.Substring(assetLength, assetPath.Length - assetLength);
                var groupName = GetGroupName(dirctory.Name, assetPath);
                AutoGroup(groupName, assetPath, groupName);
                isAdd = true;
            }
            if (isAdd)
                AssetDatabase.Refresh();
        }
    }
    public static string GetGroupName(string pDirctoryName, string pAssetPath)
    {
        var groupName = pDirctoryName;
        foreach (var item in specPath)
        {
            if (pAssetPath.Contains(item))
            {
                groupName = item;
                break;
            }
        }
        return groupName;
    }
    public static string FormatFilePath(string filePath)
    {
        var path = filePath.Replace('\\', '/');
        path = path.Replace("//", "/");
        return path;
    }
    public static void AutoGroup(string groupName, string assetPath,string pLable)
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        AddressableAssetGroup group = settings.FindGroup(groupName);
        if (group == null){
            group = CreatAssetGroup<System.Data.SchemaType>(settings, groupName);
        }
        var guid = AssetDatabase.AssetPathToGUID(assetPath);
        var entry = settings.CreateOrMoveEntry(guid, group);
        entry.address = assetPath;
        entry.SetLabel(pLable, true, true);
    }
    private static AddressableAssetGroup CreatAssetGroup<SchemaType>(AddressableAssetSettings settings, string groupName)
    {
        return settings.CreateGroup(groupName, false, false, false,
            new List<AddressableAssetGroupSchema> { settings.DefaultGroup.Schemas[0], settings.DefaultGroup.Schemas[1] }, typeof(SchemaType));
    }
}
