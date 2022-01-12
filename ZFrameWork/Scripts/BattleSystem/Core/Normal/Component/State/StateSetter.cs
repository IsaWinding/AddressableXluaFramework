using UnityEngine;

[CreateAssetMenu(menuName = "Creat/NewState")]
public class StateSetter : ScriptableObject
{
    public CampType CampType;
    public string Model;
    public string HpBar;
    public float modelHeight;

    public static StateSetter LoadDataFromFile(string Path){
        var textAsset = AssetManager.Instance.LoadAsset<StateSetter>(Path);
        return textAsset;
    }
    public StateInfo GetStateInfo(Vector3 pBornPos)
    {
        var stateInfo = new StateInfo(pBornPos, CampType) { Model = this.Model , 
            HpBar  = this.HpBar ,
            modelHeight  = this.modelHeight};
        return stateInfo;
    }
}
