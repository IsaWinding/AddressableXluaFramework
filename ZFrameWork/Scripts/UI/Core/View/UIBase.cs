using EventG;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private bool isInit = false;
    protected StringKeySender Sender;
    protected bool needRepeat = false;
    protected float repeatTime = 1;
    public void SendMessageToCtrl(string pString,object pParams = null) {
        if (Sender != null)
        {
            Sender.SendMessage(pString, pParams);
        }
        else
        {
            var sender = GetParentSender(this.transform);
            if (sender != null)
            {
                Sender = sender;
                Sender.SendMessage(pString, pParams);
            }
            else
            {
                Debug.LogError("is not have ctrl event sender!!");
            }
        }
    }
    private StringKeySender GetParentSender(Transform pTarget)
    {
        var uiBase = pTarget.gameObject.GetComponentInParent<UIBase>();
        if (uiBase != null )
        {
            if (uiBase.Sender != null)
                return uiBase.Sender;
            else
            {
                if (uiBase.transform.parent == null)
                    return null;
                else
                    return GetParentSender(uiBase.transform.parent);
            }
        }
        else
        {
            return null;
        }
    }
    public void SetSender(StringKeySender pSender)
    {
        Sender = pSender;
    }
   
    private void Awake() { Init(); }
    private void Init(){
        if (!isInit){
            isInit = true;
            OnInit();
        }
    }

    private void Start()
    {
        if (needRepeat){
            InvokeRepeating("OnRepeat", 1, repeatTime);
        }
        else {
            CancelInvoke("OnRepeat");   
        }
    }
    public virtual void OnRepeat()
    { 
        
    }
    public virtual void OnInit() { }//面板的初始化
    public void DoForward() { OnForward(); }
    //面板被切到最上层显示
    public virtual void OnForward() {} 
    public virtual void DoDestroy() { }//面板被销毁
    private void OnDestroy() { DoDestroy(); }
}
