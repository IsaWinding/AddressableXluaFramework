using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePanelCtrl : UICtrlBase
{
    protected override string Key { get { return "MessagePanel"; } }
    protected override string Path { get { return "Assets/_ABs/UIPrefabs/MessagePanel.prefab"; } }
    public string message;
    protected override void OnInit()
    {
        //this.AddEventListener("ClickCreatBtn", (obj) => {
        //    UICtrlManager.OpenBaseUI(new CreatCharacterPanelCtrl(), () => { }, UICtrlManager.DestoryOpen);
        //});
        //this.AddEventListener("ClickContinue", (obj) => {

        //});
    }
    protected override void OnForward()
    {
        var uiBase_ = uiBase as MessagePanel;
        uiBase_.Mesage = message;
    }
}
