using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 人類 : 移動速度、代理器、動畫控制器、死亡
/// </summary>
public class People : MonoBehaviour
{
    [Header ("移動速度"),Range (0,10)]

    public float Speed = 1.5f;

    /// <summary>
    /// 代理導覽器
    /// </summary>
    protected NavMeshAgent agent;
    /// <summary>
    /// 動畫控制器
    /// </summary>
    protected Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }


    public void Dead()
    {
        ani.SetTrigger("死亡");
        agent.isStopped = true;   // 停止導覽
        Destroy(gameObject ,1.4f); 
    }
}
