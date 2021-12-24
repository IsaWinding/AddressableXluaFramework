using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageData : ComponentData
{
    private Sprite sprite;
    public float fillAmount = 0;
    public override ComponentType ComType { get { return ComponentType.Image; } }
    public ImageData(string pPath, Sprite pSprite,SetType pSetType = SetType.MainSet,bool pEnable = true)
    {
        Path = pPath;
        sprite = pSprite;
        setType = pSetType;
        Enable = pEnable;
    }
    public void SetByInfo() { }
    public override void OnLoadComponent(Component pComponent)
    {
        if (setType == SetType.MainSet)
        {
            var com = pComponent as Image;
            com.sprite = sprite;
        }
        else if (setType == SetType.FillAmount)
        {
            var com = pComponent as Image;
            com.fillAmount = fillAmount;
        }
    }
}
