using UnityEngine;

[CreateAssetMenu(menuName = "Creat_Unit")]
public class UnitSetter : ScriptableObject
{
    public AttributeSetter attributeSetter;
    public StateSetter stateSetter;
    public static UnitSetter LoadDataFromFile(string Path)
    {
        var textAsset = AssetManager.Instance.LoadAsset<UnitSetter>(Path);
        return textAsset;
    }
    public Unit GetUnit(int pId)
    {
        var unit = new Unit(pId, attributeSetter.GetAttributeInfo(), stateSetter.GetStateInfo());
        return unit;
    }
}
