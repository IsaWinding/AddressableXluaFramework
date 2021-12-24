using System;
using UnityEngine;
using XLua;

[System.Serializable]
public class Injection
{
    public string name;
    public GameObject value;
}

[Hotfix]
public class LuaMono : UIBase
{
    public string luaFilePath;
    //public TextAsset luaScript;
    public Injection[] injections;
    internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 
    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;
    private Action luaOnForward;
    private LuaTable scriptEnv;
    public override void OnInit()
    {
        scriptEnv = luaEnv.NewTable();
        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        scriptEnv.Set("self", this);
        foreach (var injection in injections){
            scriptEnv.Set(injection.name, injection.value);
        }
        var text = XLuaClient.Instance.LoadLuaString(luaFilePath);
        luaEnv.DoString(text, "LuaTestScript", scriptEnv);
        Action luaAwake = scriptEnv.Get<Action>("awake");
        scriptEnv.Get("start", out luaStart);
        scriptEnv.Get("update", out luaUpdate);
        scriptEnv.Get("ondestroy", out luaOnDestroy);
        scriptEnv.Get("onforward", out luaOnForward);
        if (luaAwake != null){
            luaAwake();
        }
    }
    public override void OnForward()
    {
        if (luaOnForward != null){
            luaOnForward();
        }
    }
    public override void DoDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        scriptEnv.Dispose();
        injections = null;
    }

    // Use this for initialization
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (luaUpdate != null){
            luaUpdate();
        }
        else
        {
            this.enabled = false;
        }
        if (Time.time - LuaMono.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaMono.lastGCTime = Time.time;
        }
    }
}
