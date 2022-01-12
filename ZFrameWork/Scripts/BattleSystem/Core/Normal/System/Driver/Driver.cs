using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DelayAction {
    public int acId;
    public float acTime;
    public System.Action onAciton;
    public bool IsPass(float pTime) {
        return pTime >= acTime;
    }
    public void OnAction() {
        onAciton?.Invoke();
    }
}
public class Driver : MonoBehaviour
{
    private float PassTime;
    private Action<float,float> action;
    private float Speed = 1;
    private float deltaTime;
    private bool isStop;
    private Dictionary<int,DelayAction> AllActions = new Dictionary<int, DelayAction>();
    private List<int> RemoveActionIds = new List<int>();
    private int _acId;
    public void RemoveDelayAction(int pAcId) {
        AllActions.Remove(pAcId);
    }
    public int AddDelayAction(float pDelayTime,System.Action pAction) {
        _acId++;
        var delayAction = new DelayAction() { acId = _acId, acTime =  PassTime  + pDelayTime, onAciton = pAction };
        AllActions.Add(_acId,delayAction);
        return _acId;
    }
    public void AddAction(Action<float, float> pAction){
        action += pAction;
    }
    public void RemoveAction(Action<float, float> pAction){
        action -= pAction;
    }
    public void SetStop(bool pIsStop){
        isStop = pIsStop;
    }
    public void SetSpeed(float pSpeed){
        Speed = pSpeed;
    }
    void FixedUpdate()
    {
        if (isStop)
            return;
        deltaTime = Time.fixedDeltaTime * Speed;
        PassTime += deltaTime;
        action.Invoke(PassTime, deltaTime);
        RemoveActionIds.Clear();
        foreach (var temp in AllActions.Values)
        {
            if (temp.IsPass(PassTime)){
                temp.OnAction();
                RemoveActionIds.Add(temp.acId);
            }
        }
        foreach (var temp in RemoveActionIds) {
            RemoveDelayAction(temp);
        }
    }
}
