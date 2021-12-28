using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AudioAsset : BaseAsset
{
    [OnValueChanged("AddAssetToAddressable")]
    public AudioClip AudioSource;
    public string AddressPath;//×ÊÔ´Â·¾¶

    public override BaseBehaviour GetBehaviour(PlayableGraph graph, GameObject go)
    {
        var baseBehaviour = new AudioBehaviour();
        baseBehaviour.AddressPath = AddressPath;
        return baseBehaviour;
    }

#if UNITY_EDITOR
    public void AddAssetToAddressable()
    {
        if (AudioSource != null)
        {
            AddressPath = UnityEditor.AssetDatabase.GetAssetPath(AudioSource);
        }
    }
#endif
}
