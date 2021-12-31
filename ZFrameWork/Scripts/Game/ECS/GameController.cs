using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class GameController : MonoBehaviour
{
    // ���ϵͳ������߽ڵ㣬��Ȼ������Ҳ�Ǹ�ϵͳ��
    Systems _systems;
    Systems _battleSystems;
    private void Start()
    {
        // ��ȡ��ǰ�Ļ�����Contexts��������game������input����
        var contexts = Contexts.sharedInstance;
        // ����ϵͳ�������Զ����ϵͳ����ӽ�ȥ
        var feature_ = new Feature("System");
         _systems = feature_.Add(new WorldSystem(contexts));
        // ��ʼ������ִ������ʵ��IInitialzeSystem��Initialize����
        // ��Ȼ��߾ͻᴴ���Ǹ�ӵ��DebugMessageComponent��Entity
        _systems.Initialize();

        var battleFeature_ = new Feature("BattleSystem");
        _battleSystems = battleFeature_.Add(new BattleSystem(contexts));
        _battleSystems.Initialize();
    }

    private void Update()
    {
        // ִ��ϵͳ���е�����Execute����
        _systems.Execute();
        //  ִ��ϵͳ���е�����Cleanup����
        _systems.Cleanup();

        _battleSystems.Execute();
        _battleSystems.Cleanup();
    }
}
