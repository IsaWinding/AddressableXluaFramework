using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlPolicyBase
{
	protected GameObject SubRoot;
	protected int BaseLayer;
	protected List<UICtrlBase> ctrls = new List<UICtrlBase>();
	public CtrlPolicyBase() { }
	public CtrlPolicyBase(GameObject pSubRoot, int pBaseLayer)
	{
		SubRoot = pSubRoot;
		BaseLayer = pBaseLayer;
	}
	public virtual void Open(UICtrlBase pCtrl, System.Action pFinishCB, OpenData pOpenData){
		
	}
	public UICtrlBase GetTopCtrl()
	{
		if (ctrls.Count > 0)
			return ctrls[ctrls.Count - 1];
		else
			return null;
	}
	public virtual void RevokeToHome()
	{
		var totalCount = ctrls.Count;
		if (totalCount > 1)
		{
			for (var i = 0; i < totalCount - 1; i++)
			{
				CloseTop(i == totalCount - 2);
			}
		}
	}
	public virtual void RevokeAll()
	{
		var totalCount = ctrls.Count;
		if (totalCount >= 1)
		{
			for (var i = 0; i < totalCount; i++)
			{
				CloseTop(false);
			}
		}
	}

	public virtual void CloseTop(bool pNeedForward = true)
	{
	}

}
public enum OpenType
{
	None = 0,       //����ǰһ���������κδ���
	DisablePre = 1,//����ǰһ�����
	DestoryPre = 2,//����ǰһ�����
}
public class OpenData
{
	public OpenType OpenType = OpenType.None;
	public Object ctrlData;

}
