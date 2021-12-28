using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : BaseBehaviour
{
    public string AddressPath;
    private bool isCanSend = false;
    private AudioClip audioClip;
    private AudioSource audioSource;
    protected override void OnProgress(float pProgress)
    {
        if (pProgress >= 0.1f){
            if (isCanSend){
                if (audioClip == null)
                {
                    if (Application.isPlaying)
                        audioClip = AssetManager.Instance.LoadAsset<AudioClip>(AddressPath);
                    else
                        audioClip = AssetManager.Instance.InstanceInEditor(AddressPath, false) as AudioClip;
                }
                if (audioSource == null)
                {
                    var go = new GameObject("AudioClip");
                    audioSource = go.AddComponent<AudioSource>();

                }
                audioSource.clip = audioClip;
                audioSource.Play();
                isCanSend = false;
            }
        }
        if (pProgress < 0.1f){
            isCanSend = true;
        }
    }

    protected override void OnStart()
    {
        
    }
    protected override void OnDestory()
    {
        if (audioClip != null)
        {
            if (Application.isPlaying)
            {
                AssetManager.Instance.FreeAsset(AddressPath);
                audioClip = null;
            }
            else
            {
                GameObject.DestroyImmediate(audioClip);
                audioClip = null;
            }
        }
    }
}
