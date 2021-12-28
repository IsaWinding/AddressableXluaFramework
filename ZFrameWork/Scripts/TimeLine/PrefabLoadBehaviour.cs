using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLoadBehaviour : BaseBehaviour
{
    public string AddressPath;
    public bool isNeedReLoad;
    public bool isParticle;//是否是粒子系统
    public Transform parent;
    public Vector3 offset;
    private GameObject prefab_;
    
    private ParticleSystem[] _particles;
    private Animation[] _animations;
    private Animator[] _animators;

    private int frameCount = 5 * 60 + 1;
    private float _time;
    private bool _isCanLoad = false;
    protected override void OnProgress(float pProgress)
    {
        if (pProgress >= 0.1f && _isCanLoad)
        {
            if (_isCanLoad)
            {
                if (prefab_ == null)
                {
                    if (Application.isPlaying)
                        prefab_ = AssetManager.Instance.Instantiate(AddressPath);
                    else
                        prefab_ = AssetManager.Instance.InstanceInEditor(AddressPath, true) as GameObject;
                    prefab_.transform.parent = parent;
                    prefab_.transform.localPosition = offset;
                    prefab_.transform.localScale = Vector3.one;
                    prefab_.transform.localRotation = Quaternion.identity;
                    if (isParticle && !Application.isPlaying)
                    {
                        frameCount = (int)durtion_ * 60;
                        _animations = prefab_.GetComponentsInChildren<Animation>();
                        _animators = prefab_.GetComponentsInChildren<Animator>();
                        _particles = prefab_.GetComponentsInChildren<ParticleSystem>();
                        //Bake Animator's animation to the buffer
                        if (_animators != null)
                        {

                            foreach (var at in _animators)
                            {
                                at.Rebind();
                                at.StopPlayback();
                                at.recorderStartTime = 0f;
                                at.StartRecording(frameCount);//will the recorded frames
                                for (int i = 1; i <= frameCount; i++)
                                {
                                    //recording every frame with delta time
                                    at.Update(1.0f / 60);
                                }
                                //stop record and start playback
                                at.StopRecording();
                                at.StartPlayback();
                                at.playbackTime = 0f;
                                at.Update(0);
                            }
                        }
                    }
                }
                else
                {
                    prefab_.SetActive(true);
                }
                if (isParticle && prefab_ != null && !Application.isPlaying)
                {
                    _time = durtion_ * pProgress;
                    PlayBackAnimation();
                    PlayBackAnimator();
                    PlayBackParticle();
                }
            }
            _isCanLoad = false;
        }
        else
        {
            _isCanLoad = true;
            if (prefab_ != null)
            {
                if (isNeedReLoad)
                {
                    UnLoadPrefab();
                }
                else
                {
                    prefab_.SetActive(false);
                }
            }
        }
    }
    private void PlayBackParticle(){
        if (_particles != null){
            foreach (var psys in _particles){
                //need to close the random seed to control  ParticleSystem playback
                //psys.useAutoRandomSeed = false;
                // Simulate the ParticleSystem to the time
                psys.Simulate(_time);
            }
        }
    }
    private void PlayBackAnimation(){
        if (_animations != null){
            foreach (var animation in _animations){
                if (animation.clip){
                    var state = animation[animation.clip.name];
                    if (state){
                        animation.Play(state.name);
                        state.time = _time;
                        state.speed = 0f;
                    }
                }
            }
        }
    }
    private void PlayBackAnimator(){
        if (_animators == null)
            return;
        foreach (var at in _animators){
            at.playbackTime = _time;
            at.Update(0);
        }
    }
    protected override void OnEnd()
    {
        UnLoadPrefab();
    }
    private void UnLoadPrefab(){
        if (prefab_ != null){
            if (Application.isPlaying){
                AssetManager.Instance.FreeGameObject(prefab_);
                prefab_ = null;
            }
            else{
                GameObject.DestroyImmediate(prefab_);
                prefab_ = null;
            }
        }
    }

    protected override void OnDestory(){
        UnLoadPrefab();
    }
}
