using EventG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlBase
{
	protected virtual string Key { get { return "UICtrlBase"; } }//Ctrl 的key 数值
	protected virtual string Path { get { return ""; } }//加载的资源路径
	private StringKeySender Sender ;//Ctrl 层的内部事件发送

	/// <summary> 打开流程
	/// OnInit --> OnSetData --> OnPost  --> ResLoad --> OnForward
	/// </summary>

	/// <summary> 关闭流程
	/// OnDispose --> UnLoadRes
	/// </summary>
	/// 
	private object data_;//Ctrl层持有的数据
	private GameObject panelGO;//资源prefab
	protected UIBase uiBase;//面板
	public void DoInit(){
		Sender = new StringKeySender();
		OnInit();
	}
	//初始化
	protected virtual void OnInit() {}
	public void DoSetData(Object pData) {
		data_ = pData;
		OnSetData(pData);
	}
	//基本数据的设置
	public virtual void OnSetData(Object pData){}

	public virtual void OnPost(System.Action<bool> pCb)//异步逻辑的处理（例如网络通信层的处理）返回true 界面加载成功 返回false 界面打开失败
	{
		pCb.Invoke(true);
	}
	private bool isLoading = false;
	public void ResLoad(System.Action<UnityEngine.GameObject> pCB,Transform pSubRoot)//资源加载
	{
		if (!isLoading)
		{
			isLoading = true;
			AssetManager.Instance.InstantiateAsync(Path, (oGO) => {
				if (oGO != null)
				{
					var go = oGO as GameObject;
					uiBase = go.GetComponent<UIBase>();
					uiBase.SetSender(Sender);
					go.transform.SetParent(pSubRoot);
					go.transform.localPosition = Vector3.zero;
					go.transform.localScale = Vector3.one;
				}
				panelGO = oGO;
				pCB.Invoke(oGO as GameObject);
				isLoading = false;
			});
		}			
	}
	public void UnLoadRes()//资源卸载
	{
		AssetManager.Instance.FreeGameObject(panelGO);
	}
	public void OnHide()
	{
		if (uiBase != null)
		{
			uiBase.gameObject.SetActive(false);
		}
	}
	public bool IsHaveUIBase()
	{
		return uiBase != null;
	}
	public void DoForward()//界面切到前台
	{
		OnForward();
		uiBase.gameObject.SetActive(true);
		uiBase.OnForward();
	}
	protected virtual void OnForward()
	{ 
		
	}
	//ctrl 退出
	protected virtual void OnDispose() {}
	public void DoDispose()
	{
		Sender.Clear();
		OnDispose();
	}

	public void AddEventListener(string pKey, System.Action<object> pAction)
	{
		Sender.AddListener(pKey, pAction);
	}
	public void RemoveListener(string pKey, System.Action<object> pAction)
	{
		Sender.RemoveListener(pKey, pAction);
	}
}
