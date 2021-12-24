using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;


[LuaCallCSharp]
public class UICtrlManager
{
	private static string CanvasRootPath = "Canvas";
	//栈式打开策略 所有界面都会打开，优先关闭最顶层界面也就是最后入场的界面
	public static UICtrlPolicy BasePolicy = new UICtrlPolicy(GameObject.Find(CanvasRootPath), 100);
	//队列式打开策略 当前界面只有一个，其他界面打开需要当前界面关闭才会执行打开逻辑
	public static UICtrlPolicyQueue QueuePolicy = new UICtrlPolicyQueue(GameObject.Find(CanvasRootPath), 100);

	//队列式打开策略 当前界面只有一个，其他界面打开需要当前界面关闭才会执行打开逻辑
	public static UICtrlPolicyQueue MessageQueuePolicy = new UICtrlPolicyQueue(GameObject.Find(CanvasRootPath), 100);

	public static OpenData DefaultOpen = new OpenData() { OpenType = OpenType.None };
	public static OpenData DiableOpen = new OpenData() { OpenType = OpenType.DisablePre };
	public static OpenData DestoryOpen = new OpenData() { OpenType = OpenType.DestoryPre };

	public static void OpenLoginPanel()
    {
		var loginCtrl = new LoginPanelCtrl();
		OpenBaseUI(loginCtrl,null);
    }
	public static void ShowMessage(string pMessage)
	{
		var ctrl_ = new MessagePanelCtrl() { message = pMessage};
		MessageQueuePolicy.Open(ctrl_,()=> { }, null);
	}

	public static void CloseMessage()
	{
		MessageQueuePolicy.CloseTop();
	}
	public static void QueueOpen(UICtrlBase ctrl, System.Action pCB, OpenData pOpenData = null)
	{
		QueuePolicy.Open(ctrl, pCB, pOpenData);
	}
	public static void CloseTopQueueUI()
	{
		QueuePolicy.CloseTop();
	}

	public static void OpenBaseUI(UICtrlBase ctrl, System.Action pCB, OpenData pOpenData = null)
	{
		BasePolicy.Open(ctrl, pCB, pOpenData);
	}
	public static void CloseTopBaseUI()
	{
		BasePolicy.CloseTop();
	}
	public static void RevokeAll()
	{
		BasePolicy.RevokeAll();
	}
	public static void RevokeToHomeBaseUI()
	{
		BasePolicy.RevokeToHome();
	}

}
