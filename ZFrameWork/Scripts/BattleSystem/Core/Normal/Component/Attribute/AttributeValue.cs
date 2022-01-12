public class AttributeValue{
    public float Value;//��ǰ��������ֵ
    public float Max;//����������ֵ -1 Ϊ����������
    public float Recover;//����ֵ�Ļָ���ֵ(ÿ��ָ�������)

    private System.Action<AttributeValue> onChange;
    public void AddChangeAction(System.Action<AttributeValue> pOnChangeAction) {
        onChange += pOnChangeAction;
    }
    public void RemoveChangeAction(System.Action<AttributeValue> pOnChangeAction) {
        onChange -= pOnChangeAction;
    }
    public AttributeValue(float pValue,float pMax = -1,float pRecover = 0) {
        Value = pValue;
        Max = pMax;
        Recover = pRecover;
    }
    //ÿ����õĻָ�
    public void OnRecover(){
        if(Recover != 0)
            AddValue(Recover);
    }
    //��ֵ���ⲿ�ı�
    public void AddValue(float pValue){
        Value += pValue;
        if (Value < 0)
            Value = 0;
        if (Max != -1 && Value > Max)
            Value = Max;
        if (onChange != null)
        {
            onChange.Invoke(this);
        }
    }
    public float GetValue(){
        return Value;
    }
}
