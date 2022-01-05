public class LoginPanelCtrl : UICtrlBase

{
	protected override string Key { get { return "LoginPanelCtrl"; } }
	protected override string Path { get { return "Assets/_ABs/LocalDontChange/UIPrefabs/LoginPanel.prefab"; } }

    protected override void OnInit()
    {
        this.AddEventListener("ClickCreatBtn", (obj) => {
            AssetManager.Instance.LoadSceneAsync("Assets/_ABs/LocalDontChange/Scenes/gameLv02.unity", (oSceneIns) => {
                UnityEngine.Debug.LogError("load finish");
            }, (oProgress) => {
                UnityEngine.Debug.LogError("oProgress" + oProgress);
            });
        });
        this.AddEventListener("ClickContinue", (obj) => {

            UnityEngine.SceneManagement.SceneManager.LoadScene("4_BattleMap");

            //AssetManager.Instance.LoadSceneAsync("Assets/_ABs/LocalDontChange/Scenes/gameLv02.unity", (oSceneIns) => {
            //    UnityEngine.Debug.LogError("load finish");
            //}, (oProgress) => {
            //    UnityEngine.Debug.LogError("oProgress" + oProgress);
            //});
        });
    }
}
