public class LoginPanelCtrl : UICtrlBase
{
	protected override string Key { get { return "LoginPanelCtrl"; } }
	protected override string Path { get { return "Assets/_ABs/LocalChange/UIPrefabs/LoginPanel.prefab"; } }

    protected override void OnInit()
    {
        this.AddEventListener("ClickCreatBtn", (obj) => {
            
        });
        this.AddEventListener("ClickContinue", (obj) => {
            
        });
    }
}
