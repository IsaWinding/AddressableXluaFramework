using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MessagePanel : UIBase
{
    public PlayableDirector director;
    public Text text;

    [HideInInspector]
    public string Mesage;

    public override void OnInit()
    {
        director.stopped += OnPlayableDirectorPlayed;
    }
    public override void OnForward()
    {
        text.text = Mesage;
        director.Play();
    }
    void OnPlayableDirectorPlayed(PlayableDirector aDirector)
    {
        UICtrlManager.CloseMessage();
    }
    public override void DoDestroy()
    {
        director.stopped += OnPlayableDirectorPlayed;
    }
}
