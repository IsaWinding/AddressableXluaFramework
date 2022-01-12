using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AniNameType
{
    Idle = 1,
    Move = 2,
    Jump = 3,
    Attack = 4,
    Dead = 5,
}

[System.Serializable]
public struct AniData
{
    public AniNameType type;
    public string name;
}

public class UnitAnimation : MonoBehaviour
{
    public List<AniData> aniDatas = new List<AniData>();
    private Dictionary<AniNameType, string> AniMap = new Dictionary<AniNameType, string>();

    private Animator animator;
    private string curAniName;
    private int curAniProx = 0;
    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        for (var i = 0; i < aniDatas.Count; i++)
        {
            if (AniMap.ContainsKey(aniDatas[i].type))
            {
                AniMap[aniDatas[i].type] = aniDatas[i].name;
            }
            else
            {
                AniMap.Add(aniDatas[i].type, aniDatas[i].name);
            }
        }
    }

    public bool PlayAniByType(AniNameType pAniType, int pProx, bool pIsForce = true,System.Action pOnFinish = null)
    {
        animator.speed = 1f;
        var aniName = AniMap[pAniType];
        return PlayAni(aniName, pProx, pIsForce,pOnFinish);
    }
    private System.Action onFinish;
    public bool PlayAni(string pAniName, int pProx, bool pIsForce = true ,System.Action pOnFinish = null)
    {
        if (!pIsForce&&curAniName != null && IsPlayAning(curAniName) && pProx <= curAniProx)
            return false;
        animator.CrossFade(pAniName, 0);
        curAniName = pAniName;
        curAniProx = pProx;
        onFinish = pOnFinish;
        return true;
    }
    private bool IsPlayAning(string pAniName)
    {
        AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        if ((animatorInfo.normalizedTime <= 1.0f) && (animatorInfo.normalizedTime > 0f) && (animatorInfo.IsName(pAniName)))
        {
            return true;
        }
        return false;
    }
    public void OnDeadFinish() {
        if (onFinish != null)
            onFinish.Invoke();
    }
    public void OnJumpFinisih()
    {
        if (onFinish != null)
            onFinish.Invoke();
    }
    public void OnAttackFinish()
    {
        if (onFinish != null)
            onFinish.Invoke();
    }
}
