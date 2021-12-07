using EventG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrlBase
{
	protected virtual string Key { get { return "UICtrlBase"; } }//Ctrl ��key ��ֵ
	protected virtual string Path { get { return ""; } }//���ص���Դ·��
	private StringKeySender Sender ;//Ctrl ����ڲ��¼�����

	/// <summary> ������
	/// OnInit --> OnSetData --> OnPost  --> ResLoad --> OnForward
	/// </summary>

	/// <summary> �ر�����
	/// OnDispose --> UnLoadRes
	/// </summary>
	/// 
	private object data_;//Ctrl����е�����
	private GameObject panelGO;//��Դprefab
	protected UIBase uiBase;//���
	public void DoInit(){
		Sender = new StringKeySender();
		OnInit();
	}
	//��ʼ��
	protected virtual void OnInit() {}
	public void DoSetData(Object pData) {
		data_ = pData;
		OnSetData(pData);
	}
	//�������ݵ�����
	public virtual void OnSetData(Object pData){}

	public virtual void OnPost(System.Action<bool> pCb)//�첽�߼��Ĵ�����������ͨ�Ų�Ĵ�������true ������سɹ� ����false �����ʧ��
	{
		pCb.Invoke(true);
	}
	private bool isLoading = false;
	public void ResLoad(System.Action<UnityEngine.GameObject> pCB,Transform pSubRoot)//��Դ����
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
	public void UnLoadRes()//��Դж��
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
	public void DoForward()//�����е�ǰ̨
	{
		OnForward();
		uiBase.gameObject.SetActive(true);
		uiBase.OnForward();
	}
	protected virtual void OnForward()
	{ 
		
	}
	//ctrl �˳�
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
