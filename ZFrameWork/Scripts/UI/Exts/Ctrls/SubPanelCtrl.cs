public class SubPanelCtrl : UICtrlBase
{
    protected override string Key { get { return "SubPanelCtrl"; } }
    protected override string Path { get { return "Assets/_ABs/UIPrefabs/SubPanel.prefab"; } }
    protected override void OnInit()
    {
        this.AddEventListener("ClickSub1", (obj) => {
            var subPanel = uiBase as SubPanel;
            subPanel.OpenSubUI(1);
        });
        this.AddEventListener("ClickSub2", (obj) => {
            var subPanel = uiBase as SubPanel;
            subPanel.OpenSubUI(2);
        });
    }
}