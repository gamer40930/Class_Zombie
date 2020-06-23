using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleFarAttack : peopleTrack

{

    [Header("停止距離"),Range (1,10) ]
    public float stop = 3f;
    [Header("子彈")]
    public GameObject bullet;
    [Header("冷卻時間"),Range(0.1f,3f)]
    public float cd = 1.15f;
    private float timer;

    protected override void Start()
    {
        agent.stoppingDistance = stop;                 // 代理器.停止距離 = 停止距離
        target = GameObject.Find("殭屍").transform;
    }

    protected override void track()
    {
        if (target == null) return;
        agent.SetDestination(target.position);
        Debug.DrawLine(transform.position, target.position, Color.red);
        transform.LookAt(target);   // 看者目標

        if(agent.remainingDistance <= stop) { Attack(); } // 如果 代理器.距離 < 停止距離 就 攻擊
    }

    private void Attack()
    {
        timer += Time.deltaTime;  // 計時歸零

        if(timer >= cd )
        {
            timer = 0;
            ani.SetTrigger("攻擊");
            // Instantiate實例化(要生成的物件, 物件位置, 物件旋轉值) 
            GameObject temp = Instantiate(bullet, transform.position + transform.forward + transform.up, transform.rotation);  // 生成子彈
            Rigidbody rig = temp.AddComponent<Rigidbody>();   // 添加元件
            rig.AddForce(transform.forward * 1500);            // 子彈添加推力              
        }
                                                           
    }
}
