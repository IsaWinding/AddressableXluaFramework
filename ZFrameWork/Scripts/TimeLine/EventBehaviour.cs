using EventG;

public class EventBehaviour : BaseBehaviour
{
    public string EventKey;
    public string EventParams;
    private bool isCanSend = false;

    protected override void OnProgress(float pProgress)
    {
        if (pProgress >= 0.5f)
        {
            if (isCanSend)
            {
                var timeLineId = target_.GetComponent<TimeLineId>();
                var doubleInfo = new DoubleInfo(timeLineId.Id, EventParams);
                GlobalEvent.SendMessage(EventKey, doubleInfo);
                isCanSend = false;
            }
        }
        if (pProgress < 0.5f)
        {
            isCanSend = true;
        }
    }
}
