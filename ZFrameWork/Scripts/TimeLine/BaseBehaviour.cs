using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BaseBehaviour : PlayableBehaviour
{
    public GameObject target_;
    private float durtion_;
    private float time_;
    private float progress_;
    //public override void OnPlayableCreate(Playable playable)
    //{
    //    //director = playable.GetGraph().GetResolver() as PlayableDirector; 
    //}
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        durtion_ = (float)(playable.GetDuration());
        time_ = (float)(playable.GetTime());
        progress_ = (time_ / durtion_);
        //Debug.LogError("durtion_" + durtion_);
        //Debug.LogError("time_" + time_);
        //Debug.LogError("progress_" + progress_);
        OnProgress(progress_);
    }
    protected virtual void OnProgress(float pProgress) { }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.LogError("OnBehaviourPlay" + info.deltaTime);
        //target.transform.localPosition = start;
        OnStart();
    }
    protected virtual void OnStart()
    {
    }
    //public override void OnBehaviourPause(Playable playable, FrameData info)
    //{
    //    //Debug.LogError("OnBehaviourPause");
    //    target.transform.localPosition = end;
    //}

}
