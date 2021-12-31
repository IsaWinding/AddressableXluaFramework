public class Unit:IDriver
{
    public int Id;
    public int TargetId;
    public AttributeC AttributeInfo;
    public StateInfo StateInfo;
    public Unit(int pId, AttributeC pAttributeInfo, StateInfo pStateInfo) {
        Id = pId;
        AttributeInfo = pAttributeInfo;
        StateInfo = pStateInfo;
    }

    private float nextTime = 0;
    public void OnRun(float pTime, float pDeltaTime)
    {
        if (pTime >= nextTime)
        {
            nextTime += 1;
            OnSecondRepeat();
        }
    }
    public void OnSecondRepeat()
    {
        //AttributeInfo.OnSecondRepeat();
    }
}
