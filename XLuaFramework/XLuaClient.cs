using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public enum LuaLoaderType
{ 
    LuaFile = 1,//lua的原文件读取模式
    LuaAddressableTxt = 2,//addressable 资源的读取模式
}
public class XLuaClient : MonoBehaviour
{
    private static XLuaClient instance;
    private Dictionary<string, byte[]> bytesMap = new Dictionary<string, byte[]>();
    public static XLuaClient Instance { get { return instance; } }
    private static LuaEnv luaEnv;
    private const string FileRoot = "/XLuaFramework/Lua/";
    private const string AddressLuaTxtRoot = "Assets/_ABs/LuaTxt/";
    private LuaLoaderType loaderType = LuaLoaderType.LuaAddressableTxt;

    private void Awake(){
        instance = this;
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(Loader);
        GameObject.DontDestroyOnLoad(this);
    }
    public void OnClear()
    {
        bytesMap.Clear();
    }
    public string LoadLuaString(string filePath)
    {
        string string_;
        if (loaderType == LuaLoaderType.LuaFile)
        {
            string_ = GetLuaFileText(filePath);
        }
        else
        {
            string_ = GetAddressableLuaString(filePath);
        }
        return string_;
    }
    public static string LoaderString(string filePath)
    {
        var realFilePath = Application.dataPath + FileRoot + filePath + ".lua";
        var string_ = System.IO.File.ReadAllText(realFilePath);
        return string_;
    }
    public static string GetAddressableLuaTxtPath(string pFilePath)
    {
        var path = AddressLuaTxtRoot + pFilePath + ".txt";
        return path;
    }
    public string GetAddressableLuaString(string filePath)
    {
        var addressTxtPath = GetAddressableLuaTxtPath(filePath);
        var luaAssetText = AssetManager.Instance.LoadAsset<TextAsset>(addressTxtPath);
        return luaAssetText.text;
    }
    public string GetLuaFileText(string filePath)
    {
        var realFilePath = Application.dataPath + FileRoot + filePath + ".lua";
        var string_ = System.IO.File.ReadAllText(realFilePath);
        return string_;
    }

    private byte[] Loader(ref string filePath)
    {
        Debug.LogError("filePath" + filePath);
        if (bytesMap.ContainsKey(filePath))
            return bytesMap[filePath];
        var fixPath = filePath.Replace('.','/');
        var string_ = LoadLuaString(fixPath);
        var bytes = System.Text.Encoding.UTF8.GetBytes(string_);
        bytesMap.Add(filePath, bytes);
        return bytes;
    }
    public void StartMain()
    {
        luaEnv.DoString("require 'GameStartUp'");
    }
    public void DoString(string sring_)
    {
        luaEnv.DoString(sring_);
    }
    private void OnDestroy()
    {
        luaEnv.Dispose();
    }

}
