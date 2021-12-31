using UnityEngine;

[CreateAssetMenu(menuName = "Creat_NewState")]
public class StateSetter : ScriptableObject
{
    public float X;
    public float Y;
    public float Z;
    public CampType CampType;
    public string Model;
    public static StateSetter LoadDataFromFile(string Path){
        var textAsset = AssetManager.Instance.LoadAsset<StateSetter>(Path);
        return textAsset;
    }
    public StateInfo GetStateInfo()
    {
        var stateInfo = new StateInfo(X, Y, Z, CampType) { Model = this.Model };
        return stateInfo;
    }
}
