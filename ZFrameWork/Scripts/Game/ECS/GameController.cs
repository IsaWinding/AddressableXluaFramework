using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class GameController : MonoBehaviour
{
    // 存放系统集的最高节点，当然它本身也是个系统集
    Systems _systems;
    private void Start()
    {
        // 获取当前的环境组Contexts，里面有game环境和input环境
        var contexts = Contexts.sharedInstance;
        // 创建系统集，将自定义的系统集添加进去
        var feature_ = new Feature("System");
         _systems = feature_.Add(new WorldSystem(contexts));
        // 初始化，会执行所有实现IInitialzeSystem的Initialize方法
        // 当然这边就会创建那个拥有DebugMessageComponent的Entity
        _systems.Initialize();
    }

    private void Update()
    {
        // 执行系统集中的所有Execute方法
        _systems.Execute();
        //  执行系统集中的所有Cleanup方法
        _systems.Cleanup();

       
    }
}
