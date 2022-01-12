using UnityEngine;

[CreateAssetMenu(menuName ="Creat/NewAttribute")]
public class AttributeSetter : ScriptableObject{
    public float Hp;
    public float HpRecover;
    public float Mp;
    public float MpRecover;
    public float Atk;
    public float Def;
    public float MoveSpeed;
    public float AtkCd;
    public float AtkRange;
    public float WarnRange;
    public float FlowRange;
    public static AttributeSetter LoadDataFromFile(string Path){
        var textAsset = AssetManager.Instance.LoadAsset<AttributeSetter>(Path);
        return textAsset;
    }
    public AttributeComponent GetAttributeInfo()
    {
        var attributeInfo = new AttributeComponent();
        attributeInfo.AddAttribute(AttributeType.Hp, Hp, Hp, HpRecover);
        attributeInfo.AddAttribute(AttributeType.Mp, Mp, Mp, MpRecover);
        attributeInfo.AddAttribute(AttributeType.Atk, Atk);
        attributeInfo.AddAttribute(AttributeType.Def, Def);
        attributeInfo.AddAttribute(AttributeType.MoveSpeed, MoveSpeed);
        attributeInfo.AddAttribute(AttributeType.AtkCd, AtkCd);
        attributeInfo.AddAttribute(AttributeType.AtkRange, AtkRange);
        attributeInfo.AddAttribute(AttributeType.WarnRange, WarnRange);
        attributeInfo.AddAttribute(AttributeType.FlowRange, FlowRange);
        return attributeInfo;
    }
}
