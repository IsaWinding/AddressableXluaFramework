using UnityEngine;

[CreateAssetMenu(menuName ="Creat_NewAttribute")]
public class AttributeSetter : ScriptableObject{
    public float Hp;
    public float HpRecover;
    public float Mp;
    public float MpRecover;
    public float Atk;
    public float Def;
    public float MoveSpeed;
    public float AtkSpeed;
    public static AttributeSetter LoadDataFromFile(string Path){
        var textAsset = AssetManager.Instance.LoadAsset<AttributeSetter>(Path);
        return textAsset;
    }
    public AttributeC GetAttributeInfo()
    {
        var attributeInfo = new AttributeC();
        attributeInfo.AddAttribute(AttributeType.Hp, Hp, Hp, HpRecover);
        attributeInfo.AddAttribute(AttributeType.Mp, Mp, Mp, MpRecover);
        attributeInfo.AddAttribute(AttributeType.Atk, Atk);
        attributeInfo.AddAttribute(AttributeType.Def, Def);
        attributeInfo.AddAttribute(AttributeType.MoveSpeed, MoveSpeed);
        attributeInfo.AddAttribute(AttributeType.AtkSpeed, AtkSpeed);
        return attributeInfo;
    }
}
