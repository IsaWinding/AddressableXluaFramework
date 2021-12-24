using UnityEngine;
using UnityEngine.UI;

public class ImageColorBehaviour : BaseBehaviour
{
    public Color start;
    public Color end;
    private Image image;
    protected override void OnProgress(float pProgress){
        if (image == null)
            image = target_.GetComponentInChildren<Image>();
        image.color = start + (end - start) * pProgress;
    }
}