using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Battle.Skill
{
    public class SkillReleaser : MonoBehaviour
    {
        private PlayableDirector playableDirector;
        private Unit unit;
        private PlayableAsset skillAsset;
        private System.Action onFinish;
        private void Awake()
        {
            playableDirector = this.gameObject.GetComponent<PlayableDirector>();
            playableDirector.playOnAwake = false;
        }
        public void BindUnit(Unit pUnit){unit = pUnit;}
        public bool IsCanRelease(Skill pSkill) {
            return pSkill.IsCanRelease();
        }
        public bool ReleaseSkill(Skill pSkill,System.Action pFinish){
            if (!IsCanRelease(pSkill))
            {
                pFinish?.Invoke();
                return false;
            }
            pSkill.OnRealse();
            onFinish = pFinish;
            skillAsset = AssetManager.Instance.LoadAsset<PlayableAsset>(pSkill.SkillTimeLineRes);

            playableDirector.playableAsset = skillAsset;
            SetPlayableAssetTarget(skillAsset);
            playableDirector.stopped -= OnFinish;
            playableDirector.stopped += OnFinish;
            playableDirector.Play();
            return true;
        }
        public void SetPlayableAssetTarget(PlayableAsset pAsset) {
            foreach (var o in pAsset.outputs)
            {
                //找到今天的主角 就是通常挂特效那个轨道 UnityEngine.Timeline.ControlTrack
                if (o.sourceObject != null && o.sourceObject.GetType() == typeof(UnityEngine.Timeline.ControlTrack))
                {
                    var controlTrack = o.sourceObject as ControlTrack;
                    //遍历这个轨道中的所有片段
                    foreach (var timelineClip in controlTrack.GetClips())
                    {
                        ControlPlayableAsset clip = timelineClip.asset as ControlPlayableAsset;

                        bool isvalid;
                        //关键！！！获取：通过引用找到ParentObject对象
                        Object obj = playableDirector.GetReferenceValue(clip.sourceGameObject.exposedName, out isvalid);

                        //关键！！！修改：通过设置引用的方式设置ParentObject
                        // PropertyName propertyName = clip.sourceGameObject.exposedName;
                        // playableDirector.SetReferenceValue(propertyName, obj);
                    }
                }
            }
        }
        public void OnFinish(PlayableDirector pPlayableDirector) {
            onFinish?.Invoke();
        }
    }
}

