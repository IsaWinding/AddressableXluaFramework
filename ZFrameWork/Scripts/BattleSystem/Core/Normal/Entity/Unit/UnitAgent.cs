using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    private System.Action onReach;
    private bool NeedCheck = false;
    public void SetNavMeshAgentEnable(bool pEnable) {
        agent.enabled = pEnable;
    }
    void Start()
    {
        agent = this.gameObject.AddComponent<NavMeshAgent>();
        this.gameObject.AddComponent<CapsuleCollider>();
        agent.stoppingDistance = 0.1f;
    }
    public bool MoveToTaraget(Vector3 pTaragetPos,System.Action pOnReach) {
        if (agent.enabled == false)
            return false;
        agent.isStopped = false;
        agent.SetDestination(pTaragetPos);
        onReach = pOnReach;
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            NeedCheck = false;
            OnReach();
            return false;
        }
        else {
            NeedCheck = true;
            return true;
        }
    }
    private void OnReach() {
        if (onReach != null){
            onReach.Invoke();
            onReach = null;
        }
    }
    public void Stop()
    {
        NeedCheck = false;
        if(agent != null)
            agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedCheck && agent.enabled) {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                NeedCheck = false;
                OnReach();
            }
        }
    }
}
