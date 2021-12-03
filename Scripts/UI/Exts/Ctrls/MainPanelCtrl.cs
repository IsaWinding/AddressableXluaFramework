public class MainPanelCtrl : UICtrlBase
{
	protected override string Key { get { return "MainPanelCtrl"; } }
	protected override string Path { get { return "Assets/_ABs/UIPrefabs/MainPanel.prefab"; } }
    protected override void OnInit()
    {
        this.AddEventListener("BtnonClick", (obj) => {
            UICtrlManager.OpenBaseUI(new SubPanelCtrl(), () => { }, UICtrlManager.DefaultOpen);
        });
    }
}
