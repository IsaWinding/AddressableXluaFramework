using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Driver : MonoBehaviour
{
    private float PassTime;
    private Action<float,float> action;
    private float Speed = 1;
    private float deltaTime;
    private bool isStop;
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
    }
}
