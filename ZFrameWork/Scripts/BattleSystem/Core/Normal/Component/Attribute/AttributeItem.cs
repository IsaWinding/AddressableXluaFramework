public class AttributeItem
{
    public AttributeType Type;
    public AttributeValue Value;
    public AttributeItem(AttributeType pType,float pValue,float pMax = -1,float pRecover = 0) {
        Type = pType;
        Value = new AttributeValue(pValue, pMax, pRecover);
    }
    public void OnRecover(){
        Value.OnRecover();
    }
}
