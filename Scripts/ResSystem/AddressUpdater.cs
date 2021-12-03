using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressUpdater : MonoBehaviour
{
    private string str;
    public Text outputText;
    private List<object> _updateKeys = new List<object>();
    // Start is called before the first frame update
    void Start()
    {
        UpdateCatalog();
        
    }
    private void SetRemoteLoadPath()
    {
        string remoteLoadPath = "http://localhost/TapTap";
        AddressableAssetSettings m_Settings = AddressableAssetSettingsDefaultObject.Settings;
        string profileId = m_Settings.profileSettings.GetProfileId("Dynamic");
        m_Settings.profileSettings.SetValue(profileId, AddressableAssetSettings.kRemoteLoadPath, remoteLoadPath);
        Debug.Log(string.Format("设置Addressables Groups Profile完成\n{0}:{1}"
            , AddressableAssetSettings.kRemoteLoadPath, remoteLoadPath));
    }

    /// <summary>
    /// 对比更新Catalog
    /// </summary>
    public async void UpdateCatalog()
    {
        str = "";
        var handlew = Addressables.InitializeAsync();
        await handlew.Task;
        //开始连接服务器检查更新
        var handle = Addressables.CheckForCatalogUpdates(false);
        await handle.Task;
        Debug.Log("check catalog status " + handle.Status);
        ShowLog("check catalog status " + handle.Status);
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            List<string> catalogs = handle.Result;
            if (catalogs != null && catalogs.Count > 0)
            {
                foreach (var catalog in catalogs)
                {
                    Debug.Log("catalog  " + catalog);
                    ShowLog("catalog  " + catalog);
                }
                Debug.Log("download catalog start ");
                str += "download catalog start \n";
                outputText.text = str;
                var updateHandle = Addressables.UpdateCatalogs(catalogs, false);
                await updateHandle.Task;
                foreach (var item in updateHandle.Result)
                {
                    Debug.Log("catalog result " + item.LocatorId);
                    ShowLog("catalog result " + item.LocatorId);
                    foreach (var key in item.Keys)
                    {
                        Debug.Log("catalog key " + key);
                        ShowLog("catalog key " + key);
                    }
                    _updateKeys.AddRange(item.Keys);
                }
                Debug.Log("download catalog finish " + updateHandle.Status);
                ShowLog("download catalog finish " + updateHandle.Status);
            }
            else
            {
                Debug.Log("dont need update catalogs");
                ShowLog("dont need update catalogs");
            }
        }
        Addressables.Release(handle);
        DownLoad();
    }
    /// <summary>
    /// 主界面显示Log
    /// </summary>
    /// <param name="textStr"></param>
    private void ShowLog(string textStr)
    {
        str += textStr + "\n";
        outputText.text = str;
    }

    public IEnumerator DownAssetImpl()
    {
        var downloadsize = Addressables.GetDownloadSizeAsync(_updateKeys);
        yield return downloadsize;
        Debug.Log("start download size :" + downloadsize.Result);
        ShowLog("start download size :" + downloadsize.Result);
        if (downloadsize.Result > 0)
        {
            var download = Addressables.DownloadDependenciesAsync(_updateKeys);//, Addressables.MergeMode.Union
            yield return download;
            //await download.Task;
            if (download.Result != null)
            {
                Debug.Log("download result type " + download.Result.GetType());
                ShowLog("download result type " + download.Result.GetType());
                foreach (var item in download.Result as List<UnityEngine.ResourceManagement.ResourceProviders.IAssetBundleResource>)
                {
                    var ab = item.GetAssetBundle();
                    Debug.Log("ab name " + ab.name);
                    ShowLog("ab name " + ab.name);
                    foreach (var name in ab.GetAllAssetNames())
                    {
                        Debug.Log("asset name " + name);
                        ShowLog("asset name " + name);
                    }
                }
            }
            
            Addressables.Release(download);
        }
        Addressables.Release(downloadsize);
        OnLoadXLuaClient();
    }

    /// <summary>
    /// 下载资源
    /// </summary>
    public void DownLoad()
    {
        str = "";
        StartCoroutine(DownAssetImpl());
        //CtrlLoadSlider();
    }
 
    public void OnLoadXLuaClient()
    {
        //AddressLoadManager.LoadLuaTxtRes(()=> {
        //    if (XLuaClient.Instance == null)
        //    {
        //        var go = new GameObject("XLuaClient");
        //        go.AddComponent<XLuaClient>();
        //    }
        //    XLuaClient.Instance.StartMain();
        //    OnDownLoadFinish();
        //});
        if (XLuaClient.Instance == null)
        {
            var go = new GameObject("XLuaClient");
            go.AddComponent<XLuaClient>();
        }
        else
        {
            XLuaClient.Instance.OnClear();
        }
        OnDownLoadFinish();
    }
    public void OnDownLoadFinish()
    {
        ShowLog("下载资源完成！！");
        UnityEngine.SceneManagement.SceneManager.LoadScene("2_GameMain");
    }

}
