using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class BattleController : MonoBehaviour
{
    Systems _battleSystems;
    // Start is called before the first frame update
    void Start()
    {
        var contexts = Contexts.sharedInstance;
        var battleFeature_ = new Feature("BattleSystem");
        _battleSystems = battleFeature_.Add(new BattleSystem(contexts));
        _battleSystems.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        _battleSystems.Execute();
        _battleSystems.Cleanup();
    }
}
