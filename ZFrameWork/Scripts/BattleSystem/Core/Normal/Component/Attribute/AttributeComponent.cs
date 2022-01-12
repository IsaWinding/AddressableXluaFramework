using System.Collections.Generic;

public class AttributeComponent
{
    private Dictionary<AttributeType, AttributeItem> attributes = 
        new Dictionary<AttributeType, AttributeItem>();
    public void AddAttributeItem(AttributeItem pAttributeItem){
        attributes.Add(pAttributeItem.Type, pAttributeItem);
    }
    public void AddAttribute(AttributeType pType,float pValue,float pMax = -1,float pRecover = 0){
        var attributeItem = new AttributeItem(pType, pValue, pMax, pRecover);
        AddAttributeItem(attributeItem);
    }
    public AttributeItem GetAttributeItem(AttributeType pType)
    {
        AttributeItem AttributeItem;
        attributes.TryGetValue(pType, out AttributeItem);
        return AttributeItem;
    }
    public void OnSecondRepeat() {
        foreach (var temp in attributes.Values)
        {
            temp.OnRecover();
        }
    }

}
